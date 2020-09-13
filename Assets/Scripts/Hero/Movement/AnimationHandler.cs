using UnityEngine;

public class AnimationHandler
{
    public Animator Animator { get; private set; }
    public Direction CharacterDirection { get; private set; }
    public bool ReadyToMove { get { return (Time.time) - lastTurnTime > turnDelay; } }

    private float lastTurnTime;
    private float turnDelay;

    public AnimationHandler(Animator animator, int turnDelay)
    {
        Animator = animator;
        lastTurnTime = Time.time;
        this.turnDelay = (float)turnDelay / 1000;
    }

    public void SetState(Direction direction, bool moving)
    {
        if (CharacterDirection != direction)
        {
            CharacterDirection = direction;
            if (!moving) lastTurnTime = Time.time;
        }

        this.SetMoving(true);
        Animator.SetFloat("moveX", direction.Vector2().x);
        Animator.SetFloat("moveY", direction.Vector2().y);
    }

    public void SetMoving(bool moving)
    {
        Animator.SetBool("isMoving", moving);
    }
}