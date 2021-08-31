using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet_pooling : MonoBehaviour
{
    public Enemy_bullet_pooling_data enemy_bullet_pooling_data;


    // 적 총알 초기화
    public void Init_enemy_bullet()
    {
        for (int i = 0; i < enemy_bullet_pooling_data.max_enemy_bullet_count; i++)
        {
            // 소형 총알
            GameObject tmp_small_bullet_obj = Instantiate(enemy_bullet_pooling_data.small_enemy_bullet_prefab, enemy_bullet_pooling_data.small_enemy_bullet_container, true);
            enemy_bullet_pooling_data.small_enemy_bullet_obj_list.Add(tmp_small_bullet_obj);
            tmp_small_bullet_obj.SetActive(false);

            // 중형 총알
            GameObject tmp_medium_bullet_obj = Instantiate(enemy_bullet_pooling_data.medium_enemy_bullet_prefab, enemy_bullet_pooling_data.medium_enemy_bullet_container, true);
            enemy_bullet_pooling_data.medium_enemy_bullet_obj_list.Add(tmp_medium_bullet_obj);
            tmp_medium_bullet_obj.SetActive(false);

            // 대형 총알
            GameObject tmp_big_bullet_obj = Instantiate(enemy_bullet_pooling_data.big_enemy_bullet_prefab, enemy_bullet_pooling_data.big_enemy_bullet_container, true);
            enemy_bullet_pooling_data.big_enemy_bullet_obj_list.Add(tmp_big_bullet_obj);
            tmp_big_bullet_obj.SetActive(false);
        }
    }

    // 적 작은 총알 오브젝트 꺼내오기
    public GameObject Get_enemy_small_bullet_obj()
    {
        foreach (var item in enemy_bullet_pooling_data.small_enemy_bullet_obj_list)
        {
            if (!item.activeInHierarchy) 
                return item;
        }
        return null;
    }
}

[System.Serializable]
public class Enemy_bullet_pooling_data // 적 총알 클래스
{
    public int max_enemy_bullet_count = 200;

    // 적 (소형)총알 관련
    [Header("소형 총알")]
    public List<GameObject> small_enemy_bullet_obj_list = new List<GameObject>();
    public GameObject       small_enemy_bullet_prefab;
    public Transform        small_enemy_bullet_container;

    // 적 (중형)총알 관련
    [Header("중형 총알")]
    public List<GameObject> medium_enemy_bullet_obj_list = new List<GameObject>();
    public GameObject       medium_enemy_bullet_prefab;
    public Transform        medium_enemy_bullet_container;

    // 적 (대형)총알 관련
    [Header("대형 총알")]
    public List<GameObject> big_enemy_bullet_obj_list = new List<GameObject>();
    public GameObject       big_enemy_bullet_prefab;
    public Transform        big_enemy_bullet_container;
}