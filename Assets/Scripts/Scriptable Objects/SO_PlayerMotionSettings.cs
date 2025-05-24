using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerMotionSettings : ScriptableObject
{
    public string axisNameX;
    public float speedX;
    public float thresholdToGoVertical;
    public string axisNameY;
    public float speedY;
    public float thresholdToGoHorizontal;

    public void ChangeSpeed(float speedDiffX, float speedDiffY)
    {
        speedX += speedDiffX;
        speedY += speedDiffY;
    }
}
