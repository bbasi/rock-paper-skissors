using UnityEngine;
using TMPro;

public class UIScreen_MainMenu : UIScreen
{
    [SerializeField] TMP_Text TXTUsername;

    protected override void OnInitialize() => TXTUsername.text = glbl._.Player.Username;
    public void BTN_PlayStandard() => glbl._.Match.Start_Standard();
    public void BTN_PlayRockPaper() => glbl._.Match.Start_RockPaper();
    public void BTN_Leaderboard() => glbl._.UI.ShowLeaderboard();
    public void BTN_ToggleAudio() => glbl._.Audio.Toggle();
}
