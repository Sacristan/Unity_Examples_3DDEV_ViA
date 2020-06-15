using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    private static GameManagerSingleton singletonInstance;

    [SerializeField]
    private Transform target;

    public static Transform Target
    {
        get
        {
            return singletonInstance.target;
        }
    }

    private void Awake()
    {
        if (singletonInstance == null) singletonInstance = this;
        else Destroy(this);
    }
}
