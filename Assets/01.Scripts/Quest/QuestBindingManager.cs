using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestBindingManager : MonoSingleton<QuestBindingManager>
{
    [SerializeField] private QuestUI _prefab;
    [SerializeField] private Transform _createTransform;

    public QuestUI SetUI(Quest quest)
    {
        var clone = Instantiate(_prefab, _createTransform); // 알맞는 위치에 생성
        quest.OnSetUI += clone.SetUI; // 생성 이후 받은 퀘스트의 이벤트들을 구독해준 후
        quest.OnUpdateUI += clone.UpdateUI;
        return clone; // 반환해줌 
    }
}
