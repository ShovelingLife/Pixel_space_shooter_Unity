using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite_pooling : MonoBehaviour
{
    public Meteorite_pooling_data meteorite_pooling_data;


    // 운석 초기화
    public void Init_meteorite()
    {
        for (int i = 0; i < meteorite_pooling_data.max_meteorite_count; i++)
        {
            // 작은 크기의 운석
            GameObject small_meteorite_obj = Instantiate(meteorite_pooling_data.small_meteorite_prefab, meteorite_pooling_data.small_meteorite_container);
            small_meteorite_obj.SetActive(false);
            meteorite_pooling_data.small_meteorite_obj_list.Add(small_meteorite_obj);

            // 중간 크기의 운석
            GameObject medium_meteorite_obj = Instantiate(meteorite_pooling_data.medium_meteorite_prefab, meteorite_pooling_data.medium_meteorite_container);
            medium_meteorite_obj.SetActive(false);
            meteorite_pooling_data.medium_meteorite_obj_list.Add(medium_meteorite_obj);

            // 큰 크기의 운석
            GameObject big_meteorite_obj = Instantiate(meteorite_pooling_data.big_meteorite_prefab, meteorite_pooling_data.big_meteorite_container);
            big_meteorite_obj.SetActive(false);
            meteorite_pooling_data.big_meteorite_obj_list.Add(big_meteorite_obj);
        }
    }

    // A function which returns the obj from the list
    public GameObject Get_meteorite_obj(string _type)
    {
        List<GameObject> tmp_obj_list = new List<GameObject>();

        switch (_type)
        {
            case "small":  tmp_obj_list = meteorite_pooling_data.small_meteorite_obj_list; break;

            case "medium": tmp_obj_list = meteorite_pooling_data.medium_meteorite_obj_list; break;

            case "big":    tmp_obj_list = meteorite_pooling_data.big_meteorite_obj_list; break;
        }
        foreach (var item in tmp_obj_list)
        {
            if (!item.activeInHierarchy)
                return item;
        }
        return null;
    }
}

[System.Serializable]
public class Meteorite_pooling_data // 운석 클래스
{
    public int max_meteorite_count = 100;

    // 소형 운석 관련
    [Header("소형 운석")]
    public List<GameObject> small_meteorite_obj_list = new List<GameObject>();
    public GameObject       small_meteorite_prefab;
    public Transform        small_meteorite_container;

    // 중형 운석 관련
    [Header("중형 운석")]
    public List<GameObject> medium_meteorite_obj_list = new List<GameObject>();
    public GameObject       medium_meteorite_prefab;
    public Transform        medium_meteorite_container;

    // 대형 운석 관련
    [Header("대형 운석")]
    public List<GameObject> big_meteorite_obj_list = new List<GameObject>();
    public GameObject       big_meteorite_prefab;
    public Transform        big_meteorite_container;
}