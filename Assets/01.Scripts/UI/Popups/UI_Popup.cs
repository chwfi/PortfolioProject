using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public abstract class UI_Popup : UI_Base // 창 UI(PopupUI)들의 부모가 되는 클래스
{
    [Header("UI_Fade")]
    [SerializeField] private float _fadeDealy = 0f; // UI가 켜질때 바로 켜지지 않고 딜레이를 줄것인가
    [SerializeField] private float _fadeTime; // UI가 켜지거나 꺼질 때 몇초동안 페이드를 줄것인가

    protected CanvasGroup _canvasGroup; // 팝업 UI는 캔버스 그룹으로 관리한다
    protected List<UI_Button> _buttonList = new(); // UI에 달려있는 버튼들의 리스트


    public virtual void Awake()
    {
        if (TryGetComponent(out CanvasGroup canvasGroup)) // 캔버스 그룹 가져오기
            _canvasGroup = canvasGroup;

        var buttons = transform.GetComponentsInChildren<UI_Button>(); // UI에 달려있는 버튼들 가져오기

        if (buttons.Any()) 
        {
            _buttonList.AddRange(buttons);
        }

        AccessUI(false); // 시작하면 UI 꺼줌
    }

    public virtual void AccessUI(bool active) // UI에 접근하여 키거나 끌 수 있는 함수
    {
        SetInteractive(active);

        if (active)
            transform.SetAsLastSibling(); // 만약 UI가 켜진다면, 자식 순서를 가장 나중으로 하여 맨 앞에 띄워지게 한다.

        CoroutineUtil.CallWaitForSeconds(_fadeDealy, () => // 코루틴 유틸 클래스를 이용하여 딜레이 시간만큼 기다려주고,
        {
            _canvasGroup.DOFade(active ? 1f : 0f, _fadeTime); // 캔버스 그룹을 active bool값에 따라 키거나 꺼준다. fade 가능 
        });
    }

    private void SetInteractive(bool value)
    {
        _canvasGroup.blocksRaycasts = value;
        _canvasGroup.interactable = value;
    }
}