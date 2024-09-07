using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/Reward/GoldReward")]
public class GoldReward : Reward
{
    public override void Give(Quest quest)
    {
        GoldManager.Instance.Gold += quest.RewardCount;
    }
}
