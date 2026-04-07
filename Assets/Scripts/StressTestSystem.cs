using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections; // Obrigatório para usar NativeArray
using UnityEngine;
using Unity.Physics;

public partial struct StressTestSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        int amountToSpawn = 0;
        Entity cmdEntityToDestroy = Entity.Null;

        // 1. Procura pelo comando de botão apertado.
        // Usamos um break porque só queremos ler 1 clique de botão por frame.
        foreach (var (cmd, entity) in SystemAPI.Query<RefRO<SpawnCommand>>().WithEntityAccess())
        {
            amountToSpawn = cmd.ValueRO.Amount;
            cmdEntityToDestroy = entity;
            break;
        }

        // Se nenhum botão foi apertado, o sistema não faz nada neste frame
        if (amountToSpawn <= 0) return;

        Debug.Log($"ECS: Instanciando {amountToSpawn} objetos em Bulk.");

        // ---------------------------------------------------------
        // OTIMIZAÇÃO 1: DESTRUIÇÃO EM MASSA (Bulk Destroy)
        // Em vez de um foreach, pedimos para o EntityManager deletar 
        // a query inteira de uma vez só no nível da memória.
        // ---------------------------------------------------------
        var queryAntigos = SystemAPI.QueryBuilder().WithAll<StressMark>().Build();
        state.EntityManager.DestroyEntity(queryAntigos);


        // Pega o Prefab do Singleton
        Entity prefab = SystemAPI.GetSingleton<SpawnerConfig>().Prefab;


        // ---------------------------------------------------------
        // OTIMIZAÇÃO 2: INSTANCIAÇÃO EM MASSA (Bulk Instantiate)
        // Alocamos um array com o tamanho exato da horda e o Unity 
        // clona o Prefab para todos os espaços da memória de uma vez.
        // ---------------------------------------------------------
        var novasEntidades = new NativeArray<Entity>(amountToSpawn, Allocator.Temp);
        state.EntityManager.Instantiate(prefab, novasEntidades);


        // 3. Configura a posição inicial das entidades recém-nascidas
        for (int i = 0; i < novasEntidades.Length; i++)
        {
            float x = (i % 20) * 5f;
            float z = (i / 20) * 5f;
            float3 posicaoInicial = new float3(x, 2, z); // Guardamos a posição aqui

            // 3.1 Define a posição real no mundo
            state.EntityManager.SetComponentData(novasEntidades[i], LocalTransform.FromPosition(posicaoInicial));
            state.EntityManager.AddComponent<StressMark>(novasEntidades[i]);

            var wander = state.EntityManager.GetComponentData<WanderData>(novasEntidades[i]);

            // -------------------------------------------------------------
            // A NOVA CORREÇÃO: Reseta o alvo para o local de nascimento
            // -------------------------------------------------------------
            wander.CurrentTarget = posicaoInicial;

            // Corrige a semente (que já tínhamos feito)
            uint seed = (uint)(i + 1 + (SystemAPI.Time.ElapsedTime * 1000));
            wander.RandomGen = Unity.Mathematics.Random.CreateFromIndex(seed);

            // 1. Pega os dados de Massa da física
            var physicsMass = state.EntityManager.GetComponentData<PhysicsMass>(novasEntidades[i]);

            // 2. Zera a Inércia Inversa. (0,0,0) significa que ele não pode rotacionar
            physicsMass.InverseInertia = float3.zero;

            // Salva de volta
            state.EntityManager.SetComponentData(novasEntidades[i], wander);
        }

        // 4. Consome o comando (deleta a entidade do botão) e limpa a RAM
        state.EntityManager.DestroyEntity(cmdEntityToDestroy);
        novasEntidades.Dispose();
    }
}