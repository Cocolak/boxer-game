using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Hero2Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Animator animator; 
    [SerializeField] float Speed = 1;
    [SerializeField] float jumpPower = 500;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    private const float groundCheckRadius = 0.2f;

    
    private float horizontalValue;
    private bool isGrounded;
    private bool jump;


    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        //Debug.Log(rigidbody.velocity.y);

        // If we press Jump button we jump, otherwise disable jump
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            jump = true;
        }
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        
        //if (Input.GetKeyDown(KeyCode.J))
            //Attack(1);
        //else if (Input.GetKeyDown(KeyCode.K))
            //Attack(2);
       
    }

    void FixedUpdate()
    {
        GroundCheck();
        // Move(horizontalValue, jump);
    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer); 
        if(colliders.Length > 0)
            isGrounded = true;

        animator.SetBool("isJumping", !isGrounded);
    }

    void Move(float direction, bool jumpFlag)
    {
        #region Jumping

        if (isGrounded && jumpFlag)
        {
            isGrounded = false;
            jumpFlag = false;

            rigidbody.AddForce(new Vector2(0f, jumpPower));
        }

        #endregion

        #region Moving

        float xVal = direction * Time.fixedDeltaTime * Speed * 100;
        Vector2 targetVelocity = new Vector2(xVal, rigidbody.velocity.y);
        rigidbody.velocity = targetVelocity;

        if (direction < 0)
        {
            transform.localScale = new Vector3(-5.7f, 5.4f, 4.8f);
        } 
        else if (direction > 0)
        {
            transform.localScale = new Vector3(5.7f, 5.4f, 4.8f);
        }

        animator.SetFloat("HorSpeed", Mathf.Abs(rigidbody.velocity.x));
        animator.SetFloat("VerSpeed", rigidbody.velocity.y);

        #endregion
    }

    void Attack(int num)
    {
        animator.SetFloat("Attack", num);
        animator.SetTrigger("isAttacking");
    }

}
