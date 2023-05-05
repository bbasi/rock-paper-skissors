using NUnit.Framework;
using static Game;

public class Test_EditMode
{
    [Test]
    [TestCase(GameOutcome.Draw, GameOption.Rock, GameOption.Rock)]
    [TestCase(GameOutcome.Win, GameOption.Rock, GameOption.Scissors)]
    [TestCase(GameOutcome.Lose, GameOption.Rock, GameOption.Paper)]
    [TestCase(GameOutcome.Draw, GameOption.Paper, GameOption.Paper)]
    [TestCase(GameOutcome.Win, GameOption.Paper, GameOption.Rock)]
    [TestCase(GameOutcome.Lose, GameOption.Paper, GameOption.Scissors)]
    [TestCase(GameOutcome.Draw, GameOption.Scissors, GameOption.Scissors)]
    [TestCase(GameOutcome.Win, GameOption.Scissors, GameOption.Paper)]
    [TestCase(GameOutcome.Lose, GameOption.Scissors, GameOption.Rock)]
    [TestCase(GameOutcome.None, GameOption.None, GameOption.None)]
    [TestCase(GameOutcome.None, GameOption.None, GameOption.Rock)]
    [TestCase(GameOutcome.None, GameOption.None, GameOption.Paper)]
    [TestCase(GameOutcome.None, GameOption.None, GameOption.Scissors)]
    [TestCase(GameOutcome.None, GameOption.Rock, GameOption.None)]
    [TestCase(GameOutcome.None, GameOption.Paper, GameOption.None)]
    [TestCase(GameOutcome.None, GameOption.Scissors, GameOption.None)]

    public void Battle(GameOutcome outcomeExpected, GameOption option1, GameOption option2)
    {
        Assert.AreEqual(outcomeExpected, Rules.GetOutcome(option1, option2));
    }
}
