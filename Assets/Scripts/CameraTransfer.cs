using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransfer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] scenes;
    public Vector3 rotationPivot;
    public int index;

    private void Start()
    {
        cam = Camera.main;
    }

    public void TransferCamera()
    {
        if (index >= 0 && index < scenes.Length)
        {
            cam.transform.position = scenes[index].transform.position;
            Quaternion targetRotation = scenes[index].transform.rotation * Quaternion.Euler(rotationPivot);
            cam.transform.rotation = targetRotation;
            cam.transform.SetParent(scenes[index].transform);

            // cam.orthographicSize = scenes[index].GetComponent<Camera>().orthographicSize;

            Debug.Log($"Camera transferred to scene {index}: Position {cam.transform.position}, Rotation {cam.transform.rotation}");
        }
        else
        {
            Debug.LogError("Index out of bounds for scenes array.");
        }
    }

    public void NextScene()
    {
        index = (index + 1) % scenes.Length;
        TransferCamera();
    }

    
    public void PreviousScene()
    {
        index = (index - 1 + scenes.Length) % scenes.Length;
        TransferCamera();
    }
}
