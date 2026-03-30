using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{

    private float timer = 0;
    [Header("Spawner Properties")]
    public float spawnrate = 10;
    [SerializeField] private GameObject EntityToSpawn;
    [SerializeField] private Transform Target;
    [SerializeField] private Transform spawnpoint;
    public bool DoSpawn = true;



    private void Start()
    {
        if (spawnpoint == null)
        {
            spawnpoint = transform;
        }
    }

    private void Update()
    {
        //Spawning
        timer += Time.deltaTime;
        if (timer >= spawnrate && DoSpawn)
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    CreateUnit("Turret");
                    break;
                case 1:
                    CreateUnit("Shotgun");
                    break;
                case 2:
                    CreateUnit("Barret");
                    break;
            }
            timer = 0;
        }


    }

    public void CreateUnit(string type)
    {
        GameObject Bot;
        float ratio;
        float position;
        Vector3 SelfPos = transform.position;
        Vector3 Sum = Target.position - SelfPos;


        
        
        
        switch (type)
        {
            default:
                Debug.Log("Error: No unit type specified.");
                break;

            case "Turret":
                Bot = Instantiate(EntityToSpawn, spawnpoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Turret");
                ratio = 2;
                position = Random.Range(0f, 1f);
                Sum = ((Sum / ratio) * position) + SelfPos;
                Sum.y = 0;
                Bot.GetComponent<AIUnitMovement>().Destination = Sum;
                break;

            case "Shotgun":
                Bot = Instantiate(EntityToSpawn, spawnpoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Shotgun");
                ratio = 4;
                position = Random.Range(0f, 3f);
                Sum = ((Sum / ratio) * position) + SelfPos;
                Sum.y = 0;
                Bot.GetComponent<AIUnitMovement>().Destination = Sum;
                break;

            case "Barret":
                Bot = Instantiate(EntityToSpawn, spawnpoint.position, Quaternion.identity);
                Bot.GetComponent<Unit>().SetBotType("Barret");
                ratio = 4;
                position = Random.Range(0f, 1f);
                Sum = ((Sum / ratio) * position) + SelfPos;
                Sum.y = 0;
                Bot.GetComponent<AIUnitMovement>().Destination = Sum;

                break;
        }
        
    }
}
