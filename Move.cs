using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

[SerializeField] class Move : MonoBehaviour
{
    public Transform playerModel;
    private Transform mainCamera;
    public Rigidbody rb;
    public float speed = 3f;
    public float rotationSpeed = 1f;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 offcet = new Vector3(0, 0, v) * Time.fixedDeltaTime * speed;
        transform.Translate(offcet);

        transform.Rotate(0, h * rotationSpeed, 0);
    }
}   
    