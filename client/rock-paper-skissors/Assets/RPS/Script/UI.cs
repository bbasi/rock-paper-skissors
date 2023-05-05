using UnityEngine;

public class UI : MonoBehaviour
{
    public UIScreen_MainMenu ScreenMainMenu { get; private set; }
    public UIScreen_Match ScreenMatch { get; private set; }
    public UIScreen_Leaderboard ScreenLeaderboard { get; private set; }
    UIScreen screenActive;

    public void Initialize()
    {
        ScreenMainMenu = FindObjectOfType<UIScreen_MainMenu>(true);
        ScreenMatch = FindObjectOfType<UIScreen_Match>(true);
        ScreenLeaderboard = FindObjectOfType<UIScreen_Leaderboard>(true);

        var uiscreens = FindObjectsOfType<UIScreen>(true);
        for (int i = 0; i < uiscreens.Length; i++) uiscreens[i].Initialize();
    }

    public void ShowMainMenu()
    {
        Transition(ScreenMainMenu);
    }

    public void ShowMatch(Game.GameOption[] options)
    {
        ScreenMatch.Setup(options);
        Transition(ScreenMatch);
    }

    public void ShowLeaderboard()
    {
        ScreenLeaderboard.Setup();
        Transition(ScreenLeaderboard);
    }

    public void Transition(UIScreen screen, System.Action cbFin = null)
    {
        if (screenActive != null)
            screenActive.Hide();
        screenActive = screen;
        screenActive.Show();
        cbFin?.Invoke();
    }
}
