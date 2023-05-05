using System.Collections.Generic;

public static class xtn
{
    public static T GetRandom<T>(this IList<T> list)
    {
        int idx = UnityEngine.Random.Range(0, list.Count);
        return list[idx];
    }
}
