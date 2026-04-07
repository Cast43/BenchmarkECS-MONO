using Unity.Entities;
using Unity.Mathematics;

public struct SpawnCommand : IComponentData
{
    public int Amount;
}

public struct SpawnerConfig : IComponentData
{
    public Entity Prefab;
}

public struct StressMark : IComponentData { }

public struct WanderData : IComponentData
{
    public float Speed;
    public float WanderRadius;
    public float3 CurrentTarget;
    public Unity.Mathematics.Random RandomGen; // O "dado" aleatório individual
}
