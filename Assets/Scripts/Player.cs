using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D map;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dashCooldownOrigin;
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float baseSpeed;
    private float dashCooldown;
    private Rigidbody2D player;
    private bool leftCollide = false;
    private bool rightCollide = false;
    private string[] keyNames;
    private bool jumpable = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        keyNames = new string[]{ "x", "left", "right", "c"};
    }


    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetAnimation();
        dashCooldown -= Time.fixedDeltaTime;
        float horizontalSpeed = Mathf.Abs(player.velocity.x) < baseSpeed + 1 ? baseSpeed : Mathf.Abs(player.velocity.x);

        if (Input.GetKey(keyNames[1]))
        {
            player.velocity = new Vector2(-1 * horizontalSpeed, player.velocity.y);
            animator.transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (Input.GetKey(keyNames[2]))
        {
            player.velocity = new Vector2(1 * horizontalSpeed, player.velocity.y);
            animator.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(keyNames[0]) && jumpable == true)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        if (Input.GetKey(keyNames[3]) && dashCooldown <= 0)
        {
            var rotation = player.gameObject.transform.eulerAngles;
            player.velocity = new Vector2(Mathf.Sign(90 - rotation.y) * baseSpeed * dashMultiplier, player.velocity.y);
            dashCooldown = dashCooldownOrigin;
        }
    }

    private void SetAnimation()
    {
        if (jumpable == false && Mathf.Abs(player.velocity.x) <= baseSpeed)
        {
            animator.Play("jump");
        }
        else if (Mathf.Abs(player.velocity.x) > 0)
        {
            if (Mathf.Abs(player.velocity.x) > baseSpeed * 1.2f)
            {
                animator.Play("dash");
            }
            else if (Mathf.Abs(player.velocity.x) > 0)
            {
                animator.Play("run");
            }
        }
        else
        {
            animator.Play("idle");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "map")
        {
            jumpable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "map")
        {
            jumpable = false;
        }
    }

}
