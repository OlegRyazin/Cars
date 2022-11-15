using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuyTrainController : MonoBehaviour
{
    [SerializeField] private List<Transform> Stations = new List<Transform>();
    [SerializeField] private List<Transform> PlaceForObjects = new List<Transform>();
    [SerializeField] private GameObject Train;
    [SerializeField] private List<GameObject> CarsPrefs;
    [SerializeField] private Transform carriage;

    private Train trainScript;
    private bool canNextTrain = false;

    public static Action nextBuyTrainEvent;

    void Start()
    {
        nextBuyTrainEvent += StartCoroutine_NextTrain;
        trainScript = Train.GetComponent<Train>();
        GameController.ReceiptGenerateEvent();
        SpawnObjects(GameController.Receipt);
        trainScript.Departure(Stations[0], Stations[1]);
    }

    private void StartCoroutine_NextTrain()
    {
        StartCoroutine(NextTrain());
    }

    private IEnumerator NextTrain()
    {
        yield return new WaitForSeconds(1f);
        canNextTrain = false;
        trainScript.Departure(Stations[1], Stations[2]);
        yield return new WaitForSeconds(5f);
        foreach (GameObject car in BuyCheckTrainTrigger.objectsOnCarriage) Destroy(car);
        BuyCheckTrainTrigger.objectsOnCarriage = new List<GameObject>();
        GameController.ReceiptGenerateEvent();
        Train.transform.position = Stations[0].transform.position;
        SpawnObjects(GameController.Receipt);
        trainScript.Departure(Stations[0], Stations[1]);
    }

    private void SpawnObjects(List<GameController.OrderCar> receipt)
    {
        for(int i = 0; i < receipt.Count; i++)
        {
            GameObject CarPref = CarsPrefs[0];
            switch (receipt[i].type)
            {
                case Car.CarTypesEnum.Simple: CarPref = CarsPrefs[0]; break;
                case Car.CarTypesEnum.Race: CarPref = CarsPrefs[1]; break;
            }
            GameObject car = Instantiate(CarPref);
            car.transform.parent = carriage;
            car.transform.position = PlaceForObjects[i].position;
            car.GetComponent<Car>().types = receipt[i].type;               
            car.GetComponent<Car>().Colors = receipt[i].color;               
        }
    }
}
