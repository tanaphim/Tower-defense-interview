using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    public static T I
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            return;
        }
        m_Instance = this as T;
        if (m_Instance != null)
        {
            m_Instance.Init();
        }
    }

    private void OnApplicationQuit()
    {
        m_Instance = null;
    }

    public virtual void Init() { }
}
