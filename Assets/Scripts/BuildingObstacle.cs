using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObstacle : MonoBehaviour
{
    public float wrapAroundDistance = -2000f;
    public Vector3 moveDirection = new Vector3(5, 0, 0);
    public float moveSpeed = -25;

    public int randomMinZ = -150;
    public int randomMaxZ = 150;

    public float damage = 5f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * (Time.deltaTime * moveSpeed));
        if (transform.position.x < wrapAroundDistance)
        {
            transform.position = new Vector3(2000, transform.position.y, Random.Range(randomMinZ, randomMaxZ));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //ignore collision if not player
        if (collision.gameObject.tag == "Player")
        {
            var go = collision.transform.gameObject;
            if (go.TryGetComponent(out Health hp))
            {
                hp.Damage(damage);
            }
        }
    }
}
