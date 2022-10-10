using Sirenix.OdinInspector;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform playerTransform;

    public Transform shootPos;

    public float minTimeBetweenShots = .1f;
    public float maxTimeBetweenShots = 5f;

    private float curRandTime = 0;

    public Vector3 rotationOffset;
    
    [AssetsOnly] 
    public GameObject projectilePrefab;

    public Vector3 offsetRotation;
    
    private float timer = 0f;
    
    //We do this in fixed update because I said so
    private void FixedUpdate()
    {
        //Look at the player and add the rotation offset
        transform.LookAt(playerTransform);
        transform.eulerAngles += rotationOffset;

        //Increment the timer by fixedTime
        timer += Time.fixedTime;

        //Do random shooting here
        if (timer >= curRandTime)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        //Set the curRandTime to a random value
        curRandTime = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        //Instantiate the bullet and set its forward
        var go = Instantiate(projectilePrefab, shootPos.position, transform.rotation);
        go.transform.forward = transform.forward + offsetRotation;
    }
}
