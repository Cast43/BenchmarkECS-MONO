using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;

public class WanderAuthoring : MonoBehaviour
{
    public float speed = 2f;
    public float wanderRadius = 5f;

    class Baker : Baker<WanderAuthoring>
    {
        public override void Bake(WanderAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            // Criamos uma semente (seed) baseada no ID do objeto para que 
            // cada inimigo ande para um lado diferente. A semente não pode ser zero.
            uint seed = (uint)authoring.gameObject.GetInstanceID();
            if (seed == 0) seed = 1;

            AddComponent(entity, new WanderData
            {
                Speed = authoring.speed,
                WanderRadius = authoring.wanderRadius,
                CurrentTarget = authoring.transform.position, // Começa como o próprio local
                RandomGen = Unity.Mathematics.Random.CreateFromIndex(seed)
            });
        }
    }
}