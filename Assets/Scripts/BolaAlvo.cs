
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BolaAlvo : MonoBehaviour
{
    [Header("Área segura da mesa")]
    public Transform centroMesa;
    public float limiteX = 1f;
    public float limiteZ = 2f;

    [Header("Movimentação automática")]
    public float velocidade = 1.5f;

    [Header("Impulso ao ser atingida")]
    public float impulsoMinimo = 0.2f;
    public float multiplicadorImpulso = 0.1f;

    private Rigidbody rb;
    private SphereCollider sphere;
    private Vector3 destino;

    private float altura;
    private float raio;

    private bool atingida = false;
    private bool pontuada = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();

        // Protegida enquanto se movimenta sozinha
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousSpeculative;
    }

    void Start()
    {
        if (centroMesa == null)
        {
            Debug.LogError("Configure Centro Mesa!", this);
            enabled = false;
            return;
        }

        raio = sphere.radius * Mathf.Max(
            Mathf.Abs(transform.lossyScale.x),
            Mathf.Abs(transform.lossyScale.y),
            Mathf.Abs(transform.lossyScale.z)
        );

        // CentroMesa deve estar na altura do feltro
        altura = centroMesa.position.y + raio;

        rb.position = new Vector3(
            rb.position.x,
            altura,
            rb.position.z
        );

        EscolherDestino();
    }

    void FixedUpdate()
    {
        if (atingida || centroMesa == null)
            return;

        Vector3 atual = LimitarPosicaoSegura(rb.position);

        if (atual != rb.position)
            rb.position = atual;

        Vector3 diferenca = destino - atual;

        if (diferenca.sqrMagnitude < 0.04f)
        {
            EscolherDestino();
        }

        Vector3 proxima = Vector3.MoveTowards(
            atual,
            destino,
            velocidade * Time.fixedDeltaTime
        );
        proxima = LimitarPosicaoSegura(proxima);

        Vector3 deslocamento = proxima - atual;

        rb.MovePosition(proxima);

        // Simula a rolagem enquanto está cinemática
        if (deslocamento.sqrMagnitude > 0.000001f)
        {
            Vector3 eixo = Vector3.Cross(
                Vector3.up,
                deslocamento.normalized
            );

            float angulo =
                deslocamento.magnitude / raio *
                Mathf.Rad2Deg;

            rb.MoveRotation(
                Quaternion.AngleAxis(angulo, eixo)
                * rb.rotation
            );
        }
    }

    void EscolherDestino()
    {
        float margemX = raio / Mathf.Max(
            Mathf.Abs(centroMesa.lossyScale.x),
            0.0001f
        );
        float margemZ = raio / Mathf.Max(
            Mathf.Abs(centroMesa.lossyScale.z),
            0.0001f
        );

        float limiteSeguroX = Mathf.Max(0f, limiteX - margemX);
        float limiteSeguroZ = Mathf.Max(0f, limiteZ - margemZ);

        Vector3 posicaoLocal = new Vector3(
            Random.Range(-limiteSeguroX, limiteSeguroX),
            0f,
            Random.Range(-limiteSeguroZ, limiteSeguroZ)
        );

        destino = centroMesa.TransformPoint(posicaoLocal);
        destino.y = altura;
    }

    private Vector3 LimitarPosicaoSegura(Vector3 posicao)
    {
        Vector3 posicaoLocal = centroMesa.InverseTransformPoint(posicao);
        float margemX = raio / Mathf.Max(
            Mathf.Abs(centroMesa.lossyScale.x),
            0.0001f
        );
        float margemZ = raio / Mathf.Max(
            Mathf.Abs(centroMesa.lossyScale.z),
            0.0001f
        );

        posicaoLocal.x = Mathf.Clamp(
            posicaoLocal.x,
            -Mathf.Max(0f, limiteX - margemX),
            Mathf.Max(0f, limiteX - margemX)
        );
        posicaoLocal.z = Mathf.Clamp(
            posicaoLocal.z,
            -Mathf.Max(0f, limiteZ - margemZ),
            Mathf.Max(0f, limiteZ - margemZ)
        );

        Vector3 posicaoSegura = centroMesa.TransformPoint(posicaoLocal);
        posicaoSegura.y = altura;
        return posicaoSegura;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (atingida)
            return;

        BallPhysicsController jogador =
            collision.collider.GetComponentInParent
                <BallPhysicsController>();

        if (jogador == null)
            return;

        atingida = true;

        // Agora a bola obedece à física
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;

        Rigidbody corpoJogador =
            jogador.GetComponent<Rigidbody>();

        Vector3 direcao =
            rb.position - jogador.transform.position;

        direcao.y = 0f;

        float velocidadeJogador = 0f;

        if (corpoJogador != null)
        {
            Vector3 v = corpoJogador.linearVelocity;
            v.y = 0f;
            velocidadeJogador = v.magnitude;

            if (direcao.sqrMagnitude < 0.0001f)
                direcao = v;
        }

        if (direcao.sqrMagnitude < 0.0001f)
            direcao = Vector3.forward;

        float impulso = Mathf.Max(
            impulsoMinimo,
            velocidadeJogador * multiplicadorImpulso
        );

        rb.AddForce(
            direcao.normalized * impulso,
            ForceMode.Impulse
        );
    }

    public bool TentarPontuar()
    {
        if (!atingida || pontuada)
            return false;

        pontuada = true;
        return true;
    }

    void OnDrawGizmosSelected()
    {
        if (centroMesa == null)
            return;

        Gizmos.color = Color.green;

        Matrix4x4 anterior = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(
            centroMesa.position,
            centroMesa.rotation,
            Vector3.one
        );

        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(limiteX * 2f, 0.02f, limiteZ * 2f)
        );

        Gizmos.matrix = anterior;
    }
}
