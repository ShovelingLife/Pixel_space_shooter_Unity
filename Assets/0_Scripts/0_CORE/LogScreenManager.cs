using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScreenManager : SingletonGlobal<LogScreenManager>
{
    // ------- VARIABLES -------
    public Text    log;
    Queue<string>  logTexts = new Queue<string>();
    string         logText;
    float          curTime  = 0f;
    readonly float timer    = 1f;
    int            cnt      = 0;


    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= timer)
        {
            Show();
            curTime = 0f;
        }
        Reset();
    }

    // Put data into queue
    public void Insert(string _text)
    {
        if (!logTexts.Contains(_text))
        {
            logTexts.Enqueue(_text);
            cnt++;
        }
    }

    // Print log
    void Show()
    {
        while (logTexts.Count > 0)
               logText += logTexts.Dequeue() + "\n";

        log.text = logText;
    }

    // Reset text after printing 5 times
    void Reset()
    {
        if (cnt > 7)
        {
            logText = "";
            cnt    = 0;
        }
    }

    // Screen class type    
    public void LogClassPos<T>(T _class, EClassTransformType _position)
    {
        Type classType               = _class.GetType();
        PlayerMissile playerMissile = null;

        if (classType == typeof(PlayerMissile))
            playerMissile = _class as PlayerMissile;

        Insert(ToString(playerMissile.transform, _position));
    }

    // Converts transform to string
    string ToString(Transform _trans, EClassTransformType _type)
    {
        string tmp_str = "";

        switch (_type)
        {
            case EClassTransformType.LOCAL_POSITION: // When is local position
                tmp_str = $"CLASS local_position X : {_trans.localPosition.x} " +
                          $"      local_position Y : {_trans.localPosition.y}";
                break;

            case EClassTransformType.POSITION:       // When is position
                tmp_str = $"CLASS position X : {_trans.position.x} " +
                          $"      position Y : {_trans.position.y}";
                break;
        }
        return tmp_str;
    }
}