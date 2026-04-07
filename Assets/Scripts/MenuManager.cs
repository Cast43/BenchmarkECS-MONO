using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para trocar de cena

public class MenuManager : MonoBehaviour
{
    // Método 1: A forma mais limpa. Você digita o nome da cena direto no botão da Unity.
    public void CarregarCenaPorNome(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    // Método 2: Funções específicas para cada botão (se preferir deixar fixo no código)
    public void AbrirTesteMono()
    {
        // Substitua "CenaTesteMono" pelo nome exato do arquivo da sua cena
        SceneManager.LoadScene("MonoBehaviour");
    }

    public void AbrirTesteECS()
    {
        // Substitua "CenaTesteECS" pelo nome exato do arquivo da sua cena
        SceneManager.LoadScene("Ecs");
    }

    // Função bônus sempre útil para menus
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}