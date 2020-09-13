using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
    public int TurnDelay { get { return turnDelay; } }

    [SerializeField]
    [Header ("Private Fields")]
    private float movementSpeed = 5;
    [SerializeField]
    private float movementTrigger = 0.2f;
    [SerializeField]
    private float rayCastDistance = 1f;
    public Vector3 TargetPosition;
    [SerializeField]
    private LayerMask LayerMask;
    [SerializeField]
    private int turnDelay = 100;
    private bool isMoving;
    private AnimationHandler animationHandler;
    private BoxCollider2D boxCollider;
    private Player player;

    void Start () {
        animationHandler = GetComponent<AnimationHandler> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        player = GetComponent<Player> ();
    }

    void Update () {
        if (!isMoving) {
            InputState input = InputState.FromInput (movementTrigger);
            if (input.Direction != Direction.None) {
                Collider2D collider = Physics2D.Raycast (transform.position, input.Direction.Vector2 (), rayCastDistance, LayerMask).collider;
                bool canPass = collider == null;
                if (collider) {
                    CollisionEvent collisionEvent = collider.gameObject.GetComponent<CollisionEvent> ();
                    if (collisionEvent) {
                        collisionEvent.ExecuteEvent (player);
                    }
                }
                if (animationHandler.ReadyToMove && input.Powered && canPass) {
                    TargetPosition = transform.position + input.Direction.Vector2 ().Vector3 ();
                    StartCoroutine (Move ());
                }
                animationHandler.SetDirectionalMovement (input.Direction, isMoving);
            } else {
                animationHandler.SetMoving (false);

            }
        }
    }

    public IEnumerator Move () {
        animationHandler.SetMoving (true);
        isMoving = true;
        while ((TargetPosition - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards (transform.position, TargetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = TargetPosition;
        isMoving = false;
    }
}