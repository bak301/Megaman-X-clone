using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle = 0,
    Run = 1,
    Jump = 2,
    Dash = 3,
    AirDash = 4,
    IdleShoot = 10,
    RunShoot = 11,
    JumpShoot = 12,
    DashShoot = 13,
    Damaged = 20
}

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D map;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dashCooldownOrigin;
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float baseSpeed;
    [SerializeField] private GroundCheck groundCheck;
    private Rigidbody2D player;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Jump") > 0 && isGrounded == true)
        {
            player.velocity = Vector2.up * jumpSpeed;
            animator.Play("jump");
            isGrounded = false;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            float side = Mathf.Sign(Input.GetAxis("Horizontal"));
            Debug.Log($"Input horizontal {side}");
            this.transform.eulerAngles = new Vector3(0, 90 - 90 * side, 0);
            this.transform.Translate(Time.deltaTime * side * baseSpeed, 0, 0, Space.World);
            animator.Play("run");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = UpdateGroundedStatus();
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = UpdateGroundedStatus();
    }

    // Custom functions

    private bool UpdateGroundedStatus()
    {
        return groundCheck.isGrounded;
    }
    private void SetAnimation()
    {
        if (isGrounded == false)
        {
            animator.Play("jumping");
        }
        else if (Mathf.Abs(player.velocity.x) > 0)
        {
            if (Mathf.Abs(player.velocity.x) > baseSpeed * 1.2f)
            {
                animator.Play("dash");
            }
            else if (Mathf.Abs(player.velocity.x) > 0)
            {
                animator.Play("running");
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
