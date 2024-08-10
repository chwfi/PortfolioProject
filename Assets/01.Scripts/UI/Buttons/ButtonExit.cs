using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : UI_Button
{
    protected override void Awake()
    {
        base.Awake();

        _buttonEvent += SetEvent;
    }

    private void SetEvent()
    {
        OwnerPopup.AccessUI(false);
    }
}
