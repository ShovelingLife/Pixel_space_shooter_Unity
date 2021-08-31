using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_pooling : MonoBehaviour
{
    public Enemy_pooling_data enemy_pooling_data;


    // 첫 일반 적 초기화
    public void Init_first_normal_enemy_settings()
    {
        for (int i = 0; i < enemy_pooling_data.max_first_enemy_count; i++)
        {
            GameObject tmp_obj = Instantiate(enemy_pooling_data.first_normal_enemy_prefab, enemy_pooling_data.first_normal_enemy_container, true);
            enemy_pooling_data.first_normal_enemy_obj_list.Add(tmp_obj);
            tmp_obj.SetActive(false);
        }
    }

    // 첫번째 적 오브젝트 꺼내오기
    public GameObject Get_first_enemy_obj()
    {
        foreach (var item in enemy_pooling_data.first_normal_enemy_obj_list)
        {
            if (!item.activeInHierarchy) 
                return item;
        }
        return null;
    }
}

[System.Serializable]
public class Enemy_pooling_data // 적 클래스
{
    // 소형 적 비행기 관련
    [Header("소형 적 비행기")]
    public int              max_first_enemy_count = 50;
    public List<GameObject> first_normal_enemy_obj_list = new List<GameObject>();
    public GameObject       first_normal_enemy_prefab;
    public Transform        first_normal_enemy_container;
}