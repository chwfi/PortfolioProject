using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnboundedRewardGroup : MonoBehaviour
{
    private TextMeshProUGUI _rewardText;
    private Image _rewardIcon;  
    public Reward OwnReward;

    private void OnEnable() 
    {
        _rewardText = GetComponentInChildren<TextMeshProUGUI>();
        _rewardIcon = GetComponentInChildren<Image>();
    }

    public void UpdateText()
    {
        _rewardIcon.sprite = OwnReward.Icon;
        _rewardText.text = $"{OwnReward.Quantity}";
    }
}
