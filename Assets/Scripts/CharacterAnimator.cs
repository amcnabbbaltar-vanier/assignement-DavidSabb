using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float moveInput;
    public bool isGrounded;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        // Update movement speed
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Update grounded state
        animator.SetBool("isGrounded", isGrounded);
    }

    public void TriggerDoubleJump()
    {
        animator.SetTrigger("DoubleJump");
    }
}