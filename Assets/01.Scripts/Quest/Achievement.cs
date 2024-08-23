using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Achievement")]
public class Achievement : Quest
{
    public override bool IsCancelable => false;
    public override bool IsSavable => true;

    public override void Cancel()
    {
        Debug.LogAssertion("Achievement can't be canceled");
    }
}
