using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 10;

    public float rotationSpeed = 10;

    public float rotationSpeedLR = 10;

    public float xReset = 50;

    public bool invertedVerticalControl = false;

    private float verticalInput;

    private float horizontalInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Transform>().position.x > xReset)
        {
            transform.position =
                new Vector3(0, transform.position.y, transform.position.z);
        }

        // get the user's vertical/horizontal input
        if (invertedVerticalControl == true)
        {
            verticalInput = -1 * (Input.GetAxis("Vertical"));
        }
        else
        {
            verticalInput = (Input.GetAxis("Vertical"));
        }

        horizontalInput = Input.GetAxis("Horizontal");

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
            .Rotate(Vector3.right *
            rotationSpeedLR *
            Time.deltaTime *
            horizontalInput);
    }
}
