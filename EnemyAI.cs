using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody rb;
    public float hp = 500;

    [SerializeField] private GameObject player;
    private Transform playerTransform;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float speed = 3;
    [SerializeField] Animator animator;
    [SerializeField] private GameObject hpIndicator;

    private float playerHp;
    private float indicatorLength;
    private float damage = 2.9f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 100f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHp = player.GetComponent<Player>().hp;

        indicatorLength = hpIndicator.transform.localScale.x;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (animator.GetBool("death") != true)
        {
            if (distance <= 20)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                transform.LookAt(playerTransform.position);
                animator.SetBool("isDetected", true);
            }
            else if (distance >= 20)
            {
                animator.SetBool("isDetected", false);
            }

            Atack(distance);
        }
    }

    private bool alredyActive = false;

    private void Atack(float distance)
    {
        if (distance <= 1f && !alredyActive)
        {
            Debug.Log(indicatorLength);

            animator.SetBool("isAttack", true);
            alredyActive = true;

            Invoke(nameof(ClearAnim), 1.2f);
        }
    }

    private void ClearAnim()
    {
        if (playerHp > 0)
        {
            player.GetComponent<Player>().hp -= damage;
            hpIndicator.transform.localScale -= new Vector3(indicatorLength / (100 / damage), 0, 0);

        }
        
        alredyActive = false;
        animator.SetBool("isAttack", false);
    }

}
