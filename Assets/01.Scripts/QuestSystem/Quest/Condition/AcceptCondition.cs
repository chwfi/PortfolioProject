using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/Condition/Accept")]
public class AcceptCondition : QuestCondition
{
    public override bool IsPrepared(Quest quest)
    {
        return quest.IsAcceptable; //바꿔야댐
    }
}
