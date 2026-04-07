using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;

[BurstCompile]
public partial struct WanderSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // 1. Pegamos o DeltaTime na Main Thread para passar para o Job
        float deltaTime = SystemAPI.Time.DeltaTime;

        // 2. Criamos o Job e agendamos a sua execução em paralelo
        new WanderJob
        {
            DeltaTime = deltaTime
        }.ScheduleParallel();
    }
}

// -------------------------------------------------------------
// O JOB: Esta é a tarefa que será distribuída pelos núcleos da CPU
// -------------------------------------------------------------
[BurstCompile]
public partial struct WanderJob : IJobEntity
{
    // Variável para receber o tempo do frame
    public float DeltaTime;

    // A função Execute substitui o seu antigo 'foreach'.
    // O Unity vai chamar isto automaticamente para cada entidade que tenha LocalTransform e WanderData.
    void Execute(ref LocalTransform transform, ref WanderData wander)
    {
        // Nota: Dentro do IJobEntity, não precisamos usar .ValueRO ou .ValueRW.
        // Acessamos as variáveis diretamente!
        float3 myPos = transform.Position;
        float3 targetPos = wander.CurrentTarget;

        // 1. Descobre a diferença entre os dois pontos
        float3 diferenca = targetPos - myPos;

        // 2. Zeramos a diferença no eixo Y (Achatamos para o plano XZ)
        diferenca.y = 0f;

        // 3. Calcula a distância usando a diferença achatada
        float distance = math.length(diferenca);

        if (distance < 0.2f)
        {
            var randomGen = wander.RandomGen;
            float2 randomDir = randomGen.NextFloat2Direction() * wander.WanderRadius;

            // Define o alvo mantendo o Y original
            wander.CurrentTarget = myPos + new float3(randomDir.x, 0, randomDir.y);
            wander.RandomGen = randomGen;
        }
        else
        {
            // 4. Calcula a direção achatada com segurança
            float3 direction = math.normalizesafe(diferenca);

            transform.Position += direction * wander.Speed * DeltaTime;

            // 5. Garantia extra opcional para travar a altura no Y = 2f
            // transform.Position.y = 2f; 
        }
    }
}