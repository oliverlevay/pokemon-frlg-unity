using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionEvent : MonoBehaviour {
    public abstract void ExecuteEvent (Player player);
}