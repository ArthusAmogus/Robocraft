using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitMovement : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    [SerializeField] private GameObject SelectionMarker;
    private LayerMask ground;
    [SerializeField] private UnitTab UI;
    public Vector3 position;

    

    private void OnEnable()
    {
        SelectionMarker.SetActive(true);
    }

    private void OnDisable()
    {
        SelectionMarker.SetActive(false);
    }

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        ground = LayerMask.GetMask("Ground");
        UI = GameObject.Find("UnitSelection").GetComponent<UnitTab>();
    }

    private void Update()
    {
        if (!UI.isShown)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
                {
                    agent.SetDestination(position);
                }
            }
        }
    }
}
