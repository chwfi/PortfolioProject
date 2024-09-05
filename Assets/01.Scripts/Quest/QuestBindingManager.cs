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
        var clone = Instantiate(_prefab, _createTransform);
        quest.OnSetUI += clone.SetUI;
        quest.OnUpdateUI += clone.UpdateUI;
        return clone;
    }
}
