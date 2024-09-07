using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAccessPopup : UI_Button // popupUI에 접근하는 용도의 버튼
{
    [Header("Access할 UI 이름")]
    [SerializeField] private string _popupName;

    public override void SetButtonEvent()
    {
        PopupUIController.Instance.SetPopupUI(_popupName, true);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}