using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnboundedTaskGroup : MonoBehaviour // Quest UI 중 작업 텍스트들을 관리해주는 스크립트
{
    private TextMeshProUGUI _taskText;
    public Task OwnTask;

    private void OnEnable() 
    {
        _taskText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _taskText.text = $"{OwnTask.Description}  {OwnTask.CurrentSuccessValue}/{OwnTask.NeedToSuccessValue}";
    }
}
