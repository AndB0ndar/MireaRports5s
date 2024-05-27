using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMob : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 1f;
    public float amplitude = 1f;
    public float frequency = 1f;
    public float deadline = 100f;

    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        float sinOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        Vector3 move = transform.forward * speed;
        move.y = sinOffset;

        characterController.Move(move * Time.deltaTime);

        if (transform.position.z >= deadline)
        {
            Destroy(gameObject);
        }
    }
}

