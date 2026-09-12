using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    public Transform cameraTransform;
    public float cameraSmoothSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

    void LateUpdate()
    {
        if(cameraTransform == null ) return;

        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            cameraTransform.position.z
        );

        cameraTransform.position = Vector3.Lerp(
        cameraTransform.position,
        targetPosition,
        cameraSmoothSpeed * Time.deltaTime
        );
    }

    void SetAnimation(float horizontal)
    {
        if (horizontal != 0)
        {
            if (gameObject.name == "Soldier")
                animator.Play("Soldier_Walk");
            else if (gameObject.name == "Slime")
                animator.Play("Slime_Walk");
        }
            
            if (horizontal > 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (horizontal < 0)
                GetComponent<SpriteRenderer>().flipX = true;
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