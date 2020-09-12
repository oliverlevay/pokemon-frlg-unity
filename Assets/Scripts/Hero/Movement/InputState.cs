using UnityEngine;

public class InputState
{
    public Direction Direction { get; set; }
    public bool Powered { get; set; }

    public InputState(Direction direction, bool powered)
    {
        Direction = direction;
        Powered = powered;
    }

    public override string ToString()
    {
        return string.Format("{0} in {1}", Powered, Direction.ToString());
    }

    public static InputState FromInput(float movementTrigger)
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Direction direction = Direction.None;

        direction = inputRaw.x > 0 ? Direction.Right : (inputRaw.x < 0 ? Direction.Left : direction);
        direction = inputRaw.y > 0 ? Direction.Up : (inputRaw.y < 0 ? Direction.Down : direction);

        return new InputState(direction, input.magnitude >= movementTrigger);
    }
}