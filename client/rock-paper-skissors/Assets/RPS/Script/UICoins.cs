using UnityEngine;
using TMPro;
using System.Collections;

public class UICoins : MonoBehaviour
{
    [SerializeField] float DurationScoreUpdate = 0.75f;
    TMP_Text txtCoins;
    
    private void Awake()
    {
        txtCoins = GetComponent<TMP_Text>();
        Adjust(glbl._.Player.Coins);

        glbl._.EVT.OnCoinAdjust += (coinsOld, coinsNew) =>
        {
            Adjust(coinsOld, coinsNew);
        };
    }

    void Adjust(int amount) => txtCoins.text = $"${amount}";

    void Adjust(int from, int to)
    {
        StartCoroutine(IncreaseScore());
        IEnumerator IncreaseScore()
        {
            float elapsed = 0.0f;
            int curr = from;
            while (elapsed < DurationScoreUpdate)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / DurationScoreUpdate);
                curr = (int)Mathf.Lerp(from, to, t);
                Adjust(curr);
                yield return null;
            }
            Adjust(to);
        }
    }
}
