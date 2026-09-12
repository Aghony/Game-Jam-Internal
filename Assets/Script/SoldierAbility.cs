using UnityEngine;

public class SoldierAbility : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 0.3f;
    public LayerMask breakableLayer;

    public float attackCooldown = 0.2f;
    private float lastAttackTime = -1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Debug.Log("J PRESSED");
            Attack();
        }
    }

    void Attack()
    {
        Collider2D obj = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRadius,
            breakableLayer
        );

        if (obj != null)
        {
            Debug.Log("BREAKING: " + obj.gameObject.name);
            Destroy(obj.gameObject);
        }
    }
}