using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : SingletonMonobehaviour<T>
{
    public static T Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T) this;
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnAwake()
    {

    }
}
