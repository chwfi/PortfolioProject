using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccess_Test : UI_Button
{
    public override void SetButtonEvent()
    {
        PopupUIController.Instance.GetPopupUI("TestPopup").AccessUI(true);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}