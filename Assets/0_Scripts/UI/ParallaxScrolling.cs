using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    // 배경화면 오브젝트
    public GameObject backgroundImgObj;

    // 시작 지점과 끝
    Vector3      startPos = Vector3.zero;
    Vector3      endPos   = new Vector3(0f, -1920f, 0f);
    public float speed;

    void Update()
    {
        Camera_parallax_scrolling();
    }

    // 무한 배경화면
    void Camera_parallax_scrolling()
    {
        Vector3 backgroundImgPos = backgroundImgObj.transform.localPosition;

        backgroundImgObj.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (backgroundImgPos.y <= endPos.y) 
            backgroundImgObj.transform.localPosition = startPos;
    }
}