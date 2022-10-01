using UnityEngine;

public class CameraFollowAlternate : MonoBehaviour
{
    public Transform other;
    public Vector3 offset = new Vector3(-10, 0, 0);

    public float divisionFactor = 2f;
    
    //We do this OnPreRender as it stops any jittering
    void OnPreRender()
    {
        var op = other.position;
        transform.position = new Vector3(op.x, op.y / divisionFactor, op.z / divisionFactor) + offset;
        transform.LookAt(other.position);

    }
}
