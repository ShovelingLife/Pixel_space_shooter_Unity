using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGlobal<T> : MonoBehaviour where T : MonoBehaviour
{
    static T          _inst;
    static GameObject obj;

    public static T inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindAnyObjectByType<T>();

                if (_inst == null)
                {
                    obj = new GameObject();
                    obj.name = "(Singleton) " + typeof(T).ToString();
                    _inst = obj.AddComponent<T>();
                }
                else 
                    obj = _inst.gameObject;

                DontDestroyOnLoad(obj);
            }

            return _inst;
        }
    }
}