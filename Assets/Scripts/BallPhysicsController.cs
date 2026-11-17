
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(Rigidbody))]
public class BallPhysicsController : MonoBehaviour
{
    [Header("Movimentação")]
    public float acceleration = 18f;
    public float maxSpeed = 6f;

    private Rigidbody rb;
    private StarterAssetsInputs input;
    private Transform cameraTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<StarterAssetsInputs>();

        if (Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        if (cameraTransform == null || input == null)
            return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction =
            forward * input.move.y +
            right * input.move.x;

        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalVelocity =
            new Vector3(velocity.x, 0, velocity.z);

        if (direction.sqrMagnitude > 0.01f)
        {
            if (horizontalVelocity.magnitude < maxSpeed ||
                Vector3.Dot(horizontalVelocity, direction) < 0)
            {
                rb.AddForce(
                    direction * acceleration,
                    ForceMode.Acceleration
                );
            }
        }
    }
}
    