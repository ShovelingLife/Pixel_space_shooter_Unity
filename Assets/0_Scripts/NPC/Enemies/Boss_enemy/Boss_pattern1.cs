using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_pattern1 : Boss_pattern_core
{
    protected override void Update()
    {
        base.Update();
    }

    public override void Init()
    {
        m_max_pattern_time = 100f;
        m_reload_time = 1f;
        m_move_time = 5f;
        m_bullet_pos = boss_enemy_core.arr_bullet_pos[0].localPosition;

        Vector3 tmp_pos = transform.localPosition;
        float   speed = 0.75f;
        // 시퀀스 초기화
        m_sequence = DOTween.Sequence().OnStart(() =>
        {
            transform.DOLocalMoveX(tmp_pos.x + 1f, 0.5f);
        }).Append(GetComponent<SpriteRenderer>().DOFade(speed, 5f)).
           Append(transform.DOLocalMoveY(tmp_pos.y - 2f, speed)).
           Append(GetComponent<SpriteRenderer>().DOFade(1f, 2.5f)).
           Append(transform.DOLocalMove(new Vector3(tmp_pos.x - 1f, tmp_pos.y + 2f), speed));

        m_sequence.Play();
    }

    public override IEnumerator IE_run_pattern()
    {
        StartCoroutine(base.IE_run_pattern());
        yield return null;
    }

    protected override void Attack()
    {
        Boss_bullet_type1 bullet_type1 = null;
        Transform tmp_obj = Object_pooling_manager.instance.Create_obj(typeof(Boss_bullet_type1), boss_enemy_core.bullet_obj.transform, boss_enemy_core.bullet_container);
        tmp_obj.localPosition = m_bullet_pos;
        bullet_type1 = tmp_obj.GetComponent<Boss_bullet_type1>();

        m_bullet_pos.x += 0.25f;
        bullet_type1.current_pos = m_bullet_pos;
        bullet_type1.direction_pos = Vector3.down;
    }

    protected override void Move()
    {
    }
}
