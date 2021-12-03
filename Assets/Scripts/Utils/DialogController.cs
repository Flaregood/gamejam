using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject abilityUI;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private Image profileImage;
    [SerializeField] private Image profileImageBackground;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text speakerNameText;
    [SerializeField] private int lettersPerSec;

    public IEnumerator StartDialog(Dialog dialogObject, IEnumerator next = null)
    {
        abilityUI.SetActive(false); // Disable ability UI
        dialogUI.SetActive(true); // Enable dialog UI

        profileImage.sprite = dialogObject.profileSprite;
        profileImageBackground.sprite = dialogObject.profileBackgroundSprite;
        speakerNameText.text = dialogObject.speakerName;

        dialogText.text = "";
        foreach (char letter in dialogObject.dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSec);

        }

        if (next != null)
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(next);
        }

        yield return new WaitForSeconds(1f);
        dialogUI.SetActive(false);
        abilityUI.SetActive(true);

    }

    void Start()
    {

    }
}
