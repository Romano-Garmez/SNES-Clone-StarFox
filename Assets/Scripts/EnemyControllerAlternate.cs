using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyControllerAlternate : MonoBehaviour
{
    [Header("Targeting")]
    public Transform targetPosition;
    public Vector3 targetOffset = Vector3.zero;
    private Vector3 randomOffset = Vector3.zero;

    [Header("Movement")]
    public float maxMoveSpeed = 5f;
    public Vector2 limits = new Vector2(50, 100);

    [Header("AI")]
    public float randomChanceMinTime = 1f;
    public float randomChanceMaxTime = 5f;

    [Header("Scrambling")]
    public Vector2 scrambleValues = new Vector2(5, 5);
    

    [Range(0, 100)]
    public float chanceOfScramble = 50;
    public int scrambleBurstCost = 0;

    public int scrambleBurstAmount = 1;
    public int currentScrambleBursts;
    public float scrambleBurstRecoverTime = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(AILoop());
        currentScrambleBursts = scrambleBurstAmount;
    }

    IEnumerator AILoop()
    {
        
        yield return new WaitForSeconds(Random.Range(randomChanceMinTime, randomChanceMaxTime));

        //Add random offset (scramble)
        if (chanceOfScramble >= Random.Range(0, 100) && currentScrambleBursts > 0)
        {
            randomOffset.y = Random.Range(-scrambleValues.y, scrambleValues.y);
            randomOffset.z = Random.Range(-scrambleValues.x, scrambleValues.x);

            currentScrambleBursts -= scrambleBurstCost;
        }
        else
        //Otherwise dont have an offset
        {
            randomOffset = Vector3.zero;
        }
        
        StartCoroutine(AILoop());
    }

    private float timer = 0;
    private void Update()
    {
        var newPos = targetPosition.position + targetOffset + randomOffset;

        //Clamp newPos to within the bounds of the screen
        newPos.y = Mathf.Clamp(newPos.y, 0, limits.y);
        newPos.z = Mathf.Clamp(newPos.z, -limits.x, limits.x);
        
        //Lerp the transform position 
        transform.position = Vector3.Lerp(transform.position, newPos, maxMoveSpeed * Time.deltaTime);

        //If we have no scramble bursts
        if (currentScrambleBursts <= 0)
        {
            //Increase the timer
            timer += Time.deltaTime;

            //If we are above scramble recover time
            if (timer >= scrambleBurstRecoverTime)
            {
                //Reset the bursts and the timer
                currentScrambleBursts = scrambleBurstAmount;
                timer = 0;
            }
        }
    }
}
