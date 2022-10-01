using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3(-5, 0, 0);
    public float moveSpeed = 5;

    public float wrapAroundDistance = 1000f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * (Time.deltaTime * moveSpeed));
        if (transform.position.x < wrapAroundDistance)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
}
