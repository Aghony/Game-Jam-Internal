    using UnityEngine;

    public class Player : MonoBehaviour
    { 
        public float moveSpeed = 3f;
        public GameObject soldier;
        public GameObject slime;
        public Animator soldierAnimator;
        public Animator slimeAnimator;
        public LayerMask groundLayer;
        public float attackRadius = 0.5f;
        public float attackOffset = 0.6f;
        public LayerMask breakableLayer;
        public GameObject attackPoint;
        private Rigidbody2D rb;
        private bool isGrounded;
        private Vector2 movement;
        private bool isSoldier = true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            soldier.SetActive(true);
            slime.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;

            // Switch character
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SwitchCharacter();
            }
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
            }
            SetAnimation();
            UpdateAttackPoint();
        }

        void FixedUpdate()
        {
            rb.linearVelocity = movement * moveSpeed;
        }

        void SwitchCharacter()
        {
            isSoldier = !isSoldier;

            soldier.SetActive(isSoldier);
            slime.SetActive(!isSoldier);
        }

        void SetAnimation()
    {
        bool isMoving = movement != Vector2.zero;

        if (isSoldier) {
            if(isMoving)
            soldierAnimator.Play("Soldier_Walk");
            else
            soldierAnimator.Play("Soldier_Idle");

            if (movement.x > 0)
            soldier.GetComponent<SpriteRenderer>().flipX = false;
            else if (movement.x < 0)
            soldier.GetComponent<SpriteRenderer>().flipX = true;

        } else {

            if (isMoving) 
            slimeAnimator.Play("Slime_Walk");
            else
            slimeAnimator.Play("Slime_Idle");

            if (movement.x > 0)
            slime.GetComponent<SpriteRenderer>().flipX = false;
            else if (movement.x < 0)
            slime.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

        void Attack()
        {
            if (!isSoldier)
                return;

            Collider2D[] objects = Physics2D.OverlapCircleAll(
                attackPoint.transform.position,
                attackRadius,
                breakableLayer
            );

            foreach (Collider2D obj in objects)
            {
                Destroy(obj.gameObject);
            }
        }

        void UpdateAttackPoint()
    {
        if (!isSoldier)
            return;

        Vector3 position = attackPoint.transform.localPosition;

        if (movement.x > 0)
        {
            position.x = attackOffset;
        }
        else if (movement.x < 0)
        {
            position.x = -attackOffset;
        }

        attackPoint.transform.localPosition = position;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}
