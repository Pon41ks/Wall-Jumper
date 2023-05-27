using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    private Rigidbody2D rb;
    private bool isGround;
    private bool onLeftWall;
    private bool onRightWall;
    private bool isWallsliding;
    private bool isWallJumping;
    private float wallJUmpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuraction = 0.4f;
    private float wallJumpingDirection;
    private Vector2 wallJumpingPower = new Vector2 (8f, 5f);

    public Transform wallLeftCheck;
    public Transform groundCheck;
    public Transform wallRightCheck;
    public float wallRadius = 0.3f;
    private float wallSlidingSpeed = 10f;
    [SerializeField]private LayerMask whatIsground;
    [SerializeField]private LayerMask thatsLeftWall;
    [SerializeField]private LayerMask thatsRightWall;


    private Vector2 leftJump = new Vector2(-1, 1);
    private Vector2 rightJump = new Vector2(1, 1);
    private Vector2 jump = new Vector2(0, 1);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onLeftWall = false;
        onRightWall = false;
    }
    private void FixedUpdate()
    {
        onRightWall = Physics2D.OverlapCircle(wallRightCheck.position, wallRadius, thatsRightWall);
        onLeftWall = Physics2D.OverlapCircle(wallLeftCheck.position, wallRadius, thatsLeftWall);
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsground);
        
    }


    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }

    }

     if (onLeftWall && !isGround)
        {
            rb.velocity = rightJump * jumpForce;

        }
        if (onRightWall && !isGround)
        {
            rb.velocity = leftJump * jumpForce;

        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    */
    
    private void WallSlide()
    {
        Debug.Log(onRightWall);
        if (onRightWall && !isGround)
        {
            Debug.Log("asd");
            isWallsliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            
        }
        else
        {
            isWallsliding = false;
        }
    }
    private void WallJump ()
    {
        if (isWallsliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJUmpingTime;
            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; 
        }
        if(wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

        }
        if(transform.localScale.x != wallJumpingDirection)
        {
             Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        Invoke(nameof(StopWallJump), wallJumpingDirection);
    }
    private void StopWallJump()
    {
        isWallJumping = false;
    }


    public void Onclick()
    {
        
        if (isGround)
        {
            rb.velocity = rightJump * jumpForce;
            
            
        }
        WallSlide();
        WallJump();
        




       



    }
}
