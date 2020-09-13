using UnityEngine;
using AnimationState = Scripts.Enums.AnimationState;

public class AnimationHandler : MonoBehaviour
{
    public bool ReadyToMove { get { return (Time.time) - lastTurnTime > movement.TurnDelay / 1000; } }
    private Direction characterDirection;
    private Animator animator;
    private float lastTurnTime;
    private float turnDelay;
    private Movement movement;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
        lastTurnTime = Time.time;
    }
    public void SetDirectionalMovement(Direction direction, bool moving)
    {
        if (characterDirection != direction)
        {
            characterDirection = direction;
            if (!moving) lastTurnTime = Time.time;
        }
        this.SetMoving(true);
        animator.SetFloat("moveX", direction.Vector2().x);
        animator.SetFloat("moveY", direction.Vector2().y);
    }
    public void SetMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }
    public void PlayAnimation(AnimationState animation)
    {
        animator.Play(animation.ToString());
    }
    public float GetAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}