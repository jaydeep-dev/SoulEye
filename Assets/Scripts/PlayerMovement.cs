using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb = null;
    Animator animator = null;
    bool isGrounded = false;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] float checkRadius = 0.4f;
    [SerializeField] LayerMask groundLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Velocity", rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (horizontal > 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        else if (horizontal < 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
