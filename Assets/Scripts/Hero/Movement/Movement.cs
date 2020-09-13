using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float MovementTrigger = 0.2f;
    public int TurnDelay = 100;
    public float RaycastDistance = 1f;
    public Vector3 TargetPosition;
    public LayerMask LayerMask;
    private bool isMoving;
    private AnimationHandler animationHandler;
    private BoxCollider2D boxCollider;

    void Start()
    {
        animationHandler = new AnimationHandler(GetComponentInChildren<Animator>(), TurnDelay);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isMoving)
        {
            InputState input = InputState.FromInput(MovementTrigger);
            if (input.Direction != Direction.None)
            {
                Collider2D collider = Physics2D.Raycast(transform.position, input.Direction.Vector2(), RaycastDistance, LayerMask).collider;
                bool canPass = collider == null;

                if (animationHandler.ReadyToMove && input.Powered && canPass)
                {
                    TargetPosition = transform.position + input.Direction.Vector2().Vector3();
                    StartCoroutine(Move());
                }

                animationHandler.SetState(input.Direction, isMoving);
            }
            else
            {
                animationHandler.SetMoving(false);
            }
        }
    }

    IEnumerator Move()
    {
        animationHandler.SetMoving(true);
        isMoving = true;
        while ((TargetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MovementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = TargetPosition;
        isMoving = false;
    }
}
