
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

    private void Awake()
    {
        yaw = transform.eulerAngles.y;
        pitch = NormalizeAngle(transform.eulerAngles.x);
        CacheInput();
    }


    private void LateUpdate()
    {
        if (ball == null)
            return;

        transform.position = ball.position + Vector3.up * height;

        if (input == null)
            CacheInput();

        if (input == null)
            return;

        Vector2 lookInput = input.look;
        yaw += lookInput.x * sensitivity;
        pitch = Mathf.Clamp(pitch + lookInput.y * sensitivity, minPitch, maxPitch);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void CacheInput()
    {
        if (ball != null)
            input = ball.GetComponent<StarterAssetsInputs>();
    }

    private static float NormalizeAngle(float angle)
    {
        return angle > 180f ? angle - 360f : angle;
    }
}