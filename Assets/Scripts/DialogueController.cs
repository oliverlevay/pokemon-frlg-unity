using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textShadow;
    [SerializeField]
    private TextMeshProUGUI textHighlight;
    private TextMeshProUGUI[] textBoxes;
    [SerializeField]
    private Color genderlessColor;
    [SerializeField]
    private Color femaleColor;
    [SerializeField]
    private Color maleColor;
    private float letterPause = 5f;
    private bool writingNewDialogue = false;
    private GameState gameState;
    private SpriteRenderer spriteRenderer;
    private void Start () {
        gameState = FindObjectOfType<GameState> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        textBoxes = new TextMeshProUGUI[] { textShadow, textHighlight };
    }
    public void TypeText (Dialogue dialogue) {
        SetTextColor (dialogue);
        if (!writingNewDialogue)
            StartCoroutine (WriteTextToBox (dialogue.regularDialogue));
    }
    private void SetTextColor (Dialogue dialogue) {
        switch (dialogue.gender) {
            case Gender.Female:
                textHighlight.color = femaleColor;
                break;
            case Gender.Male:
                textHighlight.color = maleColor;
                break;
            default:
                textHighlight.color = genderlessColor;
                break;
        }
    }
    private void ClearTextBox () {
        foreach (TextMeshProUGUI textBox in textBoxes) {
            textBox.text = "";
        }
    }
    private void AppendLetterToTextBox (char letter) {
        foreach (TextMeshProUGUI textBox in textBoxes) {
            textBox.text += letter;
        }
    }
    private string FormatDialogue (string dialoguePart) {
        string formattedDialogue = dialoguePart.Replace ("{PLAYER}", gameState.PlayerName);
        return formattedDialogue;
    }
    IEnumerator WriteTextToBox (string[] dialogue) {
        writingNewDialogue = true;
        spriteRenderer.enabled = true;
        gameState.Player.DisableMovement ();
        foreach (string dialoguePart in dialogue) {
            string formattedDialogue = FormatDialogue (dialoguePart);
            foreach (char letter in formattedDialogue.ToCharArray ()) {
                AppendLetterToTextBox (letter);
                yield return new WaitForSeconds (letterPause / 100);
            }
            // Wait for keypress to continue dialogue.
            while (!Input.GetButtonDown ("A")) {
                yield return null;
            }
            ClearTextBox ();
        }
        writingNewDialogue = false;
        spriteRenderer.enabled = false;
        gameState.Player.EnableMovement ();
    }
}