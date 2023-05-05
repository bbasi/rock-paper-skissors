using UnityEngine;

public class RND : MonoBehaviour
{
    public static bool GetBool() => Random.value > 0.5f ? true : false;
    public static int GetInt(int min, int max) => Random.Range(min, max + 1);
    public static float GetFloat(float min, float max) => Random.Range(min, max);
    public static float Get01 => Random.value;
}
