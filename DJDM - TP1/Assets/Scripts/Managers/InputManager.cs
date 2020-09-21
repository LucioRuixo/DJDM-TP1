using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum InputType { Mouse, WASD, Arrows, TouchScreen }

#if UNITY_ANDROID
    Vector2 leftJoystickPosition = new Vector2(175f, 175f);
    Vector2 rightJoystickPosition = new Vector2(-175f, 175f);

    public GameObject joystickPrefab;
    public Transform canvas;
    public InputController player1Input;
    public InputController player2Input;

    void Awake()
    {
        player2Input.currentInput = InputType.TouchScreen;
        player1Input.currentInput = InputType.TouchScreen;

        Joystick rightJoystick = Instantiate(joystickPrefab, canvas).GetComponent<Joystick>();
        RectTransform rightJoystickRect = rightJoystick.GetComponent<RectTransform>();
        rightJoystickRect.anchorMin = rightJoystickRect.anchorMax = new Vector2(1f, 0f);
        rightJoystickRect.anchoredPosition = rightJoystickPosition;

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.Singleplayer)
            player1Input.joystick = rightJoystick;
        else
        {
            player2Input.joystick = rightJoystick;

            Joystick leftJoystick = Instantiate(joystickPrefab, canvas).GetComponent<Joystick>();
            RectTransform leftJoystickRect = leftJoystick.GetComponent<RectTransform>();
            leftJoystickRect.anchorMin = leftJoystickRect.anchorMax = Vector2.zero;
            leftJoystickRect.anchoredPosition = leftJoystickPosition;

            player1Input.joystick = leftJoystick;
        }
    }
#endif
}