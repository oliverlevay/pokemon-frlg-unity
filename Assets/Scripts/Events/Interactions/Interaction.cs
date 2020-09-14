using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Interactable))]
public abstract class Interaction : MonoBehaviour {
    public abstract void Interact ();
}