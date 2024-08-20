using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Target/string")]
public class StringTarget : TaskTarget
{
    [SerializeField]
    private string _value;

    public override object Value => _value;

    public override bool IsEqual(object target)
    {
        string targetAsString = target as string;
        if (targetAsString == null)
            return false;
        return _value == targetAsString;
    }
}
