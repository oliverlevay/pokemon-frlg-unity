using System;
using System.Collections;
using System.Text.RegularExpressions;
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
        ClearTextBox ();
    }
    public void TypeText (Dialogue dialogue) {
        SetTextColor (dialogue);
        if (!writingNewDialogue) {
            if (dialogue.regularMaleDialogue != "" && !gameState.PlayerIsFemale) {
                StartCoroutine (WriteTextToBox (dialogue.regularMaleDialogue));
                return;
            } else if (dialogue.regularFemaleDialogue != "" && gameState.PlayerIsFemale) {
                StartCoroutine (WriteTextToBox (dialogue.regularFemaleDialogue));
                return;
            }
            StartCoroutine (WriteTextToBox (dialogue.regularDialogue));
        }
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
        string formattedDialogue = dialoguePart.Replace ("[PLAYER]", gameState.PlayerName).Replace ("[", "").Replace ("]", "");
        return formattedDialogue;
    }
    IEnumerator WriteTextToBox (string dialogue) {
        writingNewDialogue = true;
        spriteRenderer.enabled = true;
        gameState.Player.DisableMovement ();
        string[] splitDialogue = dialogue.Split (
            new [] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
        foreach (string row in splitDialogue) {
            float startTime = Time.time;
            Debug.Log (row);
            string formattedDialogue = FormatDialogue (row);
            foreach (char letter in formattedDialogue.ToCharArray ()) {
                AppendLetterToTextBox (letter);
                yield return new WaitForSeconds (letterPause / 100);
            }
            // Wait for keypress to continue dialogue.
            Debug.Log ("Waiting for keypress");
            while (!Input.GetButtonDown ("A") || !(Time.time > (startTime + 0.5f))) {
                yield return null;
            }
            ClearTextBox ();
        }
        writingNewDialogue = false;
        spriteRenderer.enabled = false;
        gameState.Player.EnableMovement ();
    }
}