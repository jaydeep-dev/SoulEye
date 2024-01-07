using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");
    private readonly int SpeedParam = Animator.StringToHash("Speed");
    private readonly int VelocityParam = Animator.StringToHash("Velocity");

    [SerializeField] private bool isInvencible;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float checkRadius = 0.4f;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private ParticleSystem dieParticle;
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private PlayerInputController inputController;

    public bool IsGrounded { get; private set; } = false;

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = inputController.Horizontal;
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        animator.SetBool(IsGroundedParam, IsGrounded);
        animator.SetFloat(VelocityParam, rb.velocity.y);
        animator.SetFloat(SpeedParam, Mathf.Abs(horizontal));

        if (horizontal != 0)
            transform.GetComponent<SpriteRenderer>().flipX = horizontal < 0;
        
        if (IsGrounded && inputController.IsJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvencible && collision.CompareTag("Death"))
        {
            Die();
        }
        if (collision.CompareTag("Clouds"))
        {
            rb.AddForce(Vector2.right * 1000f, ForceMode2D.Impulse);
            Debug.Log("Clouds!!");
        }
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Finish"))
        {
            LevelHandler.Instance.LoadNextLevel();
        }
        
    }

    private void Die()
    {
        dieParticle.transform.SetParent(null, true);
        dieParticle.Play();
        LevelHandler.Instance.ReloadLevel();
        gameObject.SetActive(false);
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
