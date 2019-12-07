using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject DeadVersion;
    public float health = 100f;

    void OnCollisionEnter(Collision Col)
    {
        if (Col.collider.CompareTag("Bullet"))
        {
            TakeDamage(101f);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(DeadVersion, transform.position, transform.rotation);
        ScoreSystem.AddScore(10);
        Destroy(gameObject);
    }
}
