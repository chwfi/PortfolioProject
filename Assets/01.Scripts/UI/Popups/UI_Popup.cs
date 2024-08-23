using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public abstract class UI_Popup : UI_Base
{
    [Header("UI_Fade")]
    [SerializeField] private float _fadeDealy = 0f;
    [SerializeField] private float _fadeTime;

    protected CanvasGroup _canvasGroup;
    protected List<UI_Button> _buttonList = new();


    public virtual void Awake()
    {
        if (TryGetComponent(out CanvasGroup canvasGroup))
            _canvasGroup = canvasGroup;

        var buttons = transform.GetComponentsInChildren<UI_Button>();

        if (buttons.Any()) 
        {
            _buttonList.AddRange(buttons);
        }

        AccessUI(false);
    }

    public virtual void AccessUI(bool active)
    {
        SetInteractive(active);

        CoroutineUtil.CallWaitForSeconds(_fadeDealy, () =>
        {
            _canvasGroup.DOFade(active ? 1f : 0f, _fadeTime);
        });
    }

    private void SetInteractive(bool value)
    {
        _canvasGroup.blocksRaycasts = value;
        _canvasGroup.interactable = value;
    }
}