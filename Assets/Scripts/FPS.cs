using UnityEngine;

public class FPS : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Update()
    {
        // Calcula o tempo que levou para renderizar o último frame
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        // Configuração visual do texto
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(20, 20, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.yellow;

        // Cálculos
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        // Mostra na tela: Milissegundos e FPS
        string texto = string.Format("{0:0.0} ms ({1:0.} FPS)", msec, fps);
        GUI.Label(rect, texto, style);
    }
}