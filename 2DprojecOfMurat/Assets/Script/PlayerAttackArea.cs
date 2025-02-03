using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{

    public float givedamage = 30f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {

            collision.GetComponent<Enemies>().TakeHit(givedamage);
        }
    }
}
