using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : UI_Base
{
    private TextMeshProUGUI _goldText;

    private void Awake() 
    {
        _goldText = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        GoldManager.Instance.OnGoldChanged += UpdateUI;
    }

    private void UpdateUI(float value)
    {
        _goldText.text = $"{value}";
    }
}
