using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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

    public Material damageFlashMaterial;
    public List<MeshFilter> meshes;

    private List<MeshRenderer> renderers = new List<MeshRenderer>();


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        InitializeDamageFlash();
    }

    public void UpdateHealthBar()
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = curHealth;
    }

    public void InitializeDamageFlash()
    {
        meshes.ForEach(m =>
        {
            var go = new GameObject($"DamageFlash{m.sharedMesh.name}");
            go.transform.parent = m.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = new Vector3(1.01f, 1.01f, 1.01f);
            go.transform.localEulerAngles = Vector3.zero;

            var mr = go.AddComponent<MeshRenderer>();

            renderers.Add(mr);

            var mf = go.AddComponent<MeshFilter>();
            mf.mesh = m.mesh;


            mr.sharedMaterials = Enumerable.Repeat(damageFlashMaterial, m.GetComponent<MeshRenderer>().materials.Length).ToArray();
            
            mr.gameObject.SetActive(false);
        });
    }

    public void Damage(float damageAmount)
    {
        StopCoroutine(DamageEffect());
        StartCoroutine(DamageEffect());
        
        curHealth -= damageAmount;

        damageEvent?.Invoke();

        if (curHealth < 0)
        {
            Die();
        }
        
        
    }

    IEnumerator DamageEffect()
    {
        renderers.ForEach(m => m.gameObject.SetActive(true));

        yield return new WaitForSeconds(.1f);
        
        renderers.ForEach(m => m.gameObject.SetActive(false));
    }

    private void Die()
    {
        deathEvent?.Invoke();
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.eulerAngles = explosionRotationOffset;
        Destroy(explosion, 2.0f);
    }
}
