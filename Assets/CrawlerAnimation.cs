using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Unit unit;

    [Header("Animation Triggers")]
    public bool unroot;
    public bool walk;
    public bool idle;
    public bool root;
    public bool moveable = true;

    //Animation Timers and Data
    private float unroot_timer = 0;
    private float idle_timer = 0;
    private float root_timer = 0;
    private float og_speed;

    [Header("Debugging")]
    [SerializeField] private bool AutoAnimate=true;
    [SerializeField] private bool a_unroot;
    [SerializeField] private bool a_walk;
    [SerializeField] private bool a_root;




    public void Start()
    {
        //VARIABLE SETTER
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();
        unit=GetComponent<Unit>();
        og_speed = agent.speed;
    }

    private void Update()
    {
        if (a_unroot)
        {
            animator.SetTrigger("unroot");
            a_unroot = false;
        }

        if (a_walk)
        {
            animator.SetTrigger("walking");
            a_walk = false;
        }

        if (a_root)
        {
            animator.SetTrigger("root");
            a_root = false;
        }

        if (moveable)
        {
            agent.speed = og_speed;
        }
        else
        {
            agent.speed = 0;
        }



        if (AutoAnimate)
        {


            //ON MOVEMENT
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                root_timer = 0;
                idle_timer = 0;
                idle = false;

                if (root)
                {
                    if (!unroot) { animator.SetTrigger("unroot"); unroot = true; }
                    moveable = false;

                    if (unroot_timer >= 1)
                    {
                        ResetTriggers();
                        if (!walk) { animator.SetTrigger("walking"); walk = true; }
                        moveable = true;
                    }
                    else unroot_timer += Time.deltaTime;
                }
                else
                {
                    if (!walk) { animator.SetBool("walking", true); walk = true; }
                }
            }

            //ON IDLE
            else
            {
                unroot_timer = 0;
                walk = false;
                animator.SetBool("walking", false);
                if (!idle) { animator.SetTrigger("idle"); idle = true; }

                if (idle_timer >= 1)
                {

                    if (!root) { animator.SetTrigger("root"); root = true; }
                    moveable = false;
                    if (root_timer >= 1)
                    {
                        moveable = true;
                    }
                    else root_timer += Time.deltaTime;
                }
                else idle_timer += Time.deltaTime;
            }
        }


    }

    void ResetTriggers()
    {
        idle=false;
        walk=false;
        root=false;
        unroot=false;
    }
}

