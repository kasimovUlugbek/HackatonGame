using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    public int currentHealth = 100;
    public event Action onDamaged;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onDamaged?.Invoke();
    }
}
