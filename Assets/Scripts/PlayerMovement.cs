using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    //private variables
    Rigidbody2D rb = null;
    Vector2 dir = Vector2.zero;
    Animator animator = null;
    bool isGrounded = false;

    //serialized variable
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 0f;
    [SerializeField] float checkRadius = 0f;
    [SerializeField] LayerMask groundLayer = 0;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] Transform particalaTransform = null;
    [SerializeField] ParticleSystem dirtParticles = null;
    #endregion
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from Player
        dir = new Vector2
        {
            x = Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            y = 0f
        }.normalized;

        //checking for player to be on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //setting the animation
        animator.SetFloat("Speed", Mathf.Abs(dir.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Velocity", rb.velocity.y);


        //fliping the player
        if (dir.x < 0)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            particalaTransform.localScale = -Vector2.one;
        }
        else if (dir.x > 0)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            particalaTransform.localScale = Vector2.one;
        }
    }

    private void FixedUpdate()
    {
        //making player jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpPower;
        }

        if (Mathf.Abs(dir.x) > 0 && isGrounded)
        {
            dirtParticles.Play();
        }
        else
        {
            dirtParticles.Stop();
        }

        //making player move
        rb.MovePosition(dir);
    }
}
