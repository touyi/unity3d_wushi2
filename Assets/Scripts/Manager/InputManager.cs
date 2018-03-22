using UnityEngine;
using System.Collections;

public static class InputManager
{
    public static Vector2 GetInputVector2()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        return new Vector2(h, v);
    }
}
