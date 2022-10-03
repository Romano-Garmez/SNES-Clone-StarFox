using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;

    [ReadOnly] public float curHealth;

    public UnityEvent damageEvent;
    public UnityEvent deathEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        damageEvent?.Invoke();

        curHealth -= damageAmount;

        if (curHealth < 0)
        {
            Die();    
        }
    }

    private void Die()
    {
        damageEvent?.Invoke();
    }
}
