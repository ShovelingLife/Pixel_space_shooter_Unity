using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지 함수 인터페이스
public interface i_level_functions
{
    void Run_phase1();
    void Run_phase2();
    void Run_phase3();
    void Run_phase4();
    void Run_phase5();
    void Run_boss_phase();
}

public class Level_core : MonoBehaviour
{
    Dictionary<e_level_obj_type, Type> md_obj_type = new Dictionary<e_level_obj_type, Type>()
    {
        {e_level_obj_type.ENEMY_GREEN_TYPE_ONE,typeof(Enemy_type_green_one)},
        {e_level_obj_type.BIG_METEORITE,       typeof(Big_meteorite)},
        {e_level_obj_type.MEDIUM_METEORITE,    typeof(Medium_meteorite)},
        {e_level_obj_type.SMALL_METEORITE,     typeof(Small_meteorite)},
    };
    Type                         m_current_type = null;
    protected Enemy_core[]       ma_enemies     = null;
    protected i_level_functions  level_functions;
    protected e_phase_type       current_phase  = e_phase_type.FIRST;
    public    e_level_obj_type[] a_obj_type     = new e_level_obj_type[6];
    public    int[]              a_obj_quantity = new int[6];
    public    int                count          = 0;
    public    bool               is_finished    = false;


    public virtual void Init()
    {

    }

    void Init_enemy_green_type_one()
    {
        ma_enemies = new Enemy_core[count];

        for (int i = 0; i < count; i++)
             ma_enemies[i] = Pooling_manager.instance.Get_obj(m_current_type).GetComponent<Enemy_type_green_one>();
    }

    // 배열 생성
    public virtual bool Init_values()
    {
        // 키가 없음
        if (!md_obj_type.ContainsKey(a_obj_type[(int)current_phase - 1]))
            return false;

        m_current_type = md_obj_type[a_obj_type[(int)current_phase - 1]];
        count = a_obj_quantity[(int)current_phase - 1];

        if (m_current_type == typeof(Enemy_type_green_one))
            Init_enemy_green_type_one();
        
        return true;
    }

    public virtual void Run_level()
    {
        if (!Init_values())
        {
            Debug.LogError("Level_core not instantiated");
            return;
        }
        switch (current_phase)
        {
            case e_phase_type.FIRST:  level_functions.Run_phase1();     break;
            case e_phase_type.SECOND: level_functions.Run_phase2();     break;
            case e_phase_type.THIRD:  level_functions.Run_phase3();     break;
            case e_phase_type.FOURTH: level_functions.Run_phase4();     break;
            case e_phase_type.FIFTH:  level_functions.Run_phase5();     break;
            case e_phase_type.BOSS:   level_functions.Run_boss_phase(); break;

            case e_phase_type.CLEARED:
                // 타이틀
                break;
            default:
                break;
        }
        if (m_current_type == typeof(Enemy_type_green_one))
            StartCoroutine(Spawn_manager.instance.IE_spawn_monsters(ma_enemies));

        if (current_phase < e_phase_type.CLEARED)
           current_phase++;
    }
}