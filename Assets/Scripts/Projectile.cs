using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float moveSpeed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * (moveSpeed * Time.deltaTime), Space.World);

        if (transform.position.x < -50)
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
    }
}
