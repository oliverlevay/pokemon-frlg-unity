using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public Interaction[] Interactions;
    public void Interact () {
        Interactions[0].Interact ();
    }
}