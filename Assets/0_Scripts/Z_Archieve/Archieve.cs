
    // 캐릭터 연속전인 키 이벤트 관련
    //void Player_action(KeyCode _input_key)
    //{
    //    int layer_mask = 1 << LayerMask.NameToLayer("Wall");
    //    player_move_pos = this.transform.position;

    //    switch (_input_key)
    //    {
    //        case KeyCode.UpArrow: // 윗쪽
    //            player_move_pos.y += player_stat_data.move_speed * Time.deltaTime;
    //            m_current_sprite_renderer.sprite = player_sprite_arr[0];
    //            ray_direction = Vector2.up;
    //            break;

    //        case KeyCode.LeftArrow: // 왼쪽
    //            player_move_pos.x -= player_stat_data.move_speed * Time.deltaTime;
    //            ray_direction = Vector2.left;
    //            break;

    //        case KeyCode.DownArrow: // 아랫쪽
    //            player_move_pos.y -= player_stat_data.move_speed * Time.deltaTime;
    //            m_current_sprite_renderer.sprite = player_sprite_arr[0];
    //            ray_direction = Vector2.down;
    //            break;

    //        case KeyCode.RightArrow: // 오른쪽
    //            player_move_pos.x += player_stat_data.move_speed * Time.deltaTime;
    //            ray_direction = Vector2.right;
    //            break;

    //        case KeyCode.Space: // 발사

    //            break;

    //        default: return;
    //    }
    //    // 이동 제한
    //    player_hit = Physics2D.Raycast(transform.position, ray_direction, 0.5f, layer_mask);
    //    if (player_hit.collider != null) return;
    //    // 이동
    //    this.transform.position = player_move_pos;
    //}

    // 캐릭터 키 단독입력
    //void Player_pressed_single_key(KeyCode _input_key)
    //{
    //    switch (_input_key)
    //    {
    //        case KeyCode.RightArrow:
    //            transform.rotation = Global.half_rotation;
    //            m_current_sprite_renderer.sprite = player_sprite_arr[1];
    //            break;

    //        case KeyCode.LeftArrow:
    //            m_current_sprite_renderer.sprite = player_sprite_arr[1];
    //            break;

    //        default: return;
    //    }
    //}

    // 캐릭터가 이동을 멈춤
    //void Player_stopped(KeyCode _input_key)
    //{
    //    switch (_input_key)
    //    {
    //        case KeyCode.UpArrow:
    //        case KeyCode.LeftArrow: 
    //        case KeyCode.DownArrow: 
    //        case KeyCode.RightArrow:
    //            this.transform.rotation = Global.zero_rotation;
    //            m_current_sprite_renderer.sprite = player_sprite_arr[0];
    //            break;
    //    }
    //}

// 회전
//m_different_pos = m_current_pos - m_enemy_pos;
// float rotation_z = Mathf.Atan2(m_different_pos.y, m_different_pos.x) * Mathf.Rad2Deg; x축 기준
//float rotation_z = (Mathf.Atan2(m_different_pos.y, m_different_pos.x) * Mathf.Rad2Deg)-90f; y축 기준
//this.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);