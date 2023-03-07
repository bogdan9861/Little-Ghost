using System.Collections;
using TMPro;
using UnityEngine;

public class Wizard_dialog : MonoBehaviour
{
    [SerializeField] private GameObject[] phrases;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject enemies;

    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject portalTrigger;

    int dialogNumber = 0;
    private bool dialogEnded = false;

    private void Update()
    {
        if (enemies.transform.childCount == 0)
        {
            questText.text = "Закройте портал магическим кристалом";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialog();
        }
    }

    private void StartDialog()
    {
        if (!dialogEnded)
        {
            StartCoroutine(Dialog());
            phrases[dialogNumber].SetActive(true);
        }
    }

    IEnumerator Dialog()
    {
        while (dialogNumber < phrases.Length - 1)
        {
            yield return new WaitForSeconds(3);

            dialogNumber += 1;
            phrases[dialogNumber].SetActive(true);

            phrases[dialogNumber - 1].SetActive(false);

            if (dialogNumber == phrases.Length - 2)
            {
                item.SetActive(true);
                crystal.SetActive(true);
                key.SetActive(false);

                portalTrigger.GetComponent<PortalTrigger>().crystalIsTaken = true;

                Invoke(nameof(HideItem), 3);
            }

            if (dialogNumber == phrases.Length - 1)
            {
                questText.text = "Доберитесь до портала";
                dialogEnded = true;
                enemies.SetActive(true);

                Invoke(nameof(HideDialog), 3);
            }
        }
    }

    private void HideDialog()
    {
        phrases[^1].SetActive(false);
    }

    private void HideItem()
    {
        item.SetActive(false);
    }
}
