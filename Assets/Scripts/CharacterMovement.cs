using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    bool speedBoostActive = false;
    float speedBoostTimer = 0f;
    float originalRunSpeed;

    bool jumpBoostActive = false;
    float jumpBoostTimer = 0f;

    public float minJumpForce = 5f;
    public float maxJumpForce = 12f;
    public float maxChargeTime = 3f;
    public float maxJumpChargeTime = 1.5f;

    private Rigidbody rb;
    private bool isGrounded = true;

    private CharacterAnimator anim;

    
    private float jumpChargeTime = 0f;
    private bool chargingJump = false;

    private bool doubleJumpEnabled = false;
    private int jumpsRemaining = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<CharacterAnimator>();
        jumpsRemaining = 1;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        HandleJumpCharge();

        anim.isGrounded = isGrounded;

        HandleSpeedBoost();
        HandleJumpBoost();
    }

    void Move()
    {
        float speed = walkSpeed;
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            moveInput = 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            moveInput = 1f;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        anim.moveInput = moveInput;
    }

    void HandleJumpCharge()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            chargingJump = true;
        }

        if (chargingJump && Input.GetKey(KeyCode.Space))
        {
            jumpChargeTime += Time.deltaTime;

            if (jumpChargeTime >= maxChargeTime)
            {
                Jump();
            }
        }

        if (chargingJump && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        float chargePercent = Mathf.Clamp01(jumpChargeTime / maxJumpChargeTime);
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, chargePercent);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpsRemaining--;

        isGrounded = false;
        chargingJump = false;
        jumpChargeTime = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            jumpsRemaining = doubleJumpEnabled ? 2 : 1;
        }
    }

    public void ActivateSpeedBoost(float duration)
    {
        originalRunSpeed = runSpeed;
        runSpeed *= 2;

        speedBoostActive = true;
        speedBoostTimer = duration;
    }

    public void ActivateJumpBoost(float duration)
    {
        jumpBoostActive = true;
        jumpBoostTimer = duration;

        doubleJumpEnabled = true;
        jumpsRemaining = 2;
    }

    void HandleSpeedBoost()
    {
        if (speedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;

            if (speedBoostTimer <= 0)
            {
                runSpeed = originalRunSpeed;
                speedBoostActive = false;
            }
        }
    }

    void HandleJumpBoost()
    {
        if (jumpBoostActive)
        {
            jumpBoostTimer -= Time.deltaTime;

            if (jumpBoostTimer <= 0)
            {
                jumpBoostActive = false;

                if (isGrounded)
                    jumpsRemaining = 1;
                else if (jumpsRemaining > 1)
                    jumpsRemaining = 1;
            }
        }
    }
}