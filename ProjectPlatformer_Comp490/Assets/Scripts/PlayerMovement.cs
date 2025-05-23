using UnityEditor.Il2Cpp;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [Header("Movement Parameters")]
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;

    [Header("Layers")]
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;

    [Header("CoyoteTime")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Gravity Settings")]
    [SerializeField] private float normalGravityScale = 4f;
    [SerializeField] private float wallJumpGravityScale = 2f;
    [SerializeField] private float gravityRestoreTime = 0.2f;
    private float gravityRestoreTimer = 0f;
    private bool isWallJumping = false;

    [Header("Background Slide Speed")]
    [SerializeField]private float slideSpeed = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Extra Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        //Grab references from rigid body and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

private void Update()
{
    horizontalInput = Input.GetAxis("Horizontal");

    // Flip player based on input
    if (horizontalInput > 0.01f)
        transform.localScale = Vector3.one;
    else if (horizontalInput < -0.01f)
        transform.localScale = new Vector3(-1, 1, 1);

    anim.SetBool("Run", horizontalInput != 0);
    anim.SetBool("grounded", isGrounded());

    if (Input.GetKeyDown(KeyCode.Space))
        Jump();

    if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

    // Wall sticking
    if (onWall() && !isWallJumping)
    {
        body.gravityScale = 0;
        body.velocity = Vector2.zero;
    }
    else
    {
        if (isWallJumping)
        {
            gravityRestoreTimer += Time.deltaTime;
            body.gravityScale = Mathf.Lerp(wallJumpGravityScale, normalGravityScale, gravityRestoreTimer / gravityRestoreTime);
            if (gravityRestoreTimer >= gravityRestoreTime)
            {
                isWallJumping = false;
                body.gravityScale = normalGravityScale;
            }
        }
        else
        {
            // Only update horizontal velocity from input when NOT wall jumping.
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            body.gravityScale = normalGravityScale;
        }

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }
}

    //Jump Method
    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0)
            return;

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded() || coyoteCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else if (jumpCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                jumpCounter--;
            }
            coyoteCounter = 0;
        }
    }


    private void WallJump()
    {
        float desiredDirection = -Mathf.Sign(transform.localScale.x);
        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY);

        transform.localScale = new Vector3(desiredDirection, transform.localScale.y, transform.localScale.z);

        // Immediately use a lower gravity to create floatiness
        body.gravityScale = wallJumpGravityScale;

        isWallJumping = true;
        gravityRestoreTimer = 0f;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }


    private bool onWall()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 origin = boxCollider.bounds.center + new Vector3(direction.x * (boxCollider.bounds.extents.x + 0.05f), 0, 0);
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, 0.1f, wallLayer);
        Debug.DrawRay(origin, direction * 0.1f, raycastHit.collider != null ? Color.green : Color.red);

        if (raycastHit.collider != null)
        {
            Debug.Log($"On Wall: {raycastHit.collider.name} | Layer: {LayerMask.LayerToName(raycastHit.collider.gameObject.layer)}");
        }

        return raycastHit.collider != null;
    }



}
