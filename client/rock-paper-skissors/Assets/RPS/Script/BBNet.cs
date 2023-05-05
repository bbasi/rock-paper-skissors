using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class BBNet : MonoBehaviour
{
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string username;
        public int coinsHigh;
    }
   
    const string SERVER = "https://www.balrajbasi.com/bbnet.php";  // http://localhost/bbnet.php

    public void Initialize() => SendUserCoins();

    public void SendUserCoins()
    {
        StartCoroutine(PostRequest());
        IEnumerator PostRequest()
        {
            WWWForm form = new WWWForm();
            form.AddField("setCoins", "");
            form.AddField("username", glbl._.Player.Username);
            form.AddField("coins", glbl._.Player.Coins);
            UnityWebRequest www = UnityWebRequest.Post(SERVER, form);
            yield return www.SendWebRequest();
            // Debug.Log(www.result == UnityWebRequest.Result.Success ? "POST success" : "POST error" + www.error);
        }
    }

    public void GetLeaderboardEntriesTop(System.Action<List<LeaderboardEntry>> cb)
    {
        StartCoroutine(GetRequest());
        IEnumerator GetRequest()
        {
            string url = $"{SERVER}?method=getScores&t={System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            cb?.Invoke(www.result == UnityWebRequest.Result.Success ? JsonConvert.DeserializeObject<LeaderboardEntry[]>(www.downloadHandler.text).ToList() : null);
        }
    }
}
