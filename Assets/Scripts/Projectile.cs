using Assets.Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger teste");
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("Tag teste");
            collision.GetComponent<EnemyMovement>().Explode();
            Destroy(gameObject); // Destrói o laser após colidir

        }
    }
}
