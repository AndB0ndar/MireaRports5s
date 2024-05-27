using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        targetPosition.x = firePoint.position.x;

        Vector3 shootDirection = targetPosition - firePoint.position;
        shootDirection.x = 0f;
        shootDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = new Vector3(0f, shootDirection.y * bulletSpeed, shootDirection.z * bulletSpeed);
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        }
    }
}
