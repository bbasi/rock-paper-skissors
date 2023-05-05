using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIScreen_Match : UIScreen
{
    [SerializeField] CanvasGroup CGButtons;
    [SerializeField] TMP_Text TXTBet;
    [SerializeField] TMP_Text TXTResult;
    [SerializeField] GameObject GOBtnRock;
    [SerializeField] GameObject GOBtnPaper;
    [SerializeField] GameObject GOBtnScissor;
    [SerializeField] float DurationResultCharacterDisplay = 0.05f;
    [SerializeField] float DurationResultPost = 2.0f;
    Match match;

    public void Awake()
    {
        CGButtons.interactable = false;
        TXTResult.text = "";
    }

    protected override void OnInitialize()
    {
        match = glbl._.Match;
    }

    public void Setup(Game.GameOption[] options)
    {
        GOBtnRock.SetActive(options.Contains(Game.GameOption.Rock));
        GOBtnPaper.SetActive(options.Contains(Game.GameOption.Paper));
        GOBtnScissor.SetActive(options.Contains(Game.GameOption.Scissors));
    }

    public void ShowResult(Match.Result result, System.Action cbFin)
    {
        StartCoroutine(_ShowResult());
        IEnumerator _ShowResult()
        {
            string colorResult = "#FF851B"; // orange
            if (result.Outcome == Game.GameOutcome.Win) colorResult = "#01FF70"; // lime
            if (result.Outcome == Game.GameOutcome.Lose) colorResult = "#FF4136"; // red

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Opponent played <size=120%><b>{result.OptionOpponent}</b></size>");
            sb.AppendLine($"You played <size=120%><b>{result.OptionPlayer}</b></size>");
            sb.AppendLine($"<size=175%><b><color={colorResult}>{result.Outcome}</color></b></size>");
            TXTResult.text = sb.ToString();

            int len = System.Text.RegularExpressions.Regex.Replace(TXTResult.text, "<.*?>", "").Length;
            for (int i = 0; i <= len; i++)
            {
                TXTResult.maxVisibleCharacters = i;
                yield return new WaitForSeconds(DurationResultCharacterDisplay);
            }
            yield return new WaitForSeconds(DurationResultPost);
            TXTResult.text = "";
            cbFin();
        }
    }

    public void BTN_OptionRock() => match.OnPlayerSelectOption(Game.GameOption.Rock);
    public void BTN_OptionScissor() => match.OnPlayerSelectOption(Game.GameOption.Scissors);
    public void BTN_OptionPaper() => match.OnPlayerSelectOption(Game.GameOption.Paper);
    public void BTN_BetIncrease() => SetBetLabel(match.BetIncrease());
    public void BTN_BetDecrease() => SetBetLabel(match.BetDecrease());
    public void BTN_MainMenu() => match.MatchExit();

    void SetBetLabel(int bet) => TXTBet.text = $"Bet : ${bet}";

    public void RoundStart(int bet)
    {
        SetBetLabel(bet);
        CGButtons.interactable = true;
    }
    public void RoundFinish() => CGButtons.interactable = false;
}
