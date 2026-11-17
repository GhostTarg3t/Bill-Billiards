using UnityEngine;
using TMPro; // Necessário para acessar os componentes de texto

public class NeonPulse : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Material neonMaterial;

    [Header("Configurações do Neon")]
    public float brilhoMinimo = 0.1f; // Glow Power quando "apagado"
    public float brilhoMaximo = 0.8f; // Glow Power quando "aceso"
    public float velocidade = 1.5f;   // Rapidez da respiração

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        // Acessar '.fontMaterial' cria uma instância única para este objeto,
        // garantindo que você não modifique o arquivo azul do Font Asset permanentemente.
        neonMaterial = textMesh.fontMaterial;
    }

    void Update()
    {
        // O PingPong vai e volta entre 0 e 1 com base no tempo e na velocidade
        float tempoCiclo = Mathf.PingPong(Time.time * velocidade, 1f);

        // Lerp mistura o brilho mínimo e máximo de acordo com o ciclo do PingPong
        float poderAtual = Mathf.Lerp(brilhoMinimo, brilhoMaximo, tempoCiclo);

        // Altera o valor da propriedade no Shader em tempo real
        neonMaterial.SetFloat("_GlowPower", poderAtual);
    }
}