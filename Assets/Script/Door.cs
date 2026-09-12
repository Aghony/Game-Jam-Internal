using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    public GameObject winPanel;

    private bool soldierInside = false;
    private bool slimeInside = false;
    private bool doorOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Soldier")
        {
            soldierInside = true;
        }

        if (collision.gameObject.name == "Slime")
        {
            slimeInside = true;
        }

        CheckDoor();
    }

    void CheckDoor()
    {
        if (soldierInside && slimeInside && !doorOpened)
        {
            doorOpened = true;

            animator.Play("Door_open");

            winPanel.SetActive(true);
        }
    }
}