using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkeletonSword : MonoBehaviour
{
    private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            damage = Random.Range(1f, 10f);
            collision.GetComponent<PlayerScript>().TakeDamage(damage);
        }
    }
}
