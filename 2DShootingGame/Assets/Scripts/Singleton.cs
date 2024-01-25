using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// - Made by JH

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
// - Member Variable
    // - instance
    static private T _instance;


// - Method
    // - base
    private void Awake()
    {
        // - instance duplication check
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // - origin
    public static T Instance
    {
        get
        {
            // - null check
            if (_instance == null)
            {
                // - 1. FindObject
                _instance = FindObjectOfType<T>();

                // - 2. if instance still null, create new object
                if (_instance == null)
                {
                    GameObject singletonobj = new GameObject(typeof(T).Name);
                    _instance = singletonobj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}
