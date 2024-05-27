using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public int damageAmount = 50;
    public float dead_y = -1f;

    void Update()
    {
        if (transform.position.y <= dead_y)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            Debug.Log("Player " + playerHealth);
            if (playerHealth != null)
            {
                Debug.Log("Player!!!");
                playerHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
