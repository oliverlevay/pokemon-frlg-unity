using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5;
    public float movementTrigger = 0.5f;

    bool isMoving;
    Vector2 input;
    Vector2 inputRaw;

    Animator animator;

    bool facingMovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Disable diagonal direction
            if (input.y != 0) input.x = 0;

            if (inputRaw != Vector2.zero)
            {
                facingMovementDirection = inputRaw.x == animator.GetFloat("moveX") && inputRaw.y == animator.GetFloat("moveY");

                Debug.Log(animator.GetBool("isMoving").ToString() + " " + facingMovementDirection);

                if ((Mathf.Abs(input.x) > movementTrigger || Mathf.Abs(input.y) > movementTrigger))
                {
                    Vector3 targetPos = transform.position;
                    if (input.x != 0)
                        targetPos.x += input.x > 0 ? 1 : -1;
                    if (input.y != 0)
                        targetPos.y += input.y > 0 ? 1 : -1;

                    StartCoroutine(Move(targetPos));
                }
                animator.SetBool("isMoving", true);
                animator.SetFloat("moveX", inputRaw.x);
                animator.SetFloat("moveY", inputRaw.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        animator.SetBool("isMoving", true);
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
        //animator.SetBool("isMoving", false);
    }
}
