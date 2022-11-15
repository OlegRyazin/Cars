using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private float lastMouseCord;
    private float diffMouseCord;
    private bool isFirstTime = true;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isFirstTime)
            {
                lastMouseCord = Input.mousePosition.y;
                isFirstTime = false;
            }
            diffMouseCord = Input.mousePosition.y - lastMouseCord;
            lastMouseCord = Input.mousePosition.y;
            camera.transform.position -= new Vector3(0, 0, diffMouseCord / 3);
        }
        else isFirstTime = true;
    }
}
