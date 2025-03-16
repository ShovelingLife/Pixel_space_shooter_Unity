using UnityEngine;

public static class Utility
{
    readonly public static int PowerUpItemArrayIndex = 5;
    readonly public static int PowerUpPosArrayIndex = 26;


    // 아이템 위치 관련
    public static Vector3[] PowerUpPoss;

    // 값 초기화
    public static void InitPowerUpPos()
    {
        // 위치 배열 업데이트
        PowerUpPoss = new Vector3[PowerUpPosArrayIndex];
        float posX = -9.75f;

        for (int i = 0; i < PowerUpItemArrayIndex; i++)
        {
            PowerUpPoss[i] = new Vector3(posX, 25.3f, 3f);
            posX += 0.75f;
        }
    }
}
