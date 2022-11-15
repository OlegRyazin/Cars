using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private bool isBreaking;
    private int index;

    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    Rigidbody rb;

    [SerializeField] private bool isModCar;

    [SerializeField] private List<Material> CarColors = new List<Material>();
    [SerializeField] private GameObject CarModel;
    private MeshRenderer meshRendererCarModel;

    public enum CarTypesEnum
    {
        Simple = 1,
        Race = 2,
        Pickup = 3,
        Truck = 4,
        OilTruck = 5,
    }

    public CarTypesEnum types;
    public enum CarColorsEnum
    {
        Yellow = 1,
        Blue = 2,
        Red = 3,
        Gray = 4,
        Purple = 5
    }

    [SerializeField] private CarColorsEnum colors;
    public CarColorsEnum Colors
    {
        get { return colors; }
        set { colors = value; if (isModCar) { ChangeColor(); }; }
    }

    private void Awake()
    {
        if(isModCar)
        {
            meshRendererCarModel = CarModel.GetComponent<MeshRenderer>();
            ChangeColor();
        }       
        rb = GetComponent<Rigidbody>();
        index = CarsController.nextIndex++;
    }


    private void FixedUpdate()
    {
        if(index == CarsController.chosenCarIndex)
        {
            horizontalInput = CarsController.globalHorizontalInput;
            verticalInput = CarsController.globalVerticalInput;
            IsBreaking();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }     
    }

    private void IsBreaking()
    {
        isBreaking = false;
        if (verticalInput == 0 && rb.velocity.magnitude < 2) isBreaking = true;
        else
        {
            int type = 1;
            int angle = (int)transform.localEulerAngles.y;
            if (angle > 315 || angle < 45) type = 1;
            if (angle > 45 && angle < 135) type = 2;
            if (angle > 135 && angle < 225) type = 3;
            if (angle > 225 && angle < 315) type = 4;

            switch (type)
            {
                case 1:
                    if (verticalInput > 0 && rb.velocity.z < -1) isBreaking = true;
                    if (verticalInput < 0 && rb.velocity.z > 1) isBreaking = true;
                    break;
                case 2:
                    if (verticalInput > 0 && rb.velocity.x < -1) isBreaking = true;
                    if (verticalInput < 0 && rb.velocity.x > 1) isBreaking = true;
                    break;
                case 3:
                    if (verticalInput > 0 && rb.velocity.z > 1) isBreaking = true;
                    if (verticalInput < 0 && rb.velocity.z < -1) isBreaking = true;
                    break;
                case 4:
                    if (verticalInput > 0 && rb.velocity.x > 1) isBreaking = true;
                    if (verticalInput < 0 && rb.velocity.x < -1) isBreaking = true;
                    break;
            }
        }      
    }

    private void HandleMotor()
    {
        if (isBreaking)
        {
            frontLeftWheelCollider.brakeTorque = 100f;
            frontRightWheelCollider.brakeTorque = 100f;
            rearLeftWheelCollider.brakeTorque = 100f;
            rearRightWheelCollider.brakeTorque = 100f;
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;

            float power = verticalInput * motorForce * Time.deltaTime * 50f;
            frontLeftWheelCollider.motorTorque = power;
            frontRightWheelCollider.motorTorque = power;
            rb.AddForce(transform.forward * 3f * power);
        }
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
     
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ChangeColor()
    {
        int num = 1;
        switch(colors)
        {
            case CarColorsEnum.Yellow: num = 1;break; 
            case CarColorsEnum.Blue: num = 2; break; 
            case CarColorsEnum.Red: num = 3; break; 
            case CarColorsEnum.Gray: num = 4; break; 
            case CarColorsEnum.Purple: num = 5; break; 
        }
        Material[] mas = new Material[2];
        mas = meshRendererCarModel.materials;
        mas[0] = CarColors[num - 1];
        meshRendererCarModel.materials = mas;
    }

    public void OnMouseDown()
    {
        CarsController.chosenCarIndex = index;
    }
}
