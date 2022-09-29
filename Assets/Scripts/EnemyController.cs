using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10;
    public int worldPosReset = 1000;
    private int yOffset;
    private int zOffset;

    public Transform targetObject;

    void Start()
    {
        InvokeRepeating("ChangeOffset", 2.0f, 3.0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(targetObject.position + new Vector3(targetObject.transform.position.x, yOffset, zOffset));
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        //reset at end of world (looks like world goes on forever)
        if (transform.position.x > worldPosReset)
        {
            transform.position =
                new Vector3(0, transform.position.y, transform.position.z);
        }

        // move the plane forward at a constant rate (model is rotated right, so moving left cancels out)
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void ChangeOffset()
    {
        yOffset = Random.Range(-50, 50);
        zOffset = Random.Range(-50, 50);
    }
}