using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccess_Test : UI_Button
{
    protected override void Awake()
    {
        base.Awake();

        _buttonEvent += OpenPanelHandler;
    }

    private void OpenPanelHandler()
    {
        PopupUIController.Instance.GetPopupUI("TestPopup").AccessUI(true);
    }
}