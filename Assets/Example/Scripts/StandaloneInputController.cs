using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneInputController : IInputController
{
    private readonly Dictionary<KeyCode, Vector2> directionData = new Dictionary<KeyCode, Vector2>()
    {
        {KeyCode.A, Vector2.left},
        {KeyCode.D, Vector2.right},
        {KeyCode.W, Vector2.up},
        {KeyCode.S, Vector2.down},
    };
    
    public Vector3 GetDirection()
    {
        var movement = GetMovement();
        return new Vector3(movement.x, movement.y, Input.mouseScrollDelta.y);
    }

    private Vector2 GetMovement()
    {
        var result = Vector2.zero;
        
        foreach (var directionKey in directionData.Keys)
        {
            if (Input.GetKey(directionKey))
            {
                result += directionData[directionKey];
            }
        }

        return result;
    }
}
