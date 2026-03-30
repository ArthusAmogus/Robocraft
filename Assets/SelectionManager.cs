using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> AllUnits = new();
    public List<GameObject> SelectedUnits = new();

    private Camera cam;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Clickable;
    [SerializeField] private GameObject DirectionMarker;
    [SerializeField] private UnitTab UI;
    [SerializeField] private float UnitGroupSpacing;

    public static SelectionManager Instance { get; set; }

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!UI.isShown)
        {
            //Unit Selection
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Clickable))
                {
                    SelectByClicking(hit.collider.gameObject);
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift)) DeselectAll();
                }
            }

            //Marker Placement
            if (Input.GetMouseButtonDown(1) && SelectedUnits.Count > 0)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Ground))
                {
                    DirectionMarker.transform.position = hit.point;
                    DirectionMarker.SetActive(true);
                    MoveSquad(hit.point);

                }
            }
        }
    }

    private void SelectByClicking(GameObject unit)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Multiple unit selection
            if (!SelectedUnits.Contains(unit))
            {
                SelectedUnits.Add(unit);
                unit.GetComponent<UnitMovement>().enabled = true;
            }
            else
            {
                unit.GetComponent<UnitMovement>().enabled = false;
                SelectedUnits.Remove(unit);
            }
        }
        else
        {
            //One unit selection
            DeselectAll();
            SelectedUnits.Add(unit);
            unit.GetComponent<UnitMovement>().enabled = true;
        }
    }

    public void DragSelect(GameObject unit)
    {
        if (!SelectedUnits.Contains(unit))
        {
            SelectedUnits.Add(unit);
            unit.GetComponent<UnitMovement>().enabled = true;
        }
    }

    private void DeselectAll()
    {
        foreach (GameObject unit in SelectedUnits)
        {
            unit.GetComponent<UnitMovement>().enabled = false;
        }
        SelectedUnits.Clear();
    }

    public void MoveSquad(Vector3 target)
    {
        int count = SelectedUnits.Count;
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(count));
        float offset = (gridSize - 1) / 2f;
        for (int i = 0; i < count; i++)
        {
            int row = i / gridSize;
            int col = i % gridSize;
            float offsetX = (col - offset) * UnitGroupSpacing;
            float offsetZ = (offset - row) * UnitGroupSpacing;
            Vector3 destination = new(target.x + offsetX, target.y, target.z + offsetZ);
            SelectedUnits[i].GetComponent<UnitMovement>().position = destination;
        }
    }
}
