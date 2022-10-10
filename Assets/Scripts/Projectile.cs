using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float moveSpeed = 1f;

    public float minX = -50;

    public float maxX = 1000;
    // Update is called once per frame
    void Update()
    {
        //Move forward
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);

        //if out of bounds, destroy the object
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Get the hp
        if (collision.transform.TryGetComponent(out Health hp))
        {
            //Damage it
            hp.Damage(damage);
        }
        
        //Destroy this
        Destroy(this.gameObject);
    }
}
