using UnityEngine;

public class IO : MonoBehaviour
{
    const string KEY = "RPS_SAVE";

    public void Save(Player.SAV save) => PlayerPrefs.SetString(KEY, JsonUtility.ToJson(save));
    public Player.SAV Load() => JsonUtility.FromJson<Player.SAV>(PlayerPrefs.GetString(KEY));
    public bool DoesSaveFileExist() => PlayerPrefs.HasKey(KEY);
}
