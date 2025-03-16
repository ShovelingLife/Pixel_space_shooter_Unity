using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileIdle : MonoBehaviour
{
    Vector2 centerPos;
    float rotationRad = 1.5f, 
          angularSpeed   = 2.5f;
    float posX, 
          posY, 
          angle = 0f;

    float curTime = 0f;
    float cooltime     = 2.5f;
    int   count        = 0;


    private void OnEnable()
    {
        centerPos = new Vector2(transform.localPosition.x-1.5f, transform.localPosition.y);
    }

    private void OnDisable()
    {
        count = 0;
    }

    // Rotate in circle
    private void Update()
    {
        PlayerMissile missile = GetComponent<PlayerMissile>();

        // 두바퀴 돌 시 사라짐
        if (count == 2 ||
            !missile.isLostTarget)
            NoEnemy();

        else
            CheckingEnemy();
    }

    void NoEnemy()
    {
        Vector2 current_pos     = transform.localPosition;
        current_pos.y           += StatManager.inst.playerPowerUpStat.missilePowerUpData.speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = current_pos;
    }

    void CheckingEnemy()
    {
        posX                  = centerPos.x + Mathf.Cos(angle) * rotationRad;
        posY                  = centerPos.y + Mathf.Sin(angle) * rotationRad;
        transform.localPosition = new Vector2(posX, posY);
        angle                 += angularSpeed * Time.deltaTime;

        // Rotate
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + (Vector3.forward * 140f * Time.deltaTime));

        curTime += Time.deltaTime;

        if (curTime >= cooltime)
        {
            curTime = 0f;
            count++;
        }
        if (angle >= 360f)
            angle = 0f;        
    }
}