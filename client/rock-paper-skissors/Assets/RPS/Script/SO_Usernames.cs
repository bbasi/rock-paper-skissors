using UnityEngine;

[CreateAssetMenu(fileName = "Usernames", menuName = "Create - Usernames", order = 2)]
public class SO_Usernames : ScriptableObject
{
    [SerializeField] private string[] names1;
    [SerializeField] private string[] names2;

    public string GetRandom() => $"{names1.GetRandom()}{names2.GetRandom()}{RND.GetInt(1000, 9999)}";
}