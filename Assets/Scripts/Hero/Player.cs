﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationState = Scripts.Enums.AnimationState;

public class Player : MonoBehaviour {
    private Movement movement;
    private SpriteRenderer spriteRenderer;
    private AnimationHandler animationHandler;
    private Collider2D playerCollision;
    private TransitionController transitionController;
    private void Start () {
        movement = GetComponent<Movement> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animationHandler = GetComponent<AnimationHandler> ();
        playerCollision = GetComponent<Collider2D> ();
        transitionController = FindObjectOfType<TransitionController> ();
    }
    public void DisableMovement () {
        movement.enabled = false;
    }
    public void EnableMovement () {
        movement.enabled = true;
    }
    public IEnumerator Warp (Warp target, Vector3 offset, AnimationState playerAnimation = AnimationState.None) {
        playerCollision.enabled = false;
        float animationTime = 0;
        if (playerAnimation != AnimationState.None) {
            DisableMovement ();
            animationHandler.PlayAnimation (playerAnimation);
            yield return new WaitForSeconds (0.001f);
            animationTime = animationHandler.GetAnimationTime ();
        }
        transitionController.BasicTransition ();
        yield return new WaitForSeconds (animationTime);
        transitionController.BasicTransition ();
        animationHandler.PlayAnimation (AnimationState.Walk);
        EnableMovement ();
        transform.position = target.gameObject.transform.position;
        movement.TargetPosition = target.gameObject.transform.position + offset;
        if (animationTime > 0) {
            StartCoroutine (movement.Move ());
        }
        yield return new WaitForSeconds (0.3f);
        playerCollision.enabled = true;
    }
}