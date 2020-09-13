
using System.Collections;
using UnityEngine;

public class Warp : CollisionEvent
{
    public Warp warpTarget;
    public Vector3 offset;
    public override void ExecuteEvent(Player player)
    {
        player.transform.position = warpTarget.transform.position;
        player.Movement.TargetPosition = warpTarget.transform.position + offset;
        BoxCollider2D playerCollision = player.gameObject.GetComponent<BoxCollider2D>();
        playerCollision.enabled = false;
        StartCoroutine(ReEnablePlayerCollision(playerCollision));

    }
    IEnumerator ReEnablePlayerCollision(BoxCollider2D playerCollision)
    {
        yield return new WaitForSeconds(1);
        playerCollision.enabled = true;
    }
}
