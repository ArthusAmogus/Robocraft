
using UnityEngine;
using Random = UnityEngine.Random;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject AppliedObject;
    [SerializeField] private Transform target;
    [SerializeField] private float Speed=10;
    [SerializeField] private bool DoFollow = true;
    public GameObject[] GivenSequences;
    public GameObject[] HiddenSequences;
    [SerializeField] private float delay;
    [SerializeField] private bool doRandomDelay;
    [SerializeField] private bool doRandomOrder;
    [SerializeField] private bool doSequence = true;
    [SerializeField] private Vector2 MinMaxDelay;
    
    [SerializeField] private float timer;
    [SerializeField] private int index;
    private float ogspeed;

    private void Start()
    {
        ogspeed = Speed;

        if (AppliedObject == null)
        {
            AppliedObject = this.gameObject;
        }

        if (target==null)
        {
            target = GivenSequences[0].transform;
        }
    }

    public void StayIn(int index, bool isHidden, float speed)
    {
        doSequence = false;
        Speed = speed;
        if (isHidden) target = HiddenSequences[index].transform;
        else target = GivenSequences[index].transform;
    }

    public void ResumeSequence(int index, float speed)
    {
        doSequence=true;
        Speed = speed;
        target = GivenSequences[index].transform;
    }

    public void ResumeSequence(int index, bool doDefaultSpeed)
    {
        doSequence = true;
        if (doDefaultSpeed) Speed = ogspeed;
        target = GivenSequences[index].transform;
    }

    void Update()
    {
        if (DoFollow && target!=null) AppliedObject.transform.position = Vector3.Lerp(AppliedObject.transform.position, target.position, Time.deltaTime * Speed);
        if (timer>delay && doSequence)
        {
            if (doRandomOrder) index = Random.Range(0, GivenSequences.Length - 1);
            else { index++; if (index > GivenSequences.Length-1) index = 0; }
            if (doRandomDelay) delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
            target = GivenSequences[index].transform;
            timer = 0;
        }
        else if (doSequence) timer += Time.deltaTime;
    }
}