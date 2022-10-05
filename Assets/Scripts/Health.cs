using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;

    [ReadOnly] public float curHealth;

    [Tooltip("Only used to show or hide values!!!")]
    public bool isPlayer = false;

    [ShowIf(nameof(isPlayer))]
    public Slider hpSlider;

    public UnityEvent damageEvent;
    public UnityEvent deathEvent;
    public GameObject explosionPrefab;

    public Vector3 explosionRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void UpdateHealthBar()
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = curHealth;
    }

    public void Damage(float damageAmount)
    {

        curHealth -= damageAmount;

        damageEvent?.Invoke();

        if (curHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deathEvent?.Invoke();
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.eulerAngles = explosionRotationOffset;
        Destroy(explosion, 2.0f);
    }
}
