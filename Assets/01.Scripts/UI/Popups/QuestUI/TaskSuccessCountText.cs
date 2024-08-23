using TMPro;
using UnityEngine;

public class TaskSuccessCountText : MonoBehaviour
{
    private TextMeshProUGUI _countText;

    private void OnEnable() 
    {
        _countText = GetComponent<TextMeshProUGUI>();    
    }

    public void UpdateText(int current, int need)
    {
        _countText.text = $"{current} / {need}";
    }
}
