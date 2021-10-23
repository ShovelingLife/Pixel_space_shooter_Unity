using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy_path : MonoBehaviour
{
    public bool                draw_points_line;
    public bool                draw_path_line;
    public e_enemy_path_type   enemy_path;
    public e_gizmo_color_type  color_code;
    public e_path_type         path_type;
    [Range(0, 1)] public float percent;
    public Vector3[]           arr_objs_pos;


    // 색상
    // 0 = 하양색, 1 = 검정색, 2 = 회색
    // 3 = 빨강색, 4 = 녹색,   5 = 파랑색
    // 6 = 노랑색, 7 = 하늘색, 8 = 보라색
    private void OnDrawGizmos()
    {
        // 기즈모 그려줌
        // SINGLE_CURVE     = 단일 베지어 곡선
        // DOUBLE_CURVE     = 두개의 베지어 곡선
        // draw_points_line = 각 점들을 이어주는 직선 그리기
        // draw_path_line   = 경로 직선 그리기

        if (path_type == e_path_type.SINGLE_CURVE)
        {
            if(draw_points_line)
            {
                Gizmos.DrawLine(arr_objs_pos[0], arr_objs_pos[1]);
                Gizmos.DrawLine(arr_objs_pos[1], arr_objs_pos[2]);
                Gizmos.DrawLine(arr_objs_pos[2], arr_objs_pos[3]);
            }
            if(draw_path_line)
            {
                Gizmos.color = Global.dic_gizmo_color[color_code];

                for (float i = 0; i < 100; i++)
                {
                    Vector3 from_pos = Get_first_path(i / 100);
                    Vector3 to_pos = Get_first_path((i + 1) / 100);
                    Gizmos.DrawLine(from_pos, to_pos);
                }
            }            
        }
        else if (path_type == e_path_type.DOUBLE_CURVE)
        {
            if (draw_points_line)
            {
                Gizmos.DrawLine(arr_objs_pos[0], arr_objs_pos[1]);
                Gizmos.DrawLine(arr_objs_pos[1], arr_objs_pos[2]);
                Gizmos.DrawLine(arr_objs_pos[2], arr_objs_pos[3]);
                Gizmos.DrawLine(arr_objs_pos[4], arr_objs_pos[5]);
                Gizmos.DrawLine(arr_objs_pos[5], arr_objs_pos[6]);
                Gizmos.DrawLine(arr_objs_pos[6], arr_objs_pos[7]);
            }
            if(draw_path_line)
            {
                Gizmos.color = Global.dic_gizmo_color[color_code];

                for (float i = 0; i < 100; i++)
                {
                    Vector3 from_pos = Get_first_path(i / 100);
                    Vector3 to_pos   = Get_first_path((i + 1) / 100);
                    Gizmos.DrawLine(from_pos, to_pos);
                    //Gizmos.DrawCube(Get_first_path(0f), Vector3.one / 2);
                    Gizmos.DrawIcon(Get_first_path(0f), "d_PlayButton On@2x");

                    Vector3 from_pos2 = Get_second_path(i / 100);
                    Vector3 to_pos2   = Get_second_path((i + 1) / 100);
                    Gizmos.DrawLine(from_pos2, to_pos2);
                    //Gizmos.DrawCube(Get_second_path(1f), Vector3.one / 2);
                    Gizmos.DrawIcon(Get_second_path(1f), "d_PreMatQuad@2x");                    
                }
            }
        }
        
    }

    private void Update()
    {
        //test_obj.transform.position = Get_bezier(percent += (0.1f * Time.deltaTime));

        //if (percent >= 1f)
        //    percent = 0f;
    }

    [ContextMenu("기즈모 경로 생성")]
    public void Create_path()
    {
        arr_objs_pos = new Vector3[(int)path_type];
    }

    // 그리기용
    public Vector3 Get_first_path(float _percent)
    {
        // ---------
        // |  /o\  |
        // | /   \ |
        Vector3 a = Vector3.Lerp(arr_objs_pos[0], arr_objs_pos[1], _percent);
        Vector3 b = Vector3.Lerp(arr_objs_pos[1], arr_objs_pos[2], _percent);
        Vector3 c = Vector3.Lerp(arr_objs_pos[2], arr_objs_pos[3], _percent);
        Vector3 d = Vector3.Lerp(a, b, _percent);
        Vector3 e = Vector3.Lerp(b, c, _percent);
        return Vector3.Lerp(d, e, _percent);
    }

    // 그리기용#2
    public Vector3 Get_second_path(float _percent)
    {
        Vector3 a = Vector3.Lerp(arr_objs_pos[4], arr_objs_pos[5], _percent);
        Vector3 b = Vector3.Lerp(arr_objs_pos[5], arr_objs_pos[6], _percent);
        Vector3 c = Vector3.Lerp(arr_objs_pos[6], arr_objs_pos[7], _percent);
        Vector3 d = Vector3.Lerp(a, b, _percent);
        Vector3 e = Vector3.Lerp(b, c, _percent);
        return Vector3.Lerp(d, e, _percent);
    }
}

[CustomEditor(typeof(Enemy_path))]
public class Enemy_path_editor : Editor
{
    private void OnSceneGUI()
    {
        Enemy_path gizmo   = (Enemy_path)target;

        // 텍스트 색상 및 위치 설정
        GUIStyle label_style = new GUIStyle();
        label_style.normal.textColor = Color.white;
        Vector3 label_pos = Vector3.zero;

        // 위치 움직일 수 있는 핸들 그려줌
        for (int i = 0; i < gizmo.arr_objs_pos.Length; i++)
        {
            gizmo.arr_objs_pos[i] = Handles.PositionHandle(gizmo.arr_objs_pos[i], Quaternion.identity);
            label_pos = gizmo.arr_objs_pos[i];
            label_pos.x += 0.1f;
            label_pos.y += 0.75f;
            Handles.Label(label_pos, $"Point{i}", label_style);
        }
    }
}