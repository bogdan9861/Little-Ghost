using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject enemiesWrapper;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] enemies2;

    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource getHit;

    private void Start()
    {
        for (int i = 0; i < enemiesWrapper.transform.childCount; i++)
        {
            enemies[i] = enemiesWrapper.transform.GetChild(i).gameObject;
        }
    }

    public bool isAttack = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && !isAttack)
        {
            anim.SetBool("isAttack", true);
            playerAnim.SetBool("isAttack", true);

            Invoke(nameof(ClearAnim), .5f);
            Invoke(nameof(AttacCouldown), 1.3f);

            isAttack = true;
            attack.Play();
        }
    }

    private void ClearAnim()
    {
        anim.SetBool("isAttack", false);
        playerAnim.SetBool("isAttack", false);
    }

    private void AttacCouldown()
    {
        isAttack = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.CompareTag("Enemy") && anim.GetBool("isAttack"))
        {
            collider.GetComponent<EnemyAI>().hp -= 6;
            collider.GetComponent<Animator>().SetBool("getHit", true);
            getHit.Play();

            StartCoroutine(ClearHitAnim(collider));

            if (collider.GetComponent<EnemyAI>().hp <= 0)
            {
                collider.GetComponent<Animator>().Play("death");
                collider.GetComponent<Animator>().SetBool("death", true);
                StartCoroutine(ClearEnemy(collider));
            }
        }
    }

    IEnumerator ClearHitAnim(Collider collider)
    {
        while(true)
        {
            yield return new WaitForSeconds(.5f);
            collider.GetComponent<Animator>().SetBool("getHit", false);
        }
    }
        
    

    IEnumerator ClearEnemy(Collider enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            enemy.GetComponent<Animator>().Play("decrease");

            StartCoroutine(DisableEnenmy(enemy.gameObject));
        }
    }

    IEnumerator DisableEnenmy(GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            Destroy(enemy);
        }
    }

}
