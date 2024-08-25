using TMPro;
using UnityEngine;

public class TaskSuccessCountText : MonoBehaviour
{
    private TextMeshProUGUI _countText;
    public Task OwnTask;

    private void OnEnable() 
    {
        _countText = GetComponent<TextMeshProUGUI>();    
    }

    public void UpdateText()
    {
        _countText.text = $"{OwnTask.CurrentSuccessValue} / {OwnTask.NeedToSuccessValue}";
    }
}
