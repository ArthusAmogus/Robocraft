using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToDisable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            gameObject.SetActive(false);
        }
    }
}
