    using UnityEngine;

    public class Player : MonoBehaviour
    { 
        public float moveSpeed = 3f;
        public GameObject soldier;
        public GameObject slime;
        public Animator soldierAnimator;
        public Animator slimeAnimator;
        private Rigidbody2D rb;
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
            
            SetAnimation();
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
            
        }
    }
    }
