using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform[] wheels;

    public CharacterController characterController;
    private Transform groundCheck;
    public LayerMask groupMask;

    public float speed = 12f;
    private float rotationSpeed;
    public float jumpForce = 1000;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float isMoving = 0f;

    private Vector3 velocity;
    private bool isGrounded;

    // for shooting
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    void Start()
    {
        wheels = new Transform[4];
        Transform wheels_transform = gameObject.transform.Find("Wheels");
        wheels[0] = wheels_transform.Find("FrontLeftWheel");
        wheels[1] = wheels_transform.Find("FrontRightWheel");
        wheels[2] = wheels_transform.Find("RearLeftWheel");
        wheels[3] = wheels_transform.Find("RearRightWheel");
        groundCheck = gameObject.transform.Find("GroundCheck");

        rotationSpeed = speed * 50;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        isMoving = moveInput;
        if (moveInput != 0f)
        {
            Moving(moveInput);
        }
        
        Gravity();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Moving(float moveInput)
    {
        Vector3 move = transform.forward * moveInput;
        characterController.Move(move * speed * Time.deltaTime);
        
        RotateWheels(moveInput == 0 ? 0f : moveInput > 0 ? 1f : -1f);
    }

    void RotateWheels(float direction)
    {
        float rotationAngle = direction * rotationSpeed * Time.deltaTime;
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right * rotationAngle);
        }
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groupMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
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
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        float v = bulletSpeed;
        if (isMoving != 0f)
        {
            v += speed;
        }

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = new Vector3(0f, shootDirection.y * v, shootDirection.z * v);
        }
    }
}
