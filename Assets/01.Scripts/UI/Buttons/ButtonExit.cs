using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : UI_Button
{
    public override void SetButtonEvent()
    {
        OwnerPopup.AccessUI(false);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
