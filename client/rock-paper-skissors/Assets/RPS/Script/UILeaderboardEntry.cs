using UnityEngine;
using TMPro;

public class UILeaderboardEntry : MonoBehaviour
{
    [SerializeField] TMP_Text TXTRank;
    [SerializeField] TMP_Text TXTUsername;
    [SerializeField] TMP_Text TXTScore;
    [SerializeField] Color ColorActivePlayer = Color.green;
    CanvasGroup canvasGroup;
    Color colorUsernameDefault;

    private void Awake()
    {
        canvasGroup= GetComponent<CanvasGroup>();
        colorUsernameDefault = TXTUsername.color;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    public void Set(int rank, BBNet.LeaderboardEntry entry)
    {
        TXTRank.text = rank.ToString();
        TXTUsername.text = entry.username.ToString();
        TXTScore.text = entry.coinsHigh.ToString();
        TXTUsername.color = colorUsernameDefault;
        canvasGroup.alpha = 1;

        if (entry.username == glbl._.Player.Username)
            TXTUsername.color = ColorActivePlayer;
    }
}
