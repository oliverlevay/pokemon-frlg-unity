using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRelativeToPlayer : MonoBehaviour
{
    public Player player;
    public Direction direction = Direction.Down;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = player.gameObject.transform.position;
        Vector3 thisPos = transform.position;
        switch (direction)
        {
            case Direction.Down:
                spriteRenderer.enabled = playerPos.y > thisPos.y;
                break;
            case Direction.Up:
                spriteRenderer.enabled = playerPos.y < thisPos.y;
                break;
            case Direction.Left:
                spriteRenderer.enabled = playerPos.x < thisPos.x;
                break;
            case Direction.Right:
                spriteRenderer.enabled = playerPos.x > thisPos.x;
                break;
            default:
                break;
        }
    }
}
