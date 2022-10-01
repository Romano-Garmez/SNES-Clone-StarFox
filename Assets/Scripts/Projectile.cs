using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);

        if (transform.position.x < -50)
        {
            Destroy(this.gameObject);
        }
    }
}
