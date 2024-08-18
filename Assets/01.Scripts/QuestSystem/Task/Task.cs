using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task")]
public class Task : ScriptableObject
{
    [Header("Text")]
    [SerializeField] private string _codeName; //스크립트 구별 용도
    [SerializeField] private string _description; //퀘스트 설명

    [Header("Setting")]
    [SerializeField] private int _needSuccessToComplete; //성공까지 필요한 횟수

}
