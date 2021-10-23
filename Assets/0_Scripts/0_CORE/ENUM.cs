

// Missile moving type
public enum e_missile_direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
    IDLE
}

// Plane inclining
public enum e_plane_state
{
    IDLE,
    RIGHT,
    LEFT,
    SHOOTING
}

// 적 비행기 웨이포인트
public enum e_enemy_waypoint
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH
};

// Log screen class transform type
public enum e_class_transform_type
{
    POSITION,
    LOCAL_POSITION
}

// Pooling object enum
public enum e_pooling_obj_type
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

    // Enemy Type
    ENEMY_GREEN_TYPE_ONE,

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
public enum e_level_obj_type
{
    // Enemy Type
    ENEMY_GREEN_TYPE_ONE,

    // Meteorite types
    BIG_METEORITE,
    MEDIUM_METEORITE,
    SMALL_METEORITE
}

// 스테이지 이벤트 타입
public enum e_stage_event_type
{
    ITEM,
    ENEMY,
    BOSS_ENEMY,
    METEORITE,
    CLEAR,
    MAX
}

// Boss enemy pattern type
public enum e_boss_pattern_type
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH,
    MAX
}

// 일반 적 경로
public enum e_enemy_path_type
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH
};