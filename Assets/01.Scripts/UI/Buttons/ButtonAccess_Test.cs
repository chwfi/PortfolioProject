using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccess_Test : UI_Button
{
    [Header("Access할 UI 이름")]
    [SerializeField] private string _popupName;

    public override void SetButtonEvent()
    {
        PopupUIController.Instance.GetPopupUI(_popupName).AccessUI(true);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}