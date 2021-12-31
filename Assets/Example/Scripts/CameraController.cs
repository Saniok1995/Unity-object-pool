using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private float defaultSpeed;
    private float defaultZoomSpeed;
    private float currentSpeed;
    
    private const float MaxSpeed = 35;

    public void Init(Vector3 position, float speed, float zoomSpeed)
    {
        defaultSpeed = speed;
        defaultZoomSpeed = zoomSpeed;
        camera.transform.localPosition = position;
    }

    public void Move(Vector3 direction)
    {
        Vector2 moveDir = direction * Time.deltaTime * currentSpeed;
        var resultDirection = new Vector3(moveDir.x, moveDir.y, direction.z * Time.deltaTime * defaultZoomSpeed);
        var cameraPos = camera.transform.position;
        Vector3 result = Vector3.Lerp(cameraPos, cameraPos + resultDirection, 0.2f);
        camera.transform.position = result;
        
        CalculateAcceleration(direction);
    }

    private void CalculateAcceleration(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            currentSpeed = defaultSpeed;
        }
        else
        {
            float speed = currentSpeed + Time.deltaTime * currentSpeed;
            currentSpeed = Mathf.Clamp(speed, defaultSpeed, MaxSpeed);
        }
    }
}
