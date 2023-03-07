using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isDead = false;
    public float hp = 100f;
    private float indicatorLength;

    [SerializeField] private GameObject hpIndicator;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI text;

    private float timeElapsed;
    private float valueToLerp;
    private float delayTime = 5f;

    private bool fadeIn = false;

    private void Start()
    {
        indicatorLength = hpIndicator.transform.localScale.x;
    }

    private void Update()
    {
        if (hp <= 0 && !isDead)
        {
            deathScreen.SetActive(true);
            isDead = true;
            fadeIn = true;
        }

        if (fadeIn)
        {
            valueToLerp = Mathf.Lerp(-2, 1, timeElapsed / delayTime);
            timeElapsed += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, valueToLerp);

            Invoke(nameof(Restart), 6);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 10)
        {
            hp -= collision.relativeVelocity.magnitude * 3;
            hpIndicator.transform.localScale -= new Vector3(indicatorLength / (100 / collision.relativeVelocity.magnitude), 0, 0);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
