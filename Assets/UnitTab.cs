using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class UnitTab : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject UnitUIBox;

    [Header("Marker Properties")]
    [SerializeField] private GameObject BaseRobot;
    public Transform SpawnStartingPoint;
    [SerializeField] private GameObject SpawnMarker;

    [Header("Debug")]
    [SerializeField] private bool Touchable=true;
    public bool isShown;

    //Data
    [SerializeField] private float ScreenWidth = 2532;
    [SerializeField] private Vector3 SpawnLocation;
    private LayerMask Ground;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Ground = LayerMask.GetMask("Ground");
        SpawnLocation = SpawnMarker.transform.position;
    }

    private void Update()
    { 

        if (Input.GetMouseButtonDown(1) && isShown)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Ground))
            {
                SpawnLocation = hit.point;
                SpawnMarker.transform.position = hit.point;
            }
        }
    }

    public void ShowUIToggle()
    {
        if (Touchable)
        {
            if (!isShown)
            {
                Touchable = false;
                UnitUIBox.transform.LeanMoveX(0, 0.5f).setEaseOutExpo().setOnComplete(() =>
                {
                    Touchable = true;
                });
                SpawnMarker.SetActive(true);
                isShown = true;
            }
            else
            {
                Touchable = false;
                UnitUIBox.transform.LeanMoveX(-(ScreenWidth / 3.5f), 0.5f).setEaseOutQuint().setOnComplete(() =>
                {
                    Touchable = true;
                });
                SpawnMarker.SetActive(false);
                isShown = false;
            }
        }

    }

    public void CreateUnit(string type)
    {
        GameObject Bot;
        switch (type)
        {
            default:
                Debug.Log("No unit type specified.");
                break;
            case "Turret":
                Bot = Instantiate(BaseRobot, SpawnStartingPoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Turret");
                Bot.GetComponent<NavMeshAgent>().SetDestination(SpawnLocation);
                break;
            case "Shotgun":
                Bot = Instantiate(BaseRobot, SpawnStartingPoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Shotgun");
                Bot.GetComponent<NavMeshAgent>().SetDestination(SpawnLocation);
                break;
            case "Barret":
                Bot = Instantiate(BaseRobot, SpawnStartingPoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Barret");
                Bot.GetComponent<NavMeshAgent>().SetDestination(SpawnLocation);
                break;
        }
    }
}
