using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnboundedTaskGroup : MonoBehaviour
{
    private TextMeshProUGUI _taskText;
    public Task OwnTask;
    public Reward OwnReward;

    private void OnEnable() 
    {
        _taskText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _taskText.text = $"{OwnTask.Description}  {OwnTask.CurrentSuccessValue}/{OwnTask.NeedToSuccessValue}";
    }
}
