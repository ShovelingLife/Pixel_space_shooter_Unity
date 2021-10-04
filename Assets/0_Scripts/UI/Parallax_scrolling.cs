using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_scrolling : MonoBehaviour
{
    // 배경화면 오브젝트
    public GameObject background_img_obj;

    // 시작 지점과 끝
    Vector3      m_start_pos = Vector3.zero;
    Vector3      m_end_pos   = new Vector3(0f, -1920f, 0f);
    public float scrolling_speed;

    void Update()
    {
        Camera_parallax_scrolling();
    }

    // 무한 배경화면
    void Camera_parallax_scrolling()
    {
        Vector3 background_img_pos = background_img_obj.transform.localPosition;

        background_img_obj.transform.Translate(Vector3.down * scrolling_speed * Time.deltaTime);

        if (background_img_pos.y <= m_end_pos.y) background_img_obj.transform.localPosition = m_start_pos;
    }
}