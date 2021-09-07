// Missile moving type
public enum e_missile_direction_type
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
    LEFT
}

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

// Boss enemy pattern type
public enum e_boss_pattern_type
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH
}