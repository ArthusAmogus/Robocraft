using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject AppliedObject;
    public Transform[] target;
    [SerializeField] private int index;
    [SerializeField] private float RotationSpeed=10;
    public Vector3 RotationOffset;
    public bool DisableX;
    public bool DisableY;
    public bool DisableZ;
    public bool doFollow;
    private float ogspeed;

    private void Start()
    {
        ogspeed = RotationSpeed;

        if (AppliedObject == null)
        {
            AppliedObject = this.gameObject;
        }
    }

    void Update()
    {
        if (doFollow)
        {
            if (target[index]!=null)
            {
                Vector3 direction = target[index].position - AppliedObject.transform.position;
                if (DisableX) direction.x = 0;
                if (DisableY) direction.y = 0;
                if (DisableZ) direction.z = 0;
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    Quaternion finalRotation = lookRotation * Quaternion.Euler(RotationOffset);

                    AppliedObject.transform.rotation = Quaternion.Lerp(
                        AppliedObject.transform.rotation,
                        finalRotation,
                        Time.deltaTime * RotationSpeed
                    );
                }
            }
            
        }
    }

    public void SetSpeed(float speed)
    {
        RotationSpeed = speed;
    }

    public void SetSpeed(bool v)
    {
        if (v) RotationSpeed = ogspeed;
    }

    public void SetLook(int num)
    {
        index = num;
    }
}
