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

    private void InitializeDamageFlash()
    {
        //For each assigned message
        meshes.ForEach(m =>
        {
            //Make a new gameObject with the name, as well as set all of its positions
            var go = new GameObject($"DamageFlash{m.sharedMesh.name}");
            go.transform.parent = m.transform;
            go.transform.localPosition = Vector3.zero;
            
            //This is to prevent z fighting
            go.transform.localScale = new Vector3(1.01f, 1.01f, 1.01f);
            go.transform.localEulerAngles = Vector3.zero;

            //Add a mesh renderer and a mesh filter
            var mr = go.AddComponent<MeshRenderer>();
            var mf = go.AddComponent<MeshFilter>();

            //Add the mesh enderer to the renderers list
            renderers.Add(mr);

            //Assign the mesh
            mf.mesh = m.mesh;

            //Set all of the mesh render materials to be all damage flash materials
            mr.sharedMaterials = Enumerable.Repeat(damageFlashMaterial, m.GetComponent<MeshRenderer>().materials.Length).ToArray();
            
            //Disable the flash
            mr.gameObject.SetActive(false);
        });
    }

    
    public void Damage(float damageAmount)
    {
        //Stop the coroutine first then run it to prevent goofiness
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
        //Enable all the renderers 
        renderers.ForEach(m => m.gameObject.SetActive(true));

        //Wait a bit
        yield return new WaitForSeconds(.1f);
        
        //Disable all the renderers
        renderers.ForEach(m => m.gameObject.SetActive(false));
    }

    private void Die()
    {
        deathEvent?.Invoke();
        
        //Instantiate the explosion prefab
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.eulerAngles = explosionRotationOffset;
        Destroy(explosion, 2.0f);
    }
}
