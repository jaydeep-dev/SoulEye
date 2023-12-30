using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] float checkRadius = 0.4f;
    [SerializeField] LayerMask groundLayer = 0;

    Rigidbody2D rb = null;
    Animator animator = null;
    bool isGrounded = false;

    private const string HORIZONTAL = "Horizontal";
    private readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");
    private readonly int SpeedParam = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(HORIZONTAL);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        animator.SetBool(IsGroundedParam, isGrounded);
        animator.SetFloat("Velocity", rb.velocity.y);
        animator.SetFloat(SpeedParam, Mathf.Abs(horizontal));

        transform.GetComponent<SpriteRenderer>().flipX = horizontal < 0;
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetKeyDown(KeyCode.K))
            Kill();

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Kill()
    {
        UIManager.instance.RestartGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            UIManager.instance.RestartGame();
        }
        if(collision.CompareTag("Clouds"))
        {
            rb.AddForce(Vector2.right * 1000f,ForceMode2D.Impulse);
            Debug.Log("Clouds!!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
