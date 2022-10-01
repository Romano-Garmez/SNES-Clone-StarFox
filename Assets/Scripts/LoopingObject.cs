using UnityEngine;

public class LoopingObject : MonoBehaviour
{
    public static float maxXValue = 1000f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= maxXValue)
        {
            var tp = transform.position;
            transform.position = new Vector3(0, tp.y, tp.z);
        }
    }
}
