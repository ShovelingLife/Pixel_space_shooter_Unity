using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log_screen_manager : Singleton_global<Log_screen_manager>
{
    // ------- VARIABLES -------
    public Text    log;
    Queue<string>  log_text_queue = new Queue<string>();
    string         m_log_text;
    float          m_current_time = 0f;
    readonly float m_timer = 1f;
    int            m_count = 0;


    void Update()
    {
        m_current_time += Time.deltaTime;

        if (m_current_time>=m_timer)
        {
            Show_log();
            m_current_time = 0f;
        }
        Reset_log();
    }

    // Put data into queue
    public void Insert_log(string _text)
    {
        if (!log_text_queue.Contains(_text))
        {
            log_text_queue.Enqueue(_text);
            m_count++;
        }
    }

    // Print log
    void Show_log()
    {
        while(log_text_queue.Count > 0)
        {
            m_log_text += log_text_queue.Dequeue() + "\n";
        }
        log.text = m_log_text;
    }

    // Reset text after printing 5 times
    void Reset_log()
    {
        if (m_count > 7)
        {
            m_log_text = "";
            m_count    = 0;
        }
    }

    // Screen class type    
    public void Log_screen_class_position<T>(T _class, e_class_transform_type _position)
    {
        Type class_type               = _class.GetType();
        Player_missile player_missile = null;

        if (class_type == typeof(Player_missile))
            player_missile = _class as Player_missile;

        Insert_log(Convert_to_string(player_missile.transform, _position));
    }

    // Converts transform to string
    string Convert_to_string(Transform _transform, e_class_transform_type _type)
    {
        string tmp_str = "";

        switch (_type)
        {
            case e_class_transform_type.LOCAL_POSITION: // When is local position
                tmp_str = $"CLASS local_position X : {_transform.localPosition.x} " +
                          $"      local_position Y : {_transform.localPosition.y}";
                break;

            case e_class_transform_type.POSITION:       // When is position
                tmp_str = $"CLASS position X : {_transform.position.x} " +
                          $"      position Y : {_transform.position.y}";
                break;
        }
        return tmp_str;
    }
}