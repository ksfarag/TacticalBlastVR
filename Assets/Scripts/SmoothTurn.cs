using UnityEngine;
using UnityEngine.InputSystem;

public class SmoothTurn : MonoBehaviour
{
    //Simple script that reads a vector2 input action and rotate the parent object on the y-axis based on the action value
    public InputActionProperty RotateAction;
    public int TurnSpeed = 1;
    void Update()
    {
        float turnValue = RotateAction.action.ReadValue<Vector2>().x;
        transform.Rotate(0, turnValue * TurnSpeed, 0);
    }
}
