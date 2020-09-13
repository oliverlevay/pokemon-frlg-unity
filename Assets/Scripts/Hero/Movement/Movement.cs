using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float MovementTrigger = 0.2f;
    public int TurnDelay = 100;
    public float RaycastDistance = 1f;
    private bool isMoving;
    private AnimationHandler animationHandler;

    void Start()
    {
        animationHandler = new AnimationHandler(GetComponent<Animator>(), TurnDelay);
    }

    void Update()
    {
        if (!isMoving)
        {
            InputState input = InputState.FromInput(MovementTrigger);
            if (input.Direction != Direction.None)
            {
                Collider2D collider = Physics2D.Raycast(transform.position, input.Direction.Vector2(), RaycastDistance).collider;
                bool canPass = collider == null || (collider.tag.IsCollisionString() && collider.tag != input.Direction.GetCollisionString());

                if (animationHandler.ReadyToMove && input.Powered && canPass)
                {
                    StartCoroutine(Move(transform.position + input.Direction.Vector2().Vector3()));
                }

                animationHandler.SetState(input.Direction, isMoving);
            }
            else
            {
                animationHandler.SetMoving(false);
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        animationHandler.SetMoving(true);
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MovementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
}
