 using UnityEngine;

 public class PlaneControllerAlternate : MonoBehaviour
{
    [Header("Ship")]
    public Transform shipPivot;
    public float rollAmount = 20f;
    public float yawAmount = 20f;
    public float pitchAmount = 20f;
    
    
    public float barrelRollAmount = 90f;
    public float rollClamp = 90f;
    public float rollZFac = 1.2f;
    public float rolledPitchClamp = .4f;
    
    [Header("Movement")] 
    public Vector3 forwardVector;
    public float shipForwardSpeed = 1f;
    public float zMoveSpeed = 1;
    public float yMoveSpeed;

    [Header("Clamping")]
    public float zClamp = 50;
    public float yClamp = 50;
    public float lerpSpeed = 1f;
    
    private float zFac = 1;
    private float yFac = 1;

    // Update is called once per frame
    void Update()
    {
        //Input
        float zInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        var roll = Input.GetAxis("Roll");
        float tiltValue = roll * barrelRollAmount;

        if (roll >= .4f || roll <= -.4f)
        {
            zInput *= rollZFac;
            yInput = Mathf.Clamp(yInput, -rolledPitchClamp, rolledPitchClamp);
        }

        //This is the Vector3 for consistent forward movement
        var forwardMovement = (forwardVector * shipForwardSpeed);
        
        //Increase position by [forward movement]                [ Player input for y ]  [ Player movement for z ]   
        //transform.position += forwardMovement + new Vector3(0, yInput * yMoveSpeed, zInput * -zMoveSpeed) * Time.deltaTime;
        transform.Translate((forwardMovement + new Vector3(0, yInput * yMoveSpeed, zInput * -zMoveSpeed)) * Time.deltaTime);
        
        //Quick cache here
        var tPos = transform.position;
        
        //Clamp the player position               [     Clamp Y position from 0 to yClamp   ]   [       Clamp the z values (left to right)       ]
        transform.position = new Vector3(tPos.x, Mathf.Clamp(tPos.y, 0, yClamp), Mathf.Clamp(tPos.z, -zClamp, zClamp));

        //If we just clamped the y value (we are at top or bottom border) set yFac accordingly
        var yBool = transform.position.y >= yClamp || transform.position.y <= 0;
        LerpValue(ref yFac, !yBool);

        //If we just clamped the z value (we are at left or right border) set zFac accordingly
        var zBool = transform.position.z >= zClamp || transform.position.z <= -zClamp;
        LerpValue(ref zFac, !zBool);

        //Actual ship rotation happens here     [The roll is done here]                                                                    [Yaw is done here]                 [Pitch is done here]
        shipPivot.rotation = Quaternion.Euler(Mathf.Clamp((zInput * rollAmount * zFac + tiltValue), -rollClamp, rollClamp),  180 + zInput * yawAmount * zFac, yInput * -pitchAmount * yFac);
    }

    /// <summary>
    /// Lerp value by reference between 0 and 1 based on the bool boolVal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="boolVal"></param>
    private void LerpValue(ref float value, bool boolVal)
    {
        //C# doesnt let you convert bool to int so you gotta do it yourself
        int nVal = 0;
        if (boolVal)
        {
            nVal = 1;
        }
        
        //Lerp the value 
        value = Mathf.Lerp(value, nVal, lerpSpeed * Time.deltaTime);
    }
}
