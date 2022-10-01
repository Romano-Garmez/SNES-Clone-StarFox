using Sirenix.OdinInspector;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform playerTransform;

    public float minTimeBetweenShots = .1f;
    public float maxTimeBetweenShots = 5f;

    private float curRandTime = 0;
    
    [AssetsOnly] 
    public GameObject projectilePrefab;

    public Vector3 offsetRotation;
    
    private float timer = 0f;
    private void FixedUpdate()
    {
        transform.LookAt(playerTransform);

        timer += 0.0333333333333f;

        if (timer >= curRandTime)
        {
            Shoot();
            timer = 0;
        }

    }

    void Shoot()
    {
        curRandTime = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        var go = Instantiate(projectilePrefab, transform.position, transform.rotation);
        go.transform.forward = transform.forward + offsetRotation;
    }
}
