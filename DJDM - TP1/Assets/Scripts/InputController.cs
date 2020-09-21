using UnityEngine;

public class InputController : MonoBehaviour
{
    public InputManager.InputType currentInput = InputManager.InputType.Mouse;

    public bool active = true;

    float turn = 0;

    public Joystick joystick;

    //---------------------------------------------------------//
    void Update()
    {
        switch (currentInput)
        {
            case InputManager.InputType.Mouse:
                if (active)
                    gameObject.SendMessage("SetTurn", MousePercentScreen.Relation(MousePercentScreen.AxisRelation.Horizontal)); //debe ser reemplanado
                break;
            case InputManager.InputType.WASD:
                if (active)
                {
                    if (Input.GetKey(KeyCode.A))
                        gameObject.SendMessage("SetTurn", -1);

                    if (Input.GetKey(KeyCode.D))
                        gameObject.SendMessage("SetTurn", 1);
                }
                break;
            case InputManager.InputType.Arrows:
                if (active)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        gameObject.SendMessage("SetTurn", -1);

                    if (Input.GetKey(KeyCode.RightArrow))
                        gameObject.SendMessage("SetTurn", 1);
                }
                break;
#if UNITY_ANDROID
            case InputManager.InputType.TouchScreen:
                if (joystick.Horizontal < 0f)
                    gameObject.SendMessage("SetTurn", -1);
                else if ((joystick.Horizontal > 0f))
                    gameObject.SendMessage("SetTurn", 1);
                break;
#endif
        }
    }

    public float GetTurn()
    {
        return turn;
    }
}