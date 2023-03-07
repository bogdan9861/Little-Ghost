using TMPro;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemies;
    [SerializeField] private ParticleSystem portal;
    [SerializeField] private ParticleSystem portalEffect;
    [SerializeField] private GameObject endScreen;

    [SerializeField] private GameObject alertObject;
    [SerializeField] private TextMeshProUGUI alertMessage;

    [SerializeField] private AudioSource bgMusic;

    public bool crystalIsTaken;
    private bool fadeOut = false;

    private float timeElapsed;
    private float valueToLerp;
    private float delayTime = 5f;

    private bool isActive = false;
    private bool triggerIsActive = false;

    [System.Obsolete]

    private void Update()
    {
        if (fadeOut && !isActive)
        {
            valueToLerp = Mathf.Lerp(1, -2, timeElapsed / delayTime);
            timeElapsed += Time.deltaTime;

            portal.startColor = new Color(portal.startColor.r, portal.startColor.g, portal.startColor.b, valueToLerp);
            portalEffect.startColor = new Color(portal.startColor.r, portal.startColor.g, portal.startColor.b, valueToLerp);

            Invoke(nameof(End), 4);
        }

        if (triggerIsActive) accessCheck();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) triggerIsActive = true;

        if (other.CompareTag("Player"))
            showMessage("Нажмите E");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) triggerIsActive = false;
    }

    private void accessCheck()
    {   
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (enemies.transform.childCount == 0 && crystalIsTaken)
                fadeOut = true;

            if (!crystalIsTaken && enemies.transform.childCount > 0)
                showMessage("Сначала разберитесь с врагами и возьмите кристал!");

            else if (!crystalIsTaken)
                showMessage("У вас нет кристала возьмите кристал!");    

            else if (enemies.transform.childCount > 0)
                showMessage("Сначала разберитесь с врагами");
        }
    }

    private void showMessage(string message)
    {
        alertMessage.text = message;
        alertObject.SetActive(true);

        Invoke(nameof(HideMessage), 3);
    }

    private void HideMessage()
    {
        alertObject.SetActive(false);
    }

    private void End()
    {
        endScreen.SetActive(true);
        bgMusic.Stop();
    }

}
