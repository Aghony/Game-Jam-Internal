using UnityEngine;

public class SoldierAbility : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 0.6f;
    public LayerMask breakableLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            breakableLayer
        );

        foreach (Collider2D obj in objects)
        {
            Destroy(obj.gameObject);
        }
    }
}