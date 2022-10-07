using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [AssetsOnly] public List<GameObject> enemyPrefabs = new();

    [ReadOnly] 
    public List<Health> activeEnemies = new();

    public float yMax = 100;
    public float xBound = 100;


    [Range(0, 100)]
    public float chanceOfSpawningMultiple = 50f;

    public Transform playerTarget;
    public Transform playerCollider;


    private void Start()
    {
        StartCoroutine(SpawnLogic());
    }


    private WaitForSeconds wfs = new(.1f);
    private IEnumerator SpawnLogic()
    {
        yield return wfs;
        
        if (activeEnemies.Count == 0)
        {

            AddRandomEnemy();
            
        }
        
        StartCoroutine(SpawnLogic());
    }

    public void AddRandomEnemy()
    {
        int value = Mathf.RoundToInt(Random.Range(0.0f, (float) enemyPrefabs.Count - 1));
        var go = Instantiate(enemyPrefabs[value], transform.position, enemyPrefabs[value].transform.rotation);
        var hp = go.GetComponentInChildren<Health>();
        activeEnemies.Add(hp);
        hp.deathEvent.AddListener(() => RemoveEnemy(hp));

        if (hp.TryGetComponent(out EnemyControllerAlternate eca))
        {
            eca.targetPosition = playerTarget;
        }

        var turrets = hp.GetComponentsInChildren<TurretController>();
        if (turrets != null)
        {
            turrets.ForEach(x => x.playerTransform = playerCollider);
        }
    }


    public void RemoveEnemy(Health enemyHp)
    {
        activeEnemies.Remove(enemyHp);
    }
}
