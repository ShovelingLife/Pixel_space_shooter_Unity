using UnityEngine;

public static class Utility
{
    readonly public static int PowerUpItemArrayIndex = 5;
    readonly public static int PowerUpPosArrayIndex = 26;


    // ������ ��ġ ����
    public static Vector3[] PowerUpPoss;

    // �� �ʱ�ȭ
    public static void InitPowerUpPos()
    {
        // ��ġ �迭 ������Ʈ
        PowerUpPoss = new Vector3[PowerUpPosArrayIndex];
        float posX = -9.75f;

        for (int i = 0; i < PowerUpItemArrayIndex; i++)
        {
            PowerUpPoss[i] = new Vector3(posX, 25.3f, 3f);
            posX += 0.75f;
        }
    }
}
