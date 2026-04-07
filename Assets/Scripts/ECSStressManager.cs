using Unity.Entities;
using UnityEngine;
using TMPro; // Necessário para usar o InputField

public class ECSStressManager : MonoBehaviour
{
    public GameObject prefab;
    public TMP_InputField inputQuantidade; // A referência da nossa caixa de texto
    private EntityManager entityManager;

    void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        entityManager = world.EntityManager;
    }

    public void RunTest() 
    {
        if (int.TryParse(inputQuantidade.text, out int count))
        {
            Entity cmd = entityManager.CreateEntity();
            entityManager.AddComponentData(cmd, new SpawnCommand { Amount = count });
        }
        else
        {
            Debug.LogWarning("Valor inválido! Digite apenas números no campo de texto.");
        }
    }
}