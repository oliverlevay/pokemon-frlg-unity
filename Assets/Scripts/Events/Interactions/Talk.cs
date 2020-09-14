using UnityEngine;

public class Talk : Interaction {
    [SerializeField]
    Dialogue dialogue;
    public override void Interact () {
        DialogueController dialogueController = FindObjectOfType<DialogueController> ();
        dialogueController.TypeText (dialogue);
    }
}