using UnityEngine;

public class Falling : MonoBehaviour
{

    public bool isOnGround;

    [SerializeField] float smooth = 10f;
    [SerializeField] float jumpForce = 1f;
    private float rorationSpeed;

    [SerializeField] GameObject closed;
    [SerializeField] GameObject open;

    [SerializeField] Animator animator;

    private Rigidbody rb;
    private float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rorationSpeed = GetComponent<RBMove>()._rotationSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOnGround && !GetComponent<Attack>().isAttack)
            {
                Open();
                gameObject.GetComponent<RBMove>()._rotationSpeed = 2f;
            }
            else if (isOnGround)
            {
                Jump();
                gameObject.GetComponent<RBMove>()._rotationSpeed = rorationSpeed;
            }
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            Close();
            gameObject.GetComponent<RBMove>()._rotationSpeed = rorationSpeed;
        }
            
        if (isOnGround)
        {
            Close();
            gameObject.GetComponent<RBMove>()._rotationSpeed = rorationSpeed;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }

    private void Open()
    {
        closed.SetActive(false);
        open.SetActive(true);
        rb.drag = smooth;
    }

    private void Close()
    {
        closed.SetActive(true);
        open.SetActive(false);
        rb.drag = 0;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
