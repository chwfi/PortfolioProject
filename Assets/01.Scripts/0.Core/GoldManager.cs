using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoSingleton<GoldManager>
{
    public delegate void GoldChangedHandler(float value);

    public event GoldChangedHandler OnGoldChanged;

    private int _gold;
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            OnGoldChanged?.Invoke(_gold);
        }
    }
}
