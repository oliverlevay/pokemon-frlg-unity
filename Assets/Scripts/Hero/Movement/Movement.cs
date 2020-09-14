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
    private Vector2 facingDirection = new Vector2 (0, 1);

    void Start () {
        animationHandler = GetComponent<AnimationHandler> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        player = GetComponent<Player> ();
    }

    void Update () {
        bool aButtonClicked = Input.GetButtonDown ("A");
        if (!isMoving) {
            InputState input = InputState.FromInput (movementTrigger);
            Collider2D rayCastCollider = null;
            bool playerTriesToMove = input.Direction != Direction.None;

            if (playerTriesToMove || aButtonClicked) {
                if (playerTriesToMove)
                    facingDirection = input.Direction.Vector2 ();
                rayCastCollider = Physics2D.Raycast (transform.position, facingDirection, rayCastDistance, LayerMask).collider;
            }

            if (playerTriesToMove) {
                bool canPass = rayCastCollider == null;
                if (rayCastCollider) {
                    CollisionEvent collisionEvent = rayCastCollider.gameObject.GetComponent<CollisionEvent> ();
                    if (collisionEvent) {
                        collisionEvent.ExecuteEvent (player);
                    }
                }
                if (animationHandler.ReadyToMove && input.Powered && canPass) {
                    TargetPosition = transform.position + facingDirection.Vector3 ();
                    StartCoroutine (Move ());
                }
                animationHandler.SetDirectionalMovement (input.Direction, isMoving);
            } else {
                animationHandler.SetMoving (false);
                if (aButtonClicked && rayCastCollider) {
                    Interactable interactable = rayCastCollider.GetComponent<Interactable> ();
                    if (interactable) {
                        interactable.Interact ();
                    }
                }
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