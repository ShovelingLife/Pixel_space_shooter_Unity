using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    // 기즈모 색상
    readonly public static Dictionary<e_gizmo_color_type, Color> dic_gizmo_color = new Dictionary<e_gizmo_color_type, Color>
    {
        {e_gizmo_color_type.WHITE,  Color.white },  {e_gizmo_color_type.BLACK, Color.black }, {e_gizmo_color_type.GRAY,    Color.gray },
        {e_gizmo_color_type.RED,    Color.red },    {e_gizmo_color_type.GREEN, Color.green }, {e_gizmo_color_type.BLUE,    Color.blue },
        {e_gizmo_color_type.YELLOW, Color.yellow }, {e_gizmo_color_type.CYAN,  Color.cyan },  {e_gizmo_color_type.MAGENTA, Color.magenta },
    };

    // 색상
    readonly public static Color original_color    = new Color(255, 255, 255, 255);
    readonly public static Color sprite_fade_color = new Color(255, 255, 255, 0);
    readonly public static Color icon_fade_color   = new Color(255, 255, 255, 125);

    // 방향
    readonly public static Vector3 left_up_diagonal_direction    = new Vector3(-1f, 1f);
    readonly public static Vector3 right_up_diagonal_direction   = new Vector3(1f, 1f);
    readonly public static Vector3 left_down_diagonal_direction  = new Vector3(-1f, -1f);
    readonly public static Vector3 right_down_diagonal_direction = new Vector3(1f, -1f);

    // 회전
    readonly public static Quaternion zero_rotation = new Quaternion();
    readonly public static Quaternion half_rotation = new Quaternion(0f,-180f,0f,0f);

    // Z축 회전
    readonly public static Vector3 left_z_rotation  = new Vector3(0f, 0f, 90f);
    readonly public static Vector3 down_z_rotation  = new Vector3(0f, 0f, 180f);
    readonly public static Vector3 right_z_rotation = new Vector3(0f, 0f, 270f);

    // ------- UI VARIABLES -------
    readonly public static float game_bottom_pos          = -16.5f;
    readonly public static float default_power_up_ui_time = 0.05f;

    // Player variables
    readonly public static int power_up_item_array_index     = 5;
    readonly public static int power_up_position_array_index = 26;

    // 아이템 위치 관련
    public static Vector3[] arr_power_up_pos;

    // 값 초기화
    public static void Init_arr_power_up_pos()
    {
        // 위치 배열 업데이트
        arr_power_up_pos = new Vector3[power_up_position_array_index];
        float pos_x = -9.75f;

        for (int i = 0; i < power_up_position_array_index; i++)
        {
            arr_power_up_pos[i].x = pos_x;
            arr_power_up_pos[i].y = 25.3f;
            arr_power_up_pos[i].z = 3f;
            pos_x += 0.75f;
        }
    }

    // 랜덤형 int 변수
    public static int Rand(int _min,int _max) { return Random.Range(_min, _max); }

    // 랜덤형 float 실수
    public static float Rand(float _min, float _max) { return Random.Range(_min, _max); }

    // 레이캐스트 레이어 반환
    public static int Get_raycast_layermask_index(string _layer_mask) { return 1 << LayerMask.NameToLayer(_layer_mask); }

    // 전 방향 레이 테스트
    public static void Test_ray_all_direction(Transform _ray_target)
    {
        Debug.DrawLine(_ray_target.localPosition, left_up_diagonal_direction,    Color.red); // -1, 1 (상단 왼쪽)
        Debug.DrawLine(_ray_target.localPosition, right_up_diagonal_direction,   Color.red); // 1 , 1 (상단 오른쪽)
        Debug.DrawLine(_ray_target.localPosition, left_down_diagonal_direction,  Color.red); // -1 , -1 (하단 왼쪽)
        Debug.DrawLine(_ray_target.localPosition, right_down_diagonal_direction, Color.red); // 1 , -1 (하단 오른쪽)
        Debug.DrawLine(_ray_target.localPosition, Vector2.up,                    Color.red); // 윗 방향
        Debug.DrawLine(_ray_target.localPosition, Vector2.left,                  Color.red); // 왼쪽 방향
        Debug.DrawLine(_ray_target.localPosition, Vector2.right,                 Color.red); // 오른쪽 방향
        Debug.DrawLine(_ray_target.localPosition, Vector2.down,                  Color.red); // 아래 방향
    }

    // 전 방향 레이캐스트 지정
    public static void Get_raycast_all_direction(out Vector2[] _ray_pos_arr)
    {
         // 여덟 방향 레이캐스트
         //  \ | /       0 1 2
         // --   --      3   4
         //  / | \       5 6 7
        _ray_pos_arr    = new Vector2[8];
        _ray_pos_arr[0] = left_up_diagonal_direction;
        _ray_pos_arr[1] = Vector2.up;
        _ray_pos_arr[2] = right_up_diagonal_direction;
        _ray_pos_arr[3] = Vector2.left;
        _ray_pos_arr[4] = Vector2.right;
        _ray_pos_arr[5] = left_down_diagonal_direction;
        _ray_pos_arr[6] = Vector2.down;
        _ray_pos_arr[7] = right_down_diagonal_direction;
    }
}