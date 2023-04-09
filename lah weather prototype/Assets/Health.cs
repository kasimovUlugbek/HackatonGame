using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
