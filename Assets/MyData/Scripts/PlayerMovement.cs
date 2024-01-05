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
    ParticleSystem dustParticles;

    private const string HORIZONTAL = "Horizontal";
    private readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");
    private readonly int SpeedParam = Animator.StringToHash("Speed");
    private readonly int VelocityParam = Animator.StringToHash("Velocity");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dustParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(HORIZONTAL);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        animator.SetBool(IsGroundedParam, isGrounded);
        animator.SetFloat(VelocityParam, rb.velocity.y);
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
        UIManager.Instance.RestartGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            UIManager.Instance.RestartGame();
        }
        if(collision.CompareTag("Clouds"))
        {
            rb.AddForce(Vector2.right * 1000f,ForceMode2D.Impulse);
            Debug.Log("Clouds!!");
        }
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Finish"))
        {
            UIManager.Instance.RestartGame();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            dustParticles.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
