using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static Action ReceiptGenerateEvent;
    public static Action OrderGenerateEvent;
    public static List<OrderCar> Receipt;
    public static List<OrderCar> Order;
    //public static List<GameObject> Cars = new List<GameObject>();
    
    private int OrderNumber = 1;
    [SerializeField] private List<Text> orderText = new List<Text>();

    public struct OrderCar
    {
        public Car.CarTypesEnum type;
        public Car.CarColorsEnum color;
        public OrderCar(Car.CarTypesEnum _type, Car.CarColorsEnum _color)
        {
            type = _type; 
            color = _color; 
        }
    }

    private void Awake()
    {
        ReceiptGenerateEvent += ReceiptGenerate;
        OrderGenerateEvent += OrderGenerate;
        OrderGenerate();
    }

    public void ReceiptGenerate()
    {
        Receipt = new List<OrderCar>();
        int CarCount = 6;

        Car.CarTypesEnum type = Car.CarTypesEnum.Race;
        Car.CarColorsEnum color = Car.CarColorsEnum.Yellow;
        for (int i = 0; i < CarCount; i++)
        {
            Random rand = new Random();
            switch (rand.Next(1, 6))
            {
                case 1: color = Car.CarColorsEnum.Yellow; break;
                case 2: color = Car.CarColorsEnum.Blue; break;
                case 3: color = Car.CarColorsEnum.Red; break;
                case 4: color = Car.CarColorsEnum.Gray; break;
                case 5: color = Car.CarColorsEnum.Purple; break;
            }
            Random rand2 = new Random();
            switch (rand2.Next(1, 3))
            {
                case 1: type = Car.CarTypesEnum.Simple; break;
                case 2: type = Car.CarTypesEnum.Race; break;
            }
            Receipt.Add(new OrderCar(type, color));
        }
    }

    public void OrderGenerate()
    {
        Order = new List<OrderCar>();

        int CarCount = 2;
        //if(OrderNumber =< 5) CarCount = 1;
        Car.CarTypesEnum type = Car.CarTypesEnum.Race;
        Car.CarColorsEnum color = Car.CarColorsEnum.Yellow;       
        for (int i = 0; i < CarCount; i++)
        {
            Random rand = new Random();
            switch (rand.Next(1, 6))
            {
                case 1: color = Car.CarColorsEnum.Yellow; break;
                case 2: color = Car.CarColorsEnum.Blue; break;
                case 3: color = Car.CarColorsEnum.Red; break;
                case 4: color = Car.CarColorsEnum.Gray; break;
                case 5: color = Car.CarColorsEnum.Purple; break;
            }
            Random rand2 = new Random();
            switch (rand2.Next(1, 3))
            {
                case 1: type = Car.CarTypesEnum.Simple; break;
                case 2: type = Car.CarTypesEnum.Race; break;
            }
            Order.Add(new OrderCar(type, color));
            orderText[i].text = color.ToString() + " " + type.ToString();
        }
    }
}
