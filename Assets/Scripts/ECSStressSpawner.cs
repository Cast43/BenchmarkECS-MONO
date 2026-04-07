using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// 1. O Autor e o Baker (MANTENHA ISSO)
public class ECSStressSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int spawnCount = 1000;

    class Baker : Baker<ECSStressSpawner>
    {
        public override void Bake(ECSStressSpawner authoring)
        {
            var entityPrefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
            AddComponent(new SpawnerConfig
            {
                Prefab = entityPrefab,
            });
        }
    }
}