using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [AssetsOnly]
    public GameObject projectilePrefab;

    public Vector3 offsetRotation;

    public KeyCode shootKey = KeyCode.Space;

    public float timeBetweenShots = 1f;

    private float curTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (curTime < timeBetweenShots)
        {
            curTime += Time.deltaTime;
        }
        
        if (Input.GetKey(shootKey))
        {
            if (curTime >= timeBetweenShots)
            {
                Shoot();
                curTime = 0;
            }
        }
    }
    
    void Shoot()
    {
        var go = Instantiate(projectilePrefab, transform.position, transform.rotation);
        go.transform.eulerAngles = transform.eulerAngles + offsetRotation;
    }
}
