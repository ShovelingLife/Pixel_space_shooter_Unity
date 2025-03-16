using UnityEngine;

public class PlayerMissileAttack : MonoBehaviour
{
    float turnpeed     = 10f;
    float missileSpeed = 5f;


    public void AttackEnemy()
    {
        Vector2 curPos   = transform.localPosition;
        Vector2 enemyPos = GetComponent<PlayerMissile>().enemyCore.transform.localPosition;
        Vector2 dirPos   = curPos - enemyPos;

        // Rotate
        float angle             = (Mathf.Atan2(dirPos.y, dirPos.x) * Mathf.Rad2Deg) + 90f;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f, 0f, angle), turnpeed * Time.deltaTime);

        // Move
        transform.localPosition = Vector2.MoveTowards(curPos, enemyPos, missileSpeed * Time.deltaTime);
    }
}