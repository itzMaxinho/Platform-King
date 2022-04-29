using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool doubleJump;

    private void Awake()
    {  
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // obrot postaci podczas chodzenia
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // parametry animacji
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // skakanie
        Jump();

        // poruszanie siê
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // spadanie ze scian
        if (onWall() && !isGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    void Jump()
    {
        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded() || doubleJump)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetTrigger("jump");

                doubleJump = !doubleJump;
            }

            if (Input.GetButtonUp("Jump") && body.velocity.y > 0f)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
                anim.SetTrigger("jump");
            }
        }

        /*else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
            
        }*/
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && !onWall();
    }
}
