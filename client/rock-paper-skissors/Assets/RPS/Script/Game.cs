using UnityEngine;

public class Game : MonoBehaviour
{
    public enum GameOutcome
    {
        None,
        Win,
        Lose,
        Draw
    }

    public enum GameOption
    {
        None,
        Rock,
        Paper,
        Scissors,
    }

    public class Rules
    {
        private static readonly GameOutcome[,] outcomes = new GameOutcome[,]
        {
            //                           None              Rock             Paper          Scissors
            /* None     */ { GameOutcome.None, GameOutcome.None, GameOutcome.None, GameOutcome.None },
            /* Rock     */ { GameOutcome.None, GameOutcome.Draw, GameOutcome.Lose, GameOutcome.Win  },
            /* Paper    */ { GameOutcome.None, GameOutcome.Win,  GameOutcome.Draw, GameOutcome.Lose },
            /* Scissors */ { GameOutcome.None, GameOutcome.Lose, GameOutcome.Win,  GameOutcome.Draw },
        };

        public static GameOutcome GetOutcome(GameOption option1, GameOption option2)
        {
            return outcomes[(int)option1, (int)option2];
        }
    }

    /*
    public interface IGameOption
    {
        string Name { get; }
        GameOption GameOption { get; }
    }

    public class Rock : IGameOption
    {
        public string Name => "Rock";
        public GameOption GameOption => GameOption.Rock;
    }

    public class Paper : IGameOption
    {
        public string Name => "Paper";
        public GameOption GameOption => GameOption.Paper;
    }

    public class Scissors : IGameOption
    {
        public string Name => "Scissors";
        public GameOption GameOption => GameOption.Scissors;
    }
    */
}
