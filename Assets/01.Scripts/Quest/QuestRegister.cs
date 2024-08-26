using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRegister : MonoBehaviour
{
    [SerializeField] private Quest[] _quest;

    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        if (!questSystem.IsFileExist) // Json파일이 존재하지 않으면 처음 실행이므로 퀘스트들 등록해준다
        {
            for (int i = 0; i < _quest.Length; i++)
            {
                Debug.Log("Registering new quest");
                questSystem.Register(_quest[i]);
            }
        }
    }
}