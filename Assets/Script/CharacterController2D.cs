using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Vector3 cameraOffset;
    public float normalGravity = 3f;
    public float fallGravity = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    public Transform cameraTransform;
    public float cameraSmoothSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            horizontal * moveSpeed,
            rb.linearVelocity.y
        );

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        SetAnimation(horizontal);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void LateUpdate()
{
    if (cameraTransform == null)
        return;

    Vector3 targetPosition = transform.position + cameraOffset;

    targetPosition.z = cameraTransform.position.z;

    cameraTransform.position = Vector3.Lerp(
        cameraTransform.position,
        targetPosition,
        cameraSmoothSpeed * Time.deltaTime
    );
}

    void SetAnimation(float horizontal)
    {
        // Sedang di udara
        if (!isGrounded)
        {
            if (gameObject.name == "Soldier")
                animator.Play("Soldier_Jump");
            else if (gameObject.name == "Slime")
                animator.Play("Slime_Jump");

            return;
        }

        // Arah karakter
        if (horizontal > 0)
            spriteRenderer.flipX = false;
        else if (horizontal < 0)
            spriteRenderer.flipX = true;

        // Sedang berjalan
        if (horizontal != 0)
        {
            if (gameObject.name == "Soldier")
                animator.Play("Soldier_Walk");
            else if (gameObject.name == "Slime")
                animator.Play("Slime_Walk");
        }
        // Diam
        else
        {
            if (gameObject.name == "Soldier")
                animator.Play("Soldier_Idle");
            else if (gameObject.name == "Slime")
                animator.Play("Slime_Idle");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}