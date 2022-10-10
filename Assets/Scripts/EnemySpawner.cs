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

    public float timeBetweenSpawns = 2f;

    public int maxConcurrentEnemies = 1;

    public Transform playerTarget;
    public Transform playerCollider;


    private void Start()
    {
        StartCoroutine(SpawnLogic());
    }


    private IEnumerator SpawnLogic()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        
        //If we dont have the max amount of players spawned
        if (activeEnemies.Count <= maxConcurrentEnemies)
        {

            AddRandomEnemy();
            
        }
        
        StartCoroutine(SpawnLogic());
    }

    public void AddRandomEnemy()
    {
        //Index of the enemy prefab
        int value = Mathf.RoundToInt(Random.Range(0.0f, (float) enemyPrefabs.Count - 1));
        
        //Instantiate the enemy prefab
        var go = Instantiate(enemyPrefabs[value], transform.position, enemyPrefabs[value].transform.rotation);
        
        //Get the hp and add it to active enemies as well as add the remove event
        var hp = go.GetComponentInChildren<Health>();
        activeEnemies.Add(hp);
        hp.deathEvent.AddListener(() => RemoveEnemy(hp));

        //If its got an enemy controller, set its target position to the player
        if (hp.TryGetComponent(out EnemyControllerAlternate eca))
        {
            eca.targetPosition = playerTarget;
        }

        //If its got turrets, set their playerTransform (target) to the player
        var turrets = hp.GetComponentsInChildren<TurretController>();
        if (turrets != null)
        {
            turrets.ForEach(x => x.playerTransform = playerCollider);
        }
    }


    /// <summary>
    /// Removes the enemy from the activeEnemies list
    /// </summary>
    /// <param name="enemyHp"></param>
    public void RemoveEnemy(Health enemyHp)
    {
        activeEnemies.Remove(enemyHp);
    }
}
