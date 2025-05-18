#if UNITY_EDITOR

using UnityEngine;

public class NameKeeper : MonoBehaviour
{
    public string objectName;

    private void Start()
    {
        gameObject.name = objectName;
    }
}

#endif