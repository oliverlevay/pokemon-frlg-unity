using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float MovementTrigger = 0.5f;
    public int TurnDelay = 300;

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
                if (animationHandler.ReadyToMove && input.Powered)
                {
                    StartCoroutine(Move(transform.position + input.Direction.ToVector3()));
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
