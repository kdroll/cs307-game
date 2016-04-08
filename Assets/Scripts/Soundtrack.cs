using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour {

    private static Soundtrack instance = null;
    public static Soundtrack Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
