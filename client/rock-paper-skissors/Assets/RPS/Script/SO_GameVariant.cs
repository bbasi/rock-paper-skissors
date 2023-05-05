using UnityEngine;

[CreateAssetMenu(fileName = "GameVariant", menuName = "Create - Game Variant", order = 1)]
public class SO_GameVariant : ScriptableObject
{
    [SerializeField] private string variantName;
    [SerializeField] private string description;
    [SerializeField] private Game.GameOption[] gameOptions;
    [SerializeField] private int betMinimum;
    [SerializeField] private int betMaximum;

    public string VariantName { get { return variantName; } }
    public string Description { get { return description; } }
    public Game.GameOption[] GameOptions { get { return gameOptions; } }
    public int BetMinimum { get { return betMinimum; } }
    public int BetMaximum { get { return betMaximum; } }
}
