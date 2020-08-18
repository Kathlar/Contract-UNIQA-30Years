using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : SingletonBase where T : MonoBehaviour
{
    private static T instance;
    protected static T Instance
    {
        get { return instance; }
    }

    protected override sealed void Awake()
    {
        instance = this as T;
        AwakeSingleton();
    }

    protected virtual void AwakeSingleton()
    {

    }
}

public abstract class SingletonBase : MonoBehaviour
{
    protected virtual void Awake()
    {

    }
}