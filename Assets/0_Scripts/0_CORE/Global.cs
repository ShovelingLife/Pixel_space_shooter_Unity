using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class Global
{
    // 기즈모 색상
    readonly public static Dictionary<EGizmoColorType, Color> GizmoColors = new Dictionary<EGizmoColorType, Color>
    {
        {EGizmoColorType.WHITE,  Color.white },  {EGizmoColorType.BLACK, Color.black }, {EGizmoColorType.GRAY,    Color.gray },
        {EGizmoColorType.RED,    Color.red },    {EGizmoColorType.GREEN, Color.green }, {EGizmoColorType.BLUE,    Color.blue },
        {EGizmoColorType.YELLOW, Color.yellow }, {EGizmoColorType.CYAN,  Color.cyan },  {EGizmoColorType.MAGENTA, Color.magenta },
    };

    // 색상
    readonly public static Color OriginalColor = new Color(255, 255, 255, 255);
    readonly public static Color SpriteFadeColor = new Color(255, 255, 255, 0);
    readonly public static Color IconFadeColor = new Color(255, 255, 255, 125);

    // 방향
    readonly public static Vector3 LeftUpDiagonalDirection = new Vector3(-1f, 1f);
    readonly public static Vector3 RightUpDiagonalDirection = new Vector3(1f, 1f);
    readonly public static Vector3 LeftDownDiagonalDirection = new Vector3(-1f, -1f);
    readonly public static Vector3 RightDownDiagonalDirection = new Vector3(1f, -1f);

    // 회전
    readonly public static Quaternion ZeroRotation = new Quaternion();
    readonly public static Quaternion HalfRotation = new Quaternion(0f, -180f, 0f, 0f);

    // Z축 회전
    readonly public static Vector3 LeftZRotation = new Vector3(0f, 0f, 90f);
    readonly public static Vector3 DownZRotation = new Vector3(0f, 0f, 180f);
    readonly public static Vector3 RightZRotation = new Vector3(0f, 0f, 270f);

    // ------- UI VARIABLES -------
    readonly public static float GameBottomPos = -16.5f;
    readonly public static float DefaultPowerUpTime = 0.05f;

    // Player variables
    public static Transform PlayerTrans;
    
    // 랜덤형 int 변수
    public static int Rand(int min, int max) => UnityEngine.Random.Range(min, max);

    // 랜덤형 float 실수
    public static float Rand(float min, float max) => UnityEngine.Random.Range(min, max);

    // 레이캐스트 레이어 반환
    public static int GetLayermaskIndex(string layerMask) => 1 << LayerMask.NameToLayer(layerMask);

    // 전 방향 레이 테스트
    public static void TestRayAllDirection(Transform rayTarget)
    {
        Vector2[] arrRayPos = null;
        GetRaycastAllDirection(out arrRayPos);

        // 8 방향으로 레이캐스트를 쏨
        if (arrRayPos != null)
        {
            for (int i = 0; i < 8; i++)
                Debug.DrawLine(rayTarget.localPosition, arrRayPos[i], Color.red);
        }
    }

    // 전 방향 레이캐스트 지정
    public static void GetRaycastAllDirection(out Vector2[] arrRayPos)
    {
        // 여덟 방향 레이캐스트
        //  \ | /       0 1 2
        // --   --      3   4
        //  / | \       5 6 7
        arrRayPos = new Vector2[]
        {
            LeftUpDiagonalDirection,   // -1, 1 (상단 왼쪽)
            Vector2.up,                // 윗 방향
            RightUpDiagonalDirection,  // 1 , 1 (상단 오른쪽)
            Vector2.left,              // 왼쪽 방향
            Vector2.right,             // 오른쪽 방향
            LeftDownDiagonalDirection, // -1 , -1 (하단 왼쪽)
            Vector2.down,              // 아래 방향
            RightDownDiagonalDirection // 1 , -1 (하단 오른쪽)
        };
    }

    //! AssetPath를 넣으면 해당 에셋 파일 이름을 반환
    public static string GetAssetName(string sAssetPath)
    {
        string sAssetName = sAssetPath.Substring(sAssetPath.LastIndexOf("/") + 1);
        return sAssetName;
    }

    //! AssetPath를 넣으면 해당 에셋 파일 경로를 반환(파일 이름 제외)
    public static string GetAssetPath(string sAssetPath)
    {
        string sAssetName = sAssetPath.Substring(0, sAssetPath.LastIndexOf("/"));
        return sAssetName;
    }

    public static GameObject GetInstantiatedPrefab(string defaultPath, string objName)
    {
        string pathToLoad = defaultPath + ".prefab";
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(pathToLoad);

        if (obj != null)
            obj.name = objName;

        return obj;
    }

    // 모든 상속된 클래스들을 반환
    public static IEnumerable<Type> GetAllDerivedClasses<T>() where T : class
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(T)));
    }

    public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class, IComparable<T>
    {
        List<T> objects = new List<T>();
        foreach (Type type in
            Assembly.GetAssembly(typeof(T)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
        {
            objects.Add((T)Activator.CreateInstance(type, constructorArgs));
        }
        objects.Sort();
        return objects;
    }

    public static bool IsEmpty(string name) => name.Equals("key") || name.Equals("value");
}