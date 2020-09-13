using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement Movement;
    void Start()
    {
        Movement = GetComponent<Movement>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionEvent collisionEvent = other.gameObject.GetComponent<CollisionEvent>();
        if (collisionEvent)
        {
            collisionEvent.ExecuteEvent(this);
        }
    }
}
