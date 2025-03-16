using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 파워업 아이템 코어
public class PowerUpItemCore : MonoBehaviour
{
    protected StatManager    statInst     = null;
    protected PlayerManager playerInst   = null;
    protected Type           type         = null;
    protected Transform      parent       = null;
    protected Vector3        curPos       = Vector3.zero;
    protected Quaternion     curRot       = Quaternion.identity;
    protected float          fallSpeed    = 0f;
    protected float          rotateDegree = 0f;
    public    bool           isTest       = false;


    void Awake()
    {
        statInst   = StatManager.inst;
        playerInst = PlayerManager.inst;
    }

    protected virtual void Start()
    {
        curRot   = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isTest) 
            MoveItem();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 혹은 남쪽 벽과 충돌 시 제거
        ObjectPoolingManager.inst.RemoveObj(type, transform, parent);
    }

    // 아이템을 움직임
    void MoveItem()
    {
        curPos    = transform.localPosition;
        curRot    = transform.localRotation;
        curRot.y -= rotateDegree * Time.deltaTime;
        curPos.y -= fallSpeed * Time.deltaTime;

        transform.localRotation = curRot;
        transform.localPosition = curPos;
    }
}
