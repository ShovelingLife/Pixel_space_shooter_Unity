using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyPath : MonoBehaviour
{
    public bool                drawPointsLine;
    public bool                drawPathLine;
    public EEnemyPathType      enemyPath;
    public EGizmoColorType     colorCode;
    public EPathType           pathType;
    [Range(0, 1)] public float percent;
    public Vector3[]           objPositions;


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

        if (pathType == EPathType.SINGLE_CURVE)
        {
            if(drawPointsLine)
            {
                Gizmos.DrawLine(objPositions[0], objPositions[1]);
                Gizmos.DrawLine(objPositions[1], objPositions[2]);
                Gizmos.DrawLine(objPositions[2], objPositions[3]);
            }
            if(drawPathLine)
            {
                Gizmos.color = Global.GizmoColors[colorCode];

                for (float i = 0; i < 100; i++)
                {
                    Vector3 from = GetFirstPath(i / 100);
                    Vector3 to   = GetFirstPath((i + 1) / 100);
                    Gizmos.DrawLine(from, to);
                }
            }            
        }
        else if (pathType == EPathType.DOUBLE_CURVE)
        {
            if (drawPointsLine)
            {
                Gizmos.DrawLine(objPositions[0], objPositions[1]);
                Gizmos.DrawLine(objPositions[1], objPositions[2]);
                Gizmos.DrawLine(objPositions[2], objPositions[3]);
                Gizmos.DrawLine(objPositions[4], objPositions[5]);
                Gizmos.DrawLine(objPositions[5], objPositions[6]);
                Gizmos.DrawLine(objPositions[6], objPositions[7]);
            }
            if(drawPathLine)
            {
                Gizmos.color = Global.GizmoColors[colorCode];

                for (float i = 0; i < 100; i++)
                {
                    Vector3 from = GetFirstPath(i / 100);
                    Vector3 to   = GetFirstPath((i + 1) / 100);
                    Gizmos.DrawLine(from, to);
                    //Gizmos.DrawCube(Get_first_path(0f), Vector3.one / 2);
                    Gizmos.DrawIcon(GetFirstPath(0f), "d_PlayButton On@2x");

                    Vector3 from_pos2 = GetSecondPath(i / 100);
                    Vector3 to_pos2   = GetSecondPath((i + 1) / 100);
                    Gizmos.DrawLine(from_pos2, to_pos2);
                    //Gizmos.DrawCube(Get_second_path(1f), Vector3.one / 2);
                    Gizmos.DrawIcon(GetSecondPath(1f), "d_PreMatQuad@2x");                    
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
        objPositions = new Vector3[(int)pathType];
    }

    // 그리기용
    public Vector3 GetFirstPath(float _percent)
    {
        // ---------
        // |  /o\  |
        // | /   \ |
        Vector3 a = Vector3.Lerp(objPositions[0], objPositions[1], _percent);
        Vector3 b = Vector3.Lerp(objPositions[1], objPositions[2], _percent);
        Vector3 c = Vector3.Lerp(objPositions[2], objPositions[3], _percent);
        Vector3 d = Vector3.Lerp(a, b, _percent);
        Vector3 e = Vector3.Lerp(b, c, _percent);
        return Vector3.Lerp(d, e, _percent);
    }

    // 그리기용#2
    public Vector3 GetSecondPath(float _percent)
    {
        Vector3 a = Vector3.Lerp(objPositions[4], objPositions[5], _percent);
        Vector3 b = Vector3.Lerp(objPositions[5], objPositions[6], _percent);
        Vector3 c = Vector3.Lerp(objPositions[6], objPositions[7], _percent);
        Vector3 d = Vector3.Lerp(a, b, _percent);
        Vector3 e = Vector3.Lerp(b, c, _percent);
        return Vector3.Lerp(d, e, _percent);
    }
}

[CustomEditor(typeof(EnemyPath))]
public class EnemyPathEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyPath gizmo = (EnemyPath)target;

        // 텍스트 색상 및 위치 설정
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        Vector3 labelPos = Vector3.zero;

        // 위치 움직일 수 있는 핸들 그려줌
        for (int i = 0; i < gizmo.objPositions.Length; i++)
        {
            gizmo.objPositions[i] = Handles.PositionHandle(gizmo.objPositions[i], Quaternion.identity);
            labelPos = gizmo.objPositions[i];
            labelPos.x += 0.1f;
            labelPos.y += 0.75f;
            Handles.Label(labelPos, $"Point{i}", labelStyle);
        }
    }
}