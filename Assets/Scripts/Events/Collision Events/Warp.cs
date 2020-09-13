using System.Collections;
using UnityEngine;
using AnimationState = Scripts.Enums.AnimationState;

public class Warp : CollisionEvent {
    [SerializeField]
    private Warp warpTarget;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private AnimationState playerAnimation;
    public override void ExecuteEvent (Player player) {
        StartCoroutine (player.Warp (warpTarget, offset, playerAnimation));
    }
}