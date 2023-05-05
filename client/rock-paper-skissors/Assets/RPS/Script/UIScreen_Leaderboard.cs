using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BBNet;

public class UIScreen_Leaderboard : UIScreen
{
    [SerializeField] UILeaderboardEntry[] LeaderboardEntries;
    [SerializeField] float DurationMin = 1.5f;
    [SerializeField] Transform TransformLoading;
    [SerializeField] float SpeedSpinner = 100f;

    public void Setup()
    {
        for(int i = 0; i<LeaderboardEntries.Length; i++)
            LeaderboardEntries[i].Hide();
        TransformLoading.gameObject.SetActive(false);
    }

    protected override void OnShow()
    {
        StartCoroutine(_RefreshEntries());
        IEnumerator _RefreshEntries()
        {
            var serverEntries = new List<LeaderboardEntry>();
            glbl._.BBNet.GetLeaderboardEntriesTop(entries =>
            {
                serverEntries = entries;
            });

            TransformLoading.gameObject.SetActive(true);
            StartCoroutine(_Spin());
 
            yield return new WaitForSeconds(DurationMin);

            while (serverEntries.Count == 0)
                yield return null;

            TransformLoading.gameObject.SetActive(false);

            for (int i = 0; i < serverEntries.Count; i++)
                LeaderboardEntries[i].Set(i + 1, serverEntries[i]);
        }

        IEnumerator _Spin()
        {
            while(TransformLoading.gameObject.activeInHierarchy)
            {
                yield return null;
                TransformLoading.Rotate(new Vector3(0, 0, -SpeedSpinner * Time.deltaTime));
            }
        }
    }

    public void BTN_MainMenu() => glbl._.UI.ShowMainMenu();
}
