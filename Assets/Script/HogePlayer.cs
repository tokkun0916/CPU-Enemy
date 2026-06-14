using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class HogePlayer : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) input.x -= 1;
            if (Keyboard.current.dKey.isPressed) input.x += 1;
            if (Keyboard.current.sKey.isPressed) input.y -= 1;
            if (Keyboard.current.wKey.isPressed) input.y += 1;
        }

        Vector3 move =
            new Vector3(input.x, 0f, input.y).normalized;

        transform.position +=
            move *
            _moveSpeed *
            Time.deltaTime;
    }
}
