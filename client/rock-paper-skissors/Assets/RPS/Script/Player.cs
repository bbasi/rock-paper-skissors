using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public struct SAV
    {
        public int Coins;
        public string Username;
        // int id
    }

    [SerializeField] SO_Usernames Usernames;

    public int Coins => sav.Coins;
    public string Username => sav.Username;
    SAV sav;


    public Player Initialize(IO io)
    {
        if(io.DoesSaveFileExist())
        {
            sav = io.Load();
        }
        else
        {
            sav = new SAV();
            sav.Coins = 1000;
            sav.Username = Usernames.GetRandom();
            io.Save(sav);
        }
        return this;
    }

    public void AdjustCoins(int amount)
    {
        const int MIN_COINS = 250;
        int newCoins = Mathf.Max(sav.Coins + amount, MIN_COINS);
        if (newCoins != sav.Coins)
        {
            glbl._.EVT.RaiseOnCoinAdjust(sav.Coins, newCoins);
            bool uploadScore = newCoins > sav.Coins;
            sav.Coins = newCoins;
            glbl._.IO.Save(sav);

            if(uploadScore)
                glbl._.BBNet.SendUserCoins();
        }
    }
}
