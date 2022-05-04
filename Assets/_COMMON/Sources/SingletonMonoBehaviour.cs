using UnityEngine;

public abstract class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour { 
    public static T Instance;

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError($"{typeof(T).ToString()} Instance is already set.");
            Destroy(gameObject);
            return;
        }
        Instance = (T)this.GetComponent<T>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}