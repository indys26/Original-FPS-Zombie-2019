﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    bool isDead = false;


    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");

        Debug.Log("broadcast message received");

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        if (isDead)
        {
            return;
        }
        
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
