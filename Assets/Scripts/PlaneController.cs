using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 50;
    public int worldPosReset = 1000;

    public int maxHeight = 100;
    public int maxDistanceLeftRight = 100;
    public bool invertedVerticalControl = false;

    private float originalRotationX;
    private float originalRotationY;
    private float originalRotationZ;

    private Quaternion endRot;



    void Start()
    {
        originalRotationX = GetComponent<Transform>().position.x;
        originalRotationY = GetComponent<Transform>().position.y;
        originalRotationZ = GetComponent<Transform>().position.z;
        endRot = new Quaternion(originalRotationX, originalRotationY, originalRotationZ, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //reset at end of world (looks like world goes on forever)
        if (GetComponent<Transform>().position.x > worldPosReset)
        {
            transform.position =
                new Vector3(0, transform.position.y, transform.position.z);
        }

        //prevent from going to far left/right
        if (GetComponent<Transform>().position.z > maxDistanceLeftRight)
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y, maxDistanceLeftRight);
        }
        if (GetComponent<Transform>().position.z < (-1 * maxDistanceLeftRight))
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y, -maxDistanceLeftRight);
        }

        //prevent from going to high or too low
        if (GetComponent<Transform>().position.y > maxHeight)
        {
            transform.position =
                new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        if (GetComponent<Transform>().position.y < 0)
        {
            transform.position =
                new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (!Input.anyKey)
        {
            //TODO: if no input, smoothly rotate to original rotation
            Quaternion startRot = GetComponent<Transform>().rotation;
            if (transform.rotation != endRot)
            {
                Debug.Log("not same");
                transform.rotation = Quaternion.Slerp(startRot, endRot, Time.time * rotationSpeed);
            }
        }

        // get the user's vertical/horizontal input
        float verticalInput;
        float horizontalInput = Input.GetAxis("Horizontal");
        if (invertedVerticalControl == true)
        {
            verticalInput = -1 * (Input.GetAxis("Vertical"));
        }
        else
        {
            verticalInput = (Input.GetAxis("Vertical"));
        }

        // move the plane forward at a constant rate (model is rotated right, so moving left cancels out)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform
            .Rotate(Vector3.forward *
            rotationSpeed *
            Time.deltaTime *
            verticalInput *
            -1);

        // tilt the plane left/right based on left/right arrow keys
        transform
            .Rotate(Vector3.up *
            rotationSpeed *
            Time.deltaTime *
            horizontalInput);
    }
}
