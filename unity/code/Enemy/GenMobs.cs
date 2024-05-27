using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMobs : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Vector3 point = Vector3.zero;
    private int max_count_mobs = 2;

    public float dropInterval = 5f;
    public Vector3 spawnPoint = new Vector3(0, 13, -10);
    private float timer = 0f;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= dropInterval)
        {
            bool f_spawn = GameObject.FindGameObjectsWithTag(prefab.tag).Length < max_count_mobs;
            f_spawn &= Random.Range(0f, 1f) <= 0.1;
            if (f_spawn)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    private void Spawn()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Vector3 randomPosition = point + spawnPoint;
        Instantiate(prefab, randomPosition, rotation);
    }
}