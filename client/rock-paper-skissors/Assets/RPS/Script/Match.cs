using System.Collections;
using UnityEngine;
using static Game;

public class Match : MonoBehaviour
{
    public struct Result
    {
        public GameOption OptionPlayer;
        public GameOption OptionOpponent;
        public GameOutcome Outcome;

        public Result(GameOption optionPlayer, GameOption optionOpponent, GameOutcome outcome)
        {
            OptionPlayer = optionPlayer;
            OptionOpponent = optionOpponent;
            Outcome = outcome;
        }
    }

    [SerializeField] SO_GameVariant GameVariant_Standard;
    [SerializeField] SO_GameVariant GameVariant_RockPaper;

    int bet;
    Coroutine crMatch;
    GameOption optionPlayer;
    SO_GameVariant gameVariantActive;

    public void Start_Standard() => crMatch = StartCoroutine(_Match(GameVariant_Standard));
    public void Start_RockPaper() => crMatch = StartCoroutine(_Match(GameVariant_RockPaper));

    IEnumerator _Match(SO_GameVariant gameVariant)
    {
        gameVariantActive = gameVariant;
        glbl._.UI.ShowMatch(gameVariantActive.GameOptions);

        while (true)
        {
            bet = gameVariantActive.BetMinimum;
            optionPlayer = GameOption.None;

            glbl._.UI.ScreenMatch.RoundStart(gameVariantActive.BetMinimum);

            // wait on player selection
            while (optionPlayer == GameOption.None)
                yield return null;

            glbl._.UI.ScreenMatch.RoundFinish();

            var optionOpponent = gameVariantActive.GameOptions.GetRandom();
            var outcome = Rules.GetOutcome(optionPlayer, optionOpponent);
            var result = new Result(optionPlayer, optionOpponent, outcome);

            switch(outcome)
            {
                case GameOutcome.Win :  glbl._.Player.AdjustCoins(bet); break;
                case GameOutcome.Lose: glbl._.Player.AdjustCoins(-bet); break;
            }

            bool isFinishedPresentation = false;
            glbl._.UI.ScreenMatch.ShowResult(result, () =>
            {
                isFinishedPresentation = true;
            });
            while(!isFinishedPresentation)
                yield return null;
        }
    }

    public int BetIncrease() => AdjustBet(gameVariantActive.BetMinimum);
    public int BetDecrease() => AdjustBet(-gameVariantActive.BetMinimum);
    private int AdjustBet(int amt)
    {
        bet += amt;
        bet = Mathf.Max(gameVariantActive.BetMinimum, bet);
        bet = Mathf.Min(gameVariantActive.BetMaximum > 0 ? gameVariantActive.BetMaximum : glbl._.Player.Coins, bet);
        return bet;
    }

    public void MatchExit()
    {
        StopCoroutine(crMatch);
        crMatch = null;
        gameVariantActive = null;
        glbl._.UI.ShowMainMenu();
    }

    public void OnPlayerSelectOption(GameOption option)
    {
        optionPlayer = option;
    }
}
