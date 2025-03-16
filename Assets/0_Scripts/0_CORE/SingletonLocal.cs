using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLocal<T> : MonoBehaviour where T:MonoBehaviour
{
    static T _inst;

    public static T inst
    {
        get
        {
            GameObject obj = FindAnyObjectByType<T>().gameObject;
            return (obj == null) ? obj.AddComponent<T>() : obj.GetComponent<T>();
        }
    }
}