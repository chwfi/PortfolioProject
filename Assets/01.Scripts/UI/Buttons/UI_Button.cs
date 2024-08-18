using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

public class UI_Button : UI_Base, IPointerClickHandler
{
    protected Button _button;
    protected Action _buttonEvent;

    protected UI_Popup _ownerPopup;

    public UI_Popup OwnerPopup
    {
        get
        {
            if (_ownerPopup == null)
                Debug.LogWarning("OwnerPopup is null");
            return _ownerPopup;
        }

        private set
        {
            _ownerPopup = value;
        }
    }

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();

        _ownerPopup = FindObjectUtil.FindParent<UI_Popup>(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartEvent();
    }

    public void StartEvent()
    {
        _buttonEvent.Invoke();
    }
}