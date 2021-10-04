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
    protected Enemy_core[] ma_enemies = null;
    protected i_level_functions level_functions;
    protected e_phase_type current_phase = e_phase_type.FIRST;
    public int count = 0;
    public bool is_finished = false;


    public virtual void Init()
    {

    }

    public virtual void Init_enemies()
    {
        ma_enemies = new Enemy_core[Level_manager.instance.a_obj_quantity[(int)current_phase - 1]];
        count = ma_enemies.Length;

        for (int i = 0; i < count; i++)
        {
            ma_enemies[i] = Pooling_manager.instance.Get_obj(Level_manager.instance.a_pooling_obj[(int)current_phase - 1]).GetComponent<Enemy_type_green_one>();
        }
    }

    public virtual void Run_level()
    {
        Init_enemies();

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
        StartCoroutine(Spawn_manager.instance.IE_spawn_monsters(ma_enemies));

        if (current_phase < e_phase_type.CLEARED)
           current_phase++;
    }
}