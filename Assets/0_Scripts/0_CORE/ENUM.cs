

// Missile moving type
public enum EMissileDir
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
    IDLE
}

// Plane inclining
public enum EPlaneState
{
    IDLE,
    RIGHT,
    LEFT,
    SHOOTING
}

// 적 비행기 웨이포인트
public enum EEnemyWaypoint
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH
};

// Log screen class transform type
public enum EClassTransformType
{
    POSITION,
    LOCAL_POSITION
}

// Pooling object enum
public enum EPoolingObjType
{
    // Player power up
    PLAYER_HEALTH,
    PLAYER_BULLET_SPEED_UP,
    PLAYER_BULLET_POWER_UP,
    PLAYER_MISSILE_POWER_UP,
    PLAYER_SHIELD,

    // Player weapons
    PLAYER_MISSILE,
    PLAYER_BULLET,

    // Enemy weapons
    ENEMY_SMALL_BULLET,
    ENEMY_MEDIUM_BULLET,
    ENEMY_BIG_BULLET,

    // Meteorite types
    BIG_METEORITE,
    MEDIUM_METEORITE,
    SMALL_METEORITE,


    MAX
}

// Level pooling
public enum EEnemyObjType
{
    // 적 종류
    ENEMY_GREEN_TYPE_ONE = ((int)EPoolingObjType.PLAYER_SHIELD) + 1,

    // 운석 종류
    BIG_METEORITE,
    MEDIUM_METEORITE,
    SMALL_METEORITE
}

// 스테이지 이벤트 타입
public enum EStageEventType
{
    ITEM,
    ENEMY,
    BOSS_ENEMY,
    METEORITE,
    CLEAR,
    MAX
}

// Boss enemy pattern type
public enum EBossPatternType
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH,
    MAX
}

// 일반 적 경로
public enum EEnemyPathType
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH
};