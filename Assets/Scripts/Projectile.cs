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
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);

        if (transform.position.x < minX || transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var go = collision.transform.gameObject;
        Debug.Log(go.name);
        if (go.TryGetComponent(out Health hp))
        {
            hp.Damage(damage);
        }
        
        Destroy(this.gameObject);
    }
}
