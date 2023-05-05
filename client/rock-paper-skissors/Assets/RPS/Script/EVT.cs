using System;
using UnityEngine;

public class EVT : MonoBehaviour
{
    public event Action<int,int> OnCoinAdjust; // old, new

    public void RaiseOnCoinAdjust(int coinsOld, int coinsNew) => OnCoinAdjust?.Invoke(coinsOld, coinsNew);
}
