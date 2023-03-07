using TMPro;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogTrigger;
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;
    [SerializeField] private GameObject alertObject;
    [SerializeField] private GameObject hpIndicator;

    [SerializeField] private TextMeshProUGUI alertMessage;
    [SerializeField] private TextMeshProUGUI questText;

    public bool keyIsTaken;
    private float indicatorLength;

    private bool triggerIsActive;

    private void Start()
    {
        keyIsTaken = dialogTrigger.GetComponent<NPC_villager>().keyIsTaken;
    }

    private void Update()
    {
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

    private void OpenTheGate()
    {
        doorLeft.transform.rotation = Quaternion.Euler(0, 0, 0);
        doorRight.transform.rotation = Quaternion.Euler(0, 180, 0);
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

    private void accessCheck()
    {
        if (keyIsTaken && Input.GetKeyDown(KeyCode.E))
        {
            OpenTheGate();
            showMessage("Новая локация: чудесная поляна");
            questText.text = "Поговорите с чародеем";

        }
        else if (!keyIsTaken && Input.GetKeyDown(KeyCode.E))
        {
            showMessage("Что бы открыть ворота нужен ключ!");
        }
    }
}
