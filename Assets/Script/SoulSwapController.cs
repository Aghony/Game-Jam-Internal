using UnityEngine;

public class SoulSwapController : MonoBehaviour
{
    public GameObject soldier;
    public GameObject slime;

    private bool controllingSoldier = true;

    void Start()
    {
        SetControl();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controllingSoldier = !controllingSoldier;
            SetControl();
        }
    }

    void SetControl()
    {
        soldier.GetComponent<CharacterController2D>().enabled = controllingSoldier;
        slime.GetComponent<CharacterController2D>().enabled = !controllingSoldier;
    }
}