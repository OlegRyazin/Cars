using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SellCheckTrainTrigger : MonoBehaviour
{
    [SerializeField] private Transform carriage;
    [SerializeField] private Transform carsPlace;

    public static List<GameObject> carsOnCarriage = new List<GameObject>();

    private void OnTriggerEnter(Collider collider)
    {     
        collider.transform.parent = carriage;
        carsOnCarriage.Add(collider.gameObject);    
        if (CheckOrder(GameController.Order)) SellTrainController.nextSellTrainEvent();
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.transform.parent = carsPlace;
        carsOnCarriage.Remove(collider.gameObject);
    }

    private bool CheckOrder(List<GameController.OrderCar> order)
    {
        bool orderDone = false;
        int num = 0;
        List<bool> check = new List<bool>();
        for (int i = 0; i < carsOnCarriage.Count; i++)
        {
            check.Add(false);   
        }

        for (int i = 0; i < order.Count; i++)
        {
            foreach (GameObject car in carsOnCarriage)
            {                
                if (!check[num] && !orderDone)
                {
                    if (car.GetComponent<Car>().types == order[i].type &&
                   car.GetComponent<Car>().Colors == order[i].color)
                    {
                        
                        check[num] = true;
                        orderDone = true;
                    }                       
                }               
                num++;               
            }
            num = 0;
            orderDone = false;
        }

        int countOfBool = 0;
        for (int i = 0; i < check.Count; i++)
        {
            if (check[i]) countOfBool++;
        }

        return countOfBool == order.Count;
    }
}
