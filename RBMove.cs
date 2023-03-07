using UnityEngine;

public class RBMove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float _speed = 5f;
    public float _rotationSpeed = 10f;

    [SerializeField] GameObject playerModel;


    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxis("Mouse X");

        Vector3 velocity = new Vector3(0, 0, v) * _speed;
        velocity.y = rb.velocity.y;

        Vector3 worldVelocity = transform.TransformVector(velocity);
        rb.velocity = worldVelocity;

        rb.angularVelocity = new Vector3(0, h * _rotationSpeed, 0);
    }
}
