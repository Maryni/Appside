using UnityEngine;

public class DontDestroyThisOnload : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}