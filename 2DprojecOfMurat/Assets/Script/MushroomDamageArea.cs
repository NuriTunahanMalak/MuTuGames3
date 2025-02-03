using UnityEngine;

public class MushroomDamageArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float damage = Random.Range(1f, 10f);
            collision.GetComponent<PlayerScript>().Yavaslama(1f, 1.2f);
            collision.GetComponent<PlayerScript>().TakeDamage(damage);
        }
        if (collision.CompareTag("Enemies"))
        {
            collision.GetComponent<Enemies>().enemiesRb.velocity = new Vector2(collision.GetComponent<Enemies>().enemiesRb.velocity.x/2, collision.GetComponent<Enemies>().enemiesRb.velocity.y / 2);
        }
    }
}
