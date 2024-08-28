using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{   
    #region Components
    public StateMachine StateMachineCompo { get; private set; }
    public EntityAnimatorController AnimatorControllerCompo { get; private set; }
    public MoveComponent MoveCompo { get; private set; }
    #endregion

    [Header("Transition Conditions")]
    [SerializeField] private TransitionCondition[] _conditions;

    public Dictionary<ConditionTypeEnum, TransitionCondition> ConditionDictionary { get; private set; }

    protected virtual void Awake() 
    {
        Transform visual = transform.Find("Visual").transform;

        AnimatorControllerCompo = visual.GetComponent<EntityAnimatorController>();
        MoveCompo = transform.GetComponent<MoveComponent>();

        SetTransitionConditions();
        SetStates();
    }

    private void SetTransitionConditions()
    {
        Debug.Log("SetConditions");
        ConditionDictionary = new Dictionary<ConditionTypeEnum, TransitionCondition>();

        foreach (var condition in _conditions)
        {
            condition.Owner = this;
            ConditionDictionary.Add(condition.ConditionType, condition);
        }
    }

    private void SetStates() // Reflection을 사용하여 런타임에 동적으로 State들에 접근해 추가해주는 로직
    {
        StateMachineCompo = new StateMachine();

        foreach (StateTypeEnum state in Enum.GetValues(typeof(StateTypeEnum))) // StateEnum 모두 돎
        {
            string typeName = state.ToString(); // stateEnum값의 이름 받아옴
            Type t = Type.GetType($"{typeName}State"); // 받아온 이름으로 Type 가져옴 (이름 형식은 "XX State")
            State newState = Activator.CreateInstance(t, this, StateMachineCompo, null, typeName) as State; 
            // Type을 넣어 리플렉션으로 State 생성. State 생성자의 매개변수들을 넘겨준다.

            if (newState == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            StateMachineCompo.AddState(state, newState); //StateMachine에 새로 생성한 State 등록
        }

        StateMachineCompo.Init(StateTypeEnum.Idle); // Idle을 첫 State로 시작
    }

    private void Update() 
    {
        StateMachineCompo.CurrentState.UpdateState();
    }
}
