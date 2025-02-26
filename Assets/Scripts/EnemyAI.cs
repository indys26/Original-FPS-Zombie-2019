﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;

    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
   
    float distanceToTarget = Mathf.Infinity;

    EnemyHealth enemyHealth;

    Transform target;

    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;

    }

    // Update is called once per frame
    void Update()
    {

           distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (enemyHealth.IsDead())

            {

                enabled = false;

                navMeshAgent.enabled = false;

            }

            else if (!enemyHealth.IsDead())

            {

                if (isProvoked)

                {

                    EngageTarget();

                }

                else if (distanceToTarget <= chaseRange)

                {

                    isProvoked = true;

                    ChaseTarget();

                }

            }

        }
    



        public void OnDamageTaken()
    {
        isProvoked = true;
        Debug.Log("broadcast message sent");
    }


    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
           ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);

        GetComponent<Animator>().SetTrigger("move");

        navMeshAgent.SetDestination(target.position);
    }
    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);

        Debug.Log(name + " is attacking " + target.name);
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
