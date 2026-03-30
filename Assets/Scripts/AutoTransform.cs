using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransform : MonoBehaviour
{
    private float x, y, z;
    [Header("Position")]
    [SerializeField] private float PosX;
    [SerializeField] private float PosY;
    [SerializeField] private float PosZ;

    [Header("Rotation")]
    [SerializeField] private float RotX;
    [SerializeField] private float RotY;
    [SerializeField] private float RotZ;

    [Header("Properties")]
    [SerializeField] private bool DoMove = true;
    [SerializeField] private bool DoRotate = true;
    
    
    void Update()
    {
        if (DoRotate)
        {
            x = RotX * Time.deltaTime;
            y = RotY * Time.deltaTime;
            z = RotZ * Time.deltaTime;
            transform.Rotate(x, y, z);
        }

        if (DoMove)
        {
            x = PosX * Time.deltaTime;
            y = PosY * Time.deltaTime;
            z = PosZ * Time.deltaTime;
            transform.position = new(transform.position.x + x, transform.position.y + y, transform.position.z + z);
        }
    }
}
