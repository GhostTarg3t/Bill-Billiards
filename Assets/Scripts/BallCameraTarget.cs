
using UnityEngine;
using StarterAssets;

public class BallCameraTarget : MonoBehaviour
{
    [Header("Alvo")]
    public Transform ball;
    public float height = 1.2f;

    [Header("Camera")]
    public float sensitivity = 1f;
    public float minPitch = -30f;
    public float maxPitch = 70f;

    private StarterAssetsInputs input;
    private float yaw;
    private float pitch;

    void Start()
    {
        if (ball == null) return;

        input = ball.GetComponent<StarterAssetsInputs>();

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    
void LateUpdate()
{
    if (ball == null || input == null)
        return;

    // A câmera acompanha a posição da bola
    transform.position = ball.position +
                         Vector3.up * height;

    // Recebe o movimento do mouse
    Vector2 lookInput = input.look;

    // Permite girar a câmera ao mesmo tempo
    // que movimenta a bola
    yaw += lookInput.x * sensitivity;
    pitch -= lookInput.y * sensitivity;

    pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

    transform.rotation = Quaternion.Euler(
        pitch,
        yaw,
        0f
    );

    Debug.Log("Mouse: " + input.look);
}

}   