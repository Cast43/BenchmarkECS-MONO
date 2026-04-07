using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    // Uma variável estática mantém o valor mesmo quando trocamos de cena
    public static string UltimaCenaCarregada;

    public void IrParaCena(string nomeDaCena)
    {
        // Antes de sair, guardamos o nome da cena atual como "última"
        UltimaCenaCarregada = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeDaCena);
    }

    public void Voltar()
    {
        if (!string.IsNullOrEmpty(UltimaCenaCarregada))
        {
            SceneManager.LoadScene(UltimaCenaCarregada);
        }
        else
        {
            // Caso não haja histórico, volta para o Menu por padrão
            SceneManager.LoadScene("MenuInicial");
        }
    }

    // Método simples para botões que apenas precisam voltar ao Menu
    public void IrParaMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}