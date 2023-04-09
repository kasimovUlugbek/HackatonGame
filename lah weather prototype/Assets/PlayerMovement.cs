using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Health
{
    public float normalSpeed = 100f, airSpeed = 10;
    private float speed;
    bool moving = false;

    Vector3 direction;
    public float rotationSpeed = 0.3f;

    bool grounded;
    public LayerMask ground;
    public Transform groundCheck;
    public float groundRadius;

    public float normalDrag, airDrag;

    Rigidbody rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, ground);
        if (grounded)
        {
            speed = normalSpeed;
            rb.drag = normalDrag;
        }
        else
        {
            speed = airSpeed;
            rb.drag = airDrag;
        }

        TakeInput();
        if (direction != Vector3.zero)
            moving = true;
        else
            moving = false;

        animator.SetBool("moving", moving);
        animator.SetBool("falling", !grounded);

        if (moving)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(direction.normalized * speed);
    }
    void TakeInput()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horInput, 0, verInput);
    }
}
