using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnboundedRewardGroup : MonoBehaviour // Quest UI 중 보상 텍스트들을 관리해주는 스크립트
{
    private TextMeshProUGUI _rewardText;
    private Image _rewardIcon;  
    public Reward OwnReward;
    public Quest OwnQuest;

    private void OnEnable() 
    {
        _rewardText = GetComponentInChildren<TextMeshProUGUI>();
        _rewardIcon = GetComponentInChildren<Image>();
    }

    public void UpdateText()
    {
        _rewardIcon.sprite = OwnReward.Icon;
        _rewardText.text = $"{OwnQuest.RewardCount}";
    }
}
