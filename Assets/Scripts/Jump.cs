using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    private Rigidbody2D rb;
    private bool isWallsliding;
    private bool isWallJumping;
    private float wallJUmpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private float wallJumpingDirection;
    [SerializeField] private float wallSlidingSpeed = 10f;
    [SerializeField] private float runSpeed = 10f;
    private Vector2 wallJumpingPower = new Vector2 (8f, 5f);


    private int facingDirection;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.3f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;


    private Vector2 leftJump = new Vector2(-1, 1);
    private Vector2 rightJump = new Vector2(1, 1);
    private Vector2 jump = new Vector2(0, 1);

    public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    public bool WallFront => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer);
    public bool WallBack => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, wallCheckDistance, groundLayer);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        facingDirection = 1;
    }
    private void Update()
    {
        if (Ground) MoveState();
    }
    private void MoveState()
    {
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
    }
    private void WallSlide()
    {
        if (WallFront && !Ground)
        {
            isWallsliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallsliding = false;
        }
    }
    private void WallJump()
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
        
        if (Ground)
        {
            rb.velocity = rightJump * jumpForce;
        }
        WallSlide();
        WallJump();
    }
}