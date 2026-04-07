using UnityEngine;

public class MonoEnemyWander : MonoBehaviour
{
    public float velocidade = 2f;
    public float raioDeAndar = 5f;

    private Vector3 alvoAtual;

    void Start()
    {
        EscolherNovoAlvo();
    }

    void Update()
    {
        // Se a distância até o alvo for menor que 0.5, escolhe um novo
        if (Vector3.Distance(transform.position, alvoAtual) < 0.5f)
        {
            EscolherNovoAlvo();
        }

        // Calcula a direção e move
        Vector3 direcao = (alvoAtual - transform.position).normalized;
        transform.position += direcao * velocidade * Time.deltaTime;

        // Faz o objeto olhar para onde está andando
        if (direcao != Vector3.zero)
        {
            transform.forward = direcao;
        }
    }

    void EscolherNovoAlvo()
    {
        // Gera um ponto aleatório dentro de uma esfera e soma com a posição atual
        Vector3 pontoAleatorio = Random.insideUnitSphere * raioDeAndar;
        alvoAtual = transform.position + pontoAleatorio;

        // Zera o eixo Y para garantir que o inimigo não tente voar ou entrar no chão
        alvoAtual.y = transform.position.y;
    }
}