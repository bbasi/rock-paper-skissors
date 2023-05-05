using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class glbl : MonoBehaviour
{
    public static glbl _;

    public UI UI { get; private set; }
    public IO IO { get; private set; }
    public EVT EVT { get; private set; }
    public BBNet BBNet { get; private set; }
    public Match Match { get; private set; }
    public Audio Audio { get; private set; }
    public Addressables_ Addressables_ { get; private set; }
    public Player Player { get; private set; }


    private void Awake()
    {
        _ = this;
        IO = GetComponentInChildren<IO>();
        Player = GetComponentInChildren<Player>().Initialize(IO);
        Debug.Log($"<color=orange>Rock, Paper, Skissors</color> by <color=#0074D9>Balraj Basi</color> - v{Application.version}");
    }

    IEnumerator Start()
    {
        UI = GetComponentInChildren<UI>();
        EVT = GetComponentInChildren<EVT>();
        BBNet = GetComponentInChildren<BBNet>();
        Audio = GetComponentInChildren<Audio>();
        Addressables_ = GetComponentInChildren<Addressables_>();

        SceneManager.LoadScene("Main", LoadSceneMode.Additive);

        yield return null; // awake

        Match = FindObjectOfType<Match>();
        
        UI.Initialize();
        Audio.Initialize();
        BBNet.Initialize();

        yield return null; // start

        // Match.MatchStartStandard();
        // Match.MatchStartRockPaper();
        UI.ShowMainMenu();
    }
}
