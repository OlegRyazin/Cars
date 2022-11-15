using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class SellTrainController : MonoBehaviour
{
    [SerializeField] private List<Transform> Stations = new List<Transform> ();
    [SerializeField] private GameObject Train;

    private Train trainScript;
    private bool canNextTrain = false;

    public static Action nextSellTrainEvent;

    void Start()
    {
        nextSellTrainEvent += StartCoroutine_NextTrain;
        trainScript = Train.GetComponent<Train>();
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
        foreach (GameObject car in SellCheckTrainTrigger.carsOnCarriage) Destroy(car);
        SellCheckTrainTrigger.carsOnCarriage = new List<GameObject>();
        GameController.OrderGenerateEvent();
        trainScript.Departure(Stations[0], Stations[1]);
    }
}
