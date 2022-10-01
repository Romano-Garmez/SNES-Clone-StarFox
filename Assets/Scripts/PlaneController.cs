using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 10;
    public float controlRotationSpeed = 50;
    public float rotationBackSpeed = 10;
    public int worldPosReset = 1000;

    public int maxHeight = 100;
    public int maxDistanceLeftRight = 100;
    public bool invertedVerticalControl = false;

    private Vector3 originalRotation;
    private Quaternion endRot;

    void Start()
    {
        //save some important starting info
        originalRotation = transform.position;
        originalRotation.y = 180;
        endRot = Quaternion.Euler(originalRotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //reset at end of world (looks like world goes on forever)
        if (transform.position.x > worldPosReset)
        {
            transform.position =
                new Vector3(0, transform.position.y, transform.position.z);
        }

        //Clamping is done here
        var tp = transform.position;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(tp.y, 0, maxHeight),
            Mathf.Clamp(tp.z, -maxDistanceLeftRight, maxDistanceLeftRight));

        //When no keys are pressed, move back to facing directly to end of stage
        if (!Input.anyKey)
        {
            Quaternion startRot = transform.rotation;
            if (transform.rotation != endRot)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, Time.deltaTime * rotationBackSpeed);
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

        // move the plane forward at a constant rate (moving on x axis)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, verticalInput * Time.deltaTime * controlRotationSpeed), Space.World);

        // tilt the plane left/right based on left/right arrow keys
        transform
            .Rotate((Vector3.up *
            controlRotationSpeed *
            Time.deltaTime *
            horizontalInput), Space.World);

        //We do a little bit of rotation clamping, its called we do a little rotation clamping

        Vector3 tRot = transform.localEulerAngles;
        //xClamping is funny and can probably just be locked to 0 degrees always?
        //float cXVal = Mathf.Clamp(tRot.x, -45, 45);
        float cYVal = Mathf.Clamp(tRot.y, 135, 225);

        //Clamping only selects having a range the numbers can be in, rather than a range they can't be in, so we need to do this manually as far as I can tell
        //float cZVal = Mathf.Clamp(tRot.z, -45, 45);
        float cZVal = tRot.z;
        //if facing down, set 45
        if (tRot.z > 45 && tRot.z < 180)
        {
            cZVal = 45;
        }
        //if facing up, set -45
        else if (tRot.z > 180 && tRot.z < 315)
        {
            cZVal = -45;
        }
        //TODO: imma write my own method to do this properly when I get a chance. You can do it if you get a chance before I do


        //transform.eulerAngles = new Vector3(cXVal, cYVal, cZVal);
        transform.rotation = Quaternion.Euler(new Vector3(0, cYVal, cZVal));
    }
}