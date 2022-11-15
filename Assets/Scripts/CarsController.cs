using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CarsController : MonoBehaviour
{
    public static float globalHorizontalInput;
    public static float globalVerticalInput;
    public static int chosenCarIndex = 1;
    public static int nextIndex = 1;

    private CarControl input;

    private void Awake()
    {
        input = new CarControl();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        Vector2 moveDirection = input.CarControll.Move.ReadValue<Vector2>();
        globalHorizontalInput = moveDirection[0];
        globalVerticalInput = moveDirection[1];
    }
}
