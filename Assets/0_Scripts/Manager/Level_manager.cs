using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pooling_obj_type
{
    public GameObject copy_obj;
    public Transform  obj_container;
}

public class Stage_event_data
{
    public Type               enemy_type         = null;
    public e_enemy_path_type  enemy_path_type    = e_enemy_path_type.FIRST;
    public e_stage_event_type current_event_type = e_stage_event_type.MAX;
    public float              event_sec          = 0f;
    public float              enemy_shoot_time   = 0f;
    public int                event_index        = 0;
    public int                count              = 0;
}

public class Stage_event_data_list
{
    public float                  event_sec = 0f;
    public List<Stage_event_data> list_event_data = new List<Stage_event_data>();
}

public class Stage_data
{
    public Stack<Stage_event_data_list> stack_event_list_data = new Stack<Stage_event_data_list>();
           Stage_event_data             m_data                = new Stage_event_data();
           Stage_event_data_list        m_list_data           = new Stage_event_data_list();
           Stage_event_data             m_current_event       = null;
    public string                       stage_name            = "스테이지01";
           float                        m_event_sec           = 0;
           int                          m_max_wave            = 0;
           int                          m_current_wave        = 0;
           Enemy_path                   m_tmp_path;
           Enemy_path                   m_current_path;


    public void Add_event()
    {
        
        // 첫번째 웨이브
        m_data.event_index        = 1;
        m_data.event_sec          = 1f;
        m_data.current_event_type = e_stage_event_type.ENEMY;
        m_data.enemy_type         = typeof(Enemy_type_green_one);
        m_data.enemy_path_type    = e_enemy_path_type.SECOND;
        m_data.count              = 15;
        m_data.enemy_shoot_time   = 1.25f;

        Add_info();


        // 두번쨰 웨이브
        m_data.event_index        = 2;
        m_data.event_sec          = 2f;
        m_data.current_event_type = e_stage_event_type.ENEMY;
        m_data.enemy_type         = typeof(Enemy_type_green_one);
        m_data.enemy_path_type    = e_enemy_path_type.FIFTH;
        m_data.count              = 10;
        m_data.enemy_shoot_time   = 1f;

        Add_info();

        // 세번쨰 웨이브
        m_data.event_index        = 2;
        m_data.event_sec          = 1f;
        m_data.current_event_type = e_stage_event_type.ENEMY;
        m_data.enemy_type         = typeof(Enemy_type_green_one);
        m_data.enemy_path_type    = e_enemy_path_type.FIRST;
        m_data.count              = 20;
        m_data.enemy_shoot_time   = 1.25f;

        Add_info();

        // 네번쨰 웨이브
        m_data.event_index        = 2;
        m_data.event_sec          = 1.25f;
        m_data.current_event_type = e_stage_event_type.ENEMY;
        m_data.enemy_type         = typeof(Enemy_type_green_one);
        m_data.enemy_path_type    = e_enemy_path_type.FOURTH;
        m_data.count              = 10;
        m_data.enemy_shoot_time   = 1.5f;

        Add_info();

        // 다섯번쨰 웨이브
        m_data.event_index        = 2;
        m_data.event_sec          = 1.75f;
        m_data.current_event_type = e_stage_event_type.ENEMY;
        m_data.enemy_type         = typeof(Enemy_type_green_one);
        m_data.enemy_path_type    = e_enemy_path_type.THIRD;
        m_data.count              = 15;
        m_data.enemy_shoot_time   = 2f;

        Add_info();

        // 마지막 웨이브
        m_data.event_index = 2;
        m_data.event_sec = 1f;
        m_data.current_event_type = e_stage_event_type.BOSS_ENEMY;

        Add_info();

        m_max_wave = stack_event_list_data.Count;
        stack_event_list_data.Push(m_list_data);
    }

    // 정보를 추가
    void Add_info()
    {
        m_list_data.list_event_data.Add(m_data);
        m_data = new Stage_event_data();
    }

    public void Stage_update()
    {
        m_event_sec += Time.deltaTime;

        // 시간 경과 확인을 위해 리스트를 순회
        if (m_event_sec >= stack_event_list_data.Peek().list_event_data[m_current_wave].event_sec)
        {
            Level_manager inst = Level_manager.instance;
            Stage_event_data data = stack_event_list_data.Peek().list_event_data[m_current_wave];
            int index = (int)data.current_event_type;

            switch (data.current_event_type)
            {
                // 일반 적
                case e_stage_event_type.ENEMY:

                    // 오브젝트를 가져온 후 초기화
                    Transform tmp_transform = Object_pooling_manager.instance.Create_obj(data.enemy_type, inst.arr_pooling_obj_type[index].copy_obj.transform, inst.arr_pooling_obj_type[index].obj_container);
                    Enemy_type_green_one enemy_type_green_one = tmp_transform.GetComponent<Enemy_type_green_one>();

                    // 적 정보 설정
                    m_current_path = Enemy_path_manager.instance.arr_path[(int)data.enemy_path_type];
                    m_current_path.gameObject.SetActive(true);
                    enemy_type_green_one.path              = m_current_path;
                    enemy_type_green_one.bullet_shoot_time = data.enemy_shoot_time;
                    enemy_type_green_one.is_ready = true;
                    enemy_type_green_one.Run_attack_coroutine();
                    break;

                case e_stage_event_type.ITEM:
                    break;

                // 보스
                case e_stage_event_type.BOSS_ENEMY:
                    inst.arr_pooling_obj_type[index].copy_obj.SetActive(true);
                    UI_manager.instance.Set_alert_text(e_level_type.BOSS);
                    break;
            }
            data.count--;
            m_event_sec = 0f;

            // 페이즈 종료되면 다음꺼 진행
            if (data.count == 0)
            {
                m_current_wave++;
                m_tmp_path = m_current_path;
                m_tmp_path.gameObject.SetActive(false);
            }
        }
    }
}

public class Level_manager : Singleton_local<Level_manager>
{
    public Pooling_obj_type[] arr_pooling_obj_type;

    [Header("현재 레벨")]
    public e_current_level_type current_level;
    Stage_data                  m_current_stage_data = new Stage_data();


    // 초기화
    public void Init()
    {
        m_current_stage_data.Add_event();
    }

    private void Update()
    {
        m_current_stage_data.Stage_update();
    }
}
