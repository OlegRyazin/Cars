using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCheckTrainTrigger : MonoBehaviour
{
    [SerializeField] private Transform carriage;
    [SerializeField] private Transform carsPlace;

    public static List<GameObject> objectsOnCarriage = new List<GameObject>();

    private void OnTriggerEnter(Collider collider)
    {
        collider.transform.parent = carriage;
        objectsOnCarriage.Add(collider.gameObject);   
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.transform.parent = carsPlace;
        objectsOnCarriage.Remove(collider.gameObject);
        if (objectsOnCarriage.Count == 0) BuyTrainController.nextBuyTrainEvent();
    }
}
