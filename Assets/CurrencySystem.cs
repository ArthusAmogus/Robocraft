using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public int PlayerCurency = 6000;
    public int AICurrency = 6000;
    [SerializeField] private bool AutoGenerateCurrency = true;
    [SerializeField] private float GenerateRate = 5;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (AutoGenerateCurrency && timer > GenerateRate)
        {
            PlayerCurency += 500;
            AICurrency += 500;
            timer = 0f;
        }
    }
}
