using UnityEngine;
using System.Collections.Generic;
using TMPro; // Necessário para usar o InputField

public class MonoStressManager : MonoBehaviour
{
    public GameObject prefab;
    public TMP_InputField inputQuantidade; // A referência da nossa caixa de texto
    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Note que agora a função não tem mais o "int count" entre os parênteses
    public void RunTest()
    {
        // Pega o texto, tenta converter para número inteiro (out int count)
        if (int.TryParse(inputQuantidade.text, out int count))
        {
            // Limpa o teste anterior
            foreach (var obj in spawnedObjects) Destroy(obj);
            spawnedObjects.Clear();

            // Instancia novos
            for (int i = 0; i < count; i++)
            {
                float x = (i % 20) * 5f;
                float z = (i / 20) * 5f;
                var go = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
                spawnedObjects.Add(go);
            }
            Debug.Log($"Mono: {count} objetos instanciados.");
        }
        else
        {
            Debug.LogWarning("Valor inválido! Digite apenas números no campo de texto.");
        }
    }
}