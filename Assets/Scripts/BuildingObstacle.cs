using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObstacle : MonoBehaviour
{
    //when building warps to end
    public float wrapAroundDistance = 2000f;
    //direction to move building
    public Vector3 moveDirection = new Vector3(5, 0, 0);
    public float moveSpeed = -25;
    //how far left/right to place building
    public int randomMinZ = -150;
    public int randomMaxZ = 150;
    //how much damage building does
    public float damage = 5f;

    void Start()
    {
        //randomize z position to randomly place building in playable area
        transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(randomMinZ, randomMaxZ));
    }

    // Update is called once per frame
    void Update()
    {
        //constantly move building towards player
        transform.Translate(moveDirection * (Time.deltaTime * moveSpeed));
        //if building reaches certain point, warp to end of map
        if (transform.position.x < (-1/2 * wrapAroundDistance))
        {
            transform.position = new Vector3(wrapAroundDistance, transform.position.y, Random.Range(randomMinZ, randomMaxZ));
        }

    }

    //if player collides with building, remove some health
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
