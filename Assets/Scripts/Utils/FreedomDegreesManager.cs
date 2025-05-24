using UnityEngine;

public class FreedomDegreesManager : MonoBehaviour
{
    public bool isTransformDependent;

    public void SetDependenceStatus(bool status)
    {
        isTransformDependent = status;
    }
}
