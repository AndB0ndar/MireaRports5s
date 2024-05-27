using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrop : MonoBehaviour
{
    [SerializeField] public GameObject ballPrefab;
    public Transform dropPoint;
    public float dropInterval = 1f;

    private float timer = 0f;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= dropInterval)
        {
            DropBall();
            timer = 0f;
        }
    }

    void DropBall()
    {
        Instantiate(ballPrefab, dropPoint.position, Quaternion.identity);
    }
}
