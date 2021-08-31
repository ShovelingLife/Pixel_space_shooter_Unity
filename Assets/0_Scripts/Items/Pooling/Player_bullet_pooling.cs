using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet_pooling : MonoBehaviour
{
    public Player_bullet_pooling_data player_bullet_pooling_data;


    // 플레이어 총알 리스트 초기화
    public void Init_player_bullet()
    {
        for (int i = 0; i < player_bullet_pooling_data.max_bullet_count; i++)
        {
            GameObject tmp_obj = GameObject.Instantiate(player_bullet_pooling_data.player_bullet_prefab, player_bullet_pooling_data.player_bullet_container, true);
            player_bullet_pooling_data.player_bullet_obj_list.Add(tmp_obj);
            tmp_obj.SetActive(false);
        }
        for (int i = 0; i < player_bullet_pooling_data.max_missile; i++)
        {
            GameObject tmp_obj = GameObject.Instantiate(player_bullet_pooling_data.player_missile_prefab, player_bullet_pooling_data.player_missile_container, true);
            player_bullet_pooling_data.player_missile_obj_list.Add(tmp_obj);
            tmp_obj.SetActive(false);
        }
    }

    // 플레이어 총알 오브젝트 꺼내오기
    public GameObject Get_player_bullet_obj()
    {
        foreach (var item in player_bullet_pooling_data.player_bullet_obj_list)
        {
            if (!item.activeInHierarchy) 
                return item;
        }
        return null;
    }

    // 플레이어 미사일 오브젝트 꺼내오기
    public GameObject Get_player_missile_obj()
    {
        foreach (var item in player_bullet_pooling_data.player_missile_obj_list)
        {
            if (!item.activeInHierarchy) 
                return item;
        }
        return null;
    }
}

[System.Serializable]
public class Player_bullet_pooling_data // 플레이어 총알 클래스
{
    // 플레이어 총알 관련
    [Header("플레이어 총알")]
    public int              max_bullet_count = 100;
    public List<GameObject> player_bullet_obj_list = new List<GameObject>();
    public GameObject       player_bullet_prefab;
    public Transform        player_bullet_container;

    // 플레이어 미사일
    [Header("플레이어 미사일")]
    public int              max_missile = 50;
    public List<GameObject> player_missile_obj_list = new List<GameObject>();
    public GameObject       player_missile_prefab;
    public Transform        player_missile_container;
}