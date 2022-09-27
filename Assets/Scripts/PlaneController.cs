using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 10;
    public float rotationSpeedLR = 10;
    public float verticalInput = 10;
    public float horizontalInput = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical/horizontal input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate (model is rotated right, so moving left cancels out)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * verticalInput);

        // tilt the plane left/right based on left/right arrow keys
        transform.Rotate(Vector3.right * rotationSpeedLR * Time.deltaTime * horizontalInput);
    }
}
