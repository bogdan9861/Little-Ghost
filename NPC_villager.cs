using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_villager : MonoBehaviour
{

    public bool keyIsTaken;
    private bool dialogEnded = false;
    int dialogNumber = 0;

    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private GameObject[] phrases;
    [SerializeField] private GameObject endPhrase;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject enemies;

    [SerializeField] private TextMeshProUGUI endPhraseText;
    [SerializeField] private TextMeshProUGUI questText;

    private bool enemiesIsAlife = true;

    private void Update()
    {
        if (enemies.transform.childCount == 0 && enemiesIsAlife)
        {
            questText.text = "Приймите награду от жителя";
            enemiesIsAlife = false;
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

        } else if (dialogEnded && enemies.transform.childCount == 0)
        {
            endPhrase.SetActive(true);
            endPhraseText.text = "Спасибо за помощь! Этот ключь поможет тебе пройти к порталу пришельцев";

            key.SetActive(true);
            doorTrigger.GetComponent<DoorTrigger>().keyIsTaken = true;

            questText.text = "Откройте ворота";

            Invoke(nameof(HideEndPhrase), 3);
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

            if (dialogNumber == phrases.Length - 1)
            {
                questText.text = "Уничтожте всех пришельцев";
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

    private void HideEndPhrase()
    {
        endPhrase.SetActive(false);
        key.SetActive(false);
    }
}
