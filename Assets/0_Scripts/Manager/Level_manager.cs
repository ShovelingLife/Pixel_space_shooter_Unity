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

[Serializable]
public class Power_up_item_data
{
    public Type[] arr_pooling_obj_type = new Type[]
    {
        typeof(Bullet_power_up_item),
        typeof(Bullet_speed_up_item),
        typeof(Health_restore_item),
        typeof(Missile_power_up_item),
        typeof(Shield_power_up_item)
    };
}

public class Stage_event_data
{
    public Type               enemy_type         = null;
    public e_enemy_path_type  enemy_path_type    = e_enemy_path_type.FIRST;
    public e_stage_event_type event_type         = e_stage_event_type.MAX;
    public float              event_sec          = 0f;
    public float              enemy_shoot_time   = 0f;
    public int                event_index        = 0;
    public int                count              = 0;

    // 아이템 데이터 관련
    public e_pooling_obj_type pooling_obj_type = 0;
    public e_enemy_obj_type enemy_obj_type;


    public Stage_event_data Set_enemy_type(Type _enemy_type)
    {
        this.enemy_type = _enemy_type;
        return this;
    }

    public Stage_event_data Set_enemy_path_type(e_enemy_path_type _enemy_path_type)
    {
        this.enemy_path_type = _enemy_path_type;
        return this;
    }

    public Stage_event_data Set_event_type(e_stage_event_type _event_type)
    {
        this.event_type = _event_type;
        return this;
    }

    public Stage_event_data Set_event_sec(float _sec)
    {
        this.event_sec = _sec;
        return this;
    }

    public Stage_event_data Set_enemy_shoot_time(float _shoot_time)
    {
        this.enemy_shoot_time = _shoot_time;
        return this;
    }

    public Stage_event_data Set_event_index(int _index)
    {
        this.event_index= _index;
        return this;
    }

    public Stage_event_data Set_count(int _count)
    {
        this.count = _count;
        return this;
    }

    public Stage_event_data Set_pooling_obj_type(e_pooling_obj_type _pooling_obj_type)
    {
        this.pooling_obj_type = _pooling_obj_type;
        return this;
    }

    public Stage_event_data Set_enemy_obj_type(e_enemy_obj_type _enemy_obj_type)
    {
        this.enemy_obj_type = _enemy_obj_type;
        return this;
    }
}

public class Stage_event_data_list
{
    public float                  event_sec = 0f;
    public List<Stage_event_data> list_monster_event_data = new List<Stage_event_data>();
    public List<Stage_event_data> list_item_event_data = new List<Stage_event_data>();
}

public class Stage_data
{
    public Stack<Stage_event_data_list> stack_event_list_data = new Stack<Stage_event_data_list>();
    Stage_event_data_list list_data = new Stage_event_data_list();
    Stage_event_data current_event = null;
    public string stage_name = "스테이지01";
    float event_sec = 0;
    int max_wave = 0;
    int current_monster_wave = 0;
    int current_item_wave = 0;
    Enemy_path tmp_path;
    Enemy_path current_path;

    Power_up_item_data power_up_item_data = new Power_up_item_data();

    public void Add_event()
    {
        // 첫번째 웨이브
        {
            // 몬스터
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_enemy_type(typeof(Enemy_type_green_one))
              .Set_enemy_path_type(e_enemy_path_type.SECOND)
              .Set_event_type(e_stage_event_type.ENEMY)
              .Set_enemy_shoot_time(1.25f)
              .Set_event_index(2)
              .Set_event_sec(1f)
              .Set_count(15)
              .Set_enemy_obj_type(e_enemy_obj_type.ENEMY_GREEN_TYPE_ONE));

            // 아이템
            list_data.list_item_event_data
              .Add(new Stage_event_data()
              .Set_event_type(e_stage_event_type.ITEM)
              .Set_event_index(1)
              .Set_event_sec(1f)
              .Set_count(5)
              .Set_pooling_obj_type(e_pooling_obj_type.PLAYER_HEALTH));
        }
        // 두번쨰 웨이브
        {
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_enemy_type(typeof(Enemy_type_green_one))
              .Set_enemy_path_type(e_enemy_path_type.FIFTH)
              .Set_event_type(e_stage_event_type.ENEMY)
              .Set_enemy_shoot_time(1f)
              .Set_event_index(3)
              .Set_event_sec(2f)
              .Set_count(10)
              .Set_enemy_obj_type(e_enemy_obj_type.ENEMY_GREEN_TYPE_ONE));
        }
        // 세번째 웨이브
        {
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_enemy_type(typeof(Enemy_type_green_one))
              .Set_enemy_path_type(e_enemy_path_type.SECOND)
              .Set_event_type(e_stage_event_type.ENEMY)
              .Set_enemy_shoot_time(1.25f)
              .Set_event_index(2)
              .Set_event_sec(1f)
              .Set_count(20)
              .Set_enemy_obj_type(e_enemy_obj_type.ENEMY_GREEN_TYPE_ONE));
        }
        // 네번째 웨이브
        {
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_enemy_type(typeof(Enemy_type_green_one))
              .Set_enemy_path_type(e_enemy_path_type.FOURTH)
              .Set_event_type(e_stage_event_type.ENEMY)
              .Set_enemy_shoot_time(1.5f)
              .Set_event_index(2)
              .Set_event_sec(1.5f)
              .Set_count(10)
              .Set_enemy_obj_type(e_enemy_obj_type.ENEMY_GREEN_TYPE_ONE));

        }
        // 다섯번째 웨이브
        {
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_enemy_type(typeof(Enemy_type_green_one))
              .Set_enemy_path_type(e_enemy_path_type.THIRD)
              .Set_event_type(e_stage_event_type.ENEMY)
              .Set_enemy_shoot_time(2f)
              .Set_event_index(2)
              .Set_event_sec(1.75f)
              .Set_count(15)
              .Set_enemy_obj_type(e_enemy_obj_type.ENEMY_GREEN_TYPE_ONE));
        }
        // 마지막 웨이브
        {
            list_data.list_monster_event_data
              .Add(new Stage_event_data()
              .Set_event_type(e_stage_event_type.BOSS_ENEMY)
              .Set_enemy_shoot_time(1.25f)
              .Set_event_sec(1f));
        }
        max_wave = stack_event_list_data.Count;
        stack_event_list_data.Push(list_data);
    }

    public void Stage_update()
    {
        event_sec += Time.deltaTime;

        // 시간 경과 확인을 위해 리스트를 순회
        var item_peek = stack_event_list_data.Peek();

        if (current_item_wave >= item_peek.list_monster_event_data.Count)
            return;

        Stage_event_data data = item_peek.list_monster_event_data[current_monster_wave];

        if (event_sec >= item_peek.list_monster_event_data[current_monster_wave].event_sec)
        {
            var arr_pooling_obj_type = Level_manager.instance.arr_pooling_obj_type;

            switch (data.event_type)
            {
                // 일반 적
                case e_stage_event_type.ENEMY:
                    {
                        // 오브젝트를 가져온 후 초기화
                        int index = (int)data.enemy_obj_type;
                        Transform tmp_trans = Object_pooling_manager.instance.Create_obj(data.enemy_type, arr_pooling_obj_type[index].copy_obj.transform, arr_pooling_obj_type[index].obj_container);
                        Enemy_type_green_one enemy = tmp_trans.GetComponent<Enemy_type_green_one>();

                        // 적 정보 설정
                        current_path = Enemy_path_manager.instance.arr_path[(int)data.enemy_path_type];
                        current_path.gameObject.SetActive(true);
                        enemy.path = current_path;
                        enemy.bullet_shoot_time = data.enemy_shoot_time;
                        enemy.is_ready = true;
                        enemy.Run_attack_coroutine();
                    }
                    break;

                // 보스
                case e_stage_event_type.BOSS_ENEMY:
                    {
                        arr_pooling_obj_type[(int)data.event_type].copy_obj.SetActive(true);
                        UI_manager.instance.Set_alert_text(e_level_type.BOSS);
                    }
                    break;
            }
            data.count--;
            event_sec = 0f;

            // 페이즈 종료되면 다음꺼 진행
            if (data.count == 0)
            {
                current_monster_wave++;
                tmp_path = current_path;
                tmp_path.gameObject.SetActive(false);
            }
        }
    }

    public void Update_item()
    {
        event_sec += Time.deltaTime;

        // 시간 경과 확인을 위해 리스트를 순회
        var item_peek = stack_event_list_data.Peek();

        if (current_item_wave >= item_peek.list_item_event_data.Count)
            return;

        Stage_event_data data = item_peek.list_item_event_data[current_item_wave];

        if (event_sec >= item_peek.list_item_event_data[current_item_wave].event_sec)
        {
            var arr_pooling_obj_type = Level_manager.instance.arr_pooling_obj_type;

            switch (data.event_type)
            {
                // 아이템) 0 = 치유,
                //        1 = 탄알 개수 증가
                //        2 = 탄알 속도 증가
                //        3 = 미사일
                //        4 = 보호막
                case e_stage_event_type.ITEM:
                    {
                        int index = (int)data.pooling_obj_type;
                        Transform tmp_trans = Object_pooling_manager.instance.Create_obj(power_up_item_data.arr_pooling_obj_type[index], arr_pooling_obj_type[index].copy_obj.transform, arr_pooling_obj_type[index].obj_container);

                        // 위치 설정 후 키기
                        tmp_trans.localPosition = Global.arr_power_up_pos[Global.Rand(0, Global.power_up_position_array_index)];
                        tmp_trans.gameObject.SetActive(true);
                    }
                    break;
            }
            data.count--;
            event_sec = 0f;

            // 페이즈 종료되면 다음꺼 진행
            if (data.count == 0)
            {
                current_item_wave++;
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
        m_current_stage_data.Update_item();
    }
}