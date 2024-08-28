using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReader : ScriptableObject, Controls.IPlayerMapActions
{
    public Vector2 MoveInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}