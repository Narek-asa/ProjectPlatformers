using UnityEditor.Il2Cpp;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;
    [SerializeField]private float slideSpeed = 1f;
    

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;


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


        //flips player left and right input
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //wall Jump logic
        if (wallJumpCooldown > 0.2f)
        {

            if (!(onWall() && !isGrounded()))
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }

            if (onWall() && !isGrounded())
            {
                body.velocity = new Vector2(0, body.velocity.y);
                body.velocity = Vector2.down * slideSpeed;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space))
            {

                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
                

        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    //Jump Method
    private void Jump()
    {
        if (isGrounded())
        {
            
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, jumpPower * 1.3f);
            transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),
                                                        transform.localScale.y,
                                                        transform.localScale.z);
            wallJumpCooldown = 0;
        }
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
