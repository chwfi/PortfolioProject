using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{   
    #region BaseComponents
    public StateMachine StateMachineCompo { get; private set; }
    public AnimatorController AnimatorControllerCompo { get; private set; }
    public MoveComponent MoveCompo { get; private set; }
    public AttackComponent AttackCompo { get; private set; }
    public HealthComponent HealthCompo { get; private set; }
    public TargetComponent TargetCompo { get; protected set; } // protect로 구현된 setter는 자식에서 할당한다는 것을 의미
    #endregion

    [Header("Transition Conditions")]
    [SerializeField] private TransitionCondition[] _conditions;

    public Dictionary<ConditionTypeEnum, TransitionCondition> ConditionDictionary { get; private set; }

    protected virtual void Awake() 
    {
        Transform visual = transform.Find("Visual").transform;

        // 모든 Entity에 공통으로 들어가는 필수 컴포넌트들은 모두 여기서 할당
        AnimatorControllerCompo = visual.GetComponent<AnimatorController>();
        MoveCompo = transform.GetComponent<MoveComponent>();
        AttackCompo = transform.GetComponent<AttackComponent>();
        HealthCompo = transform.GetComponent<HealthComponent>();

        SetComponents();
        SetTransitionConditions();
        SetStates();
    }

    private void Start() 
    {
        StateMachineCompo.Init(StateTypeEnum.Idle); // Idle을 첫 State로 시작
    }

    private void SetComponents()
    {
        List<BaseComponent> components = new();
        components.AddRange(transform.GetComponentsInChildren<BaseComponent>());
        // 컴포넌트들을 모두 긁어와서 리스트에 넣어준다. 본인 오브젝트와 자식 오브젝트까지 모두 돈다.

        foreach (var compo in components)
        {
            compo.SetOwner(this); // 리스트를 순회하며 오너를 세팅해줌
        }
    }

    private void SetTransitionConditions()
    {
        if (_conditions.Length == 0)
            return;

        ConditionDictionary = new Dictionary<ConditionTypeEnum, TransitionCondition>();

        foreach (var condition in _conditions)
        {
            condition.Owner = this;
            ConditionDictionary.Add(condition.ConditionType, condition);
            // 컨디션 배열들을 모두 딕셔너리에 넣어줌
        }
    }

    private void SetStates() // Reflection을 사용하여 런타임에 동적으로 State들에 접근해 추가해주는 로직
    {
        StateMachineCompo = new StateMachine();

        foreach (StateTypeEnum state in Enum.GetValues(typeof(StateTypeEnum))) // StateEnum 모두 돎
        {
            string typeName = state.ToString(); // stateEnum값의 이름 받아옴
            Type t = Type.GetType($"{typeName}State"); // 받아온 이름으로 Type 가져옴 (이름 형식은 "XX State")
            State newState = Activator.CreateInstance(t, this, StateMachineCompo, typeName) as State; 
            // Type을 넣어 리플렉션으로 State 생성. State 생성자의 매개변수들을 넘겨준다. 3번째 매개변수는 State에서 직접 구현하므로 일단 null 넘김

            if (newState == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            StateMachineCompo.AddState(state, newState); //StateMachine에 새로 생성한 State 등록
        }
    }

    public bool GetConditionValid(ConditionTypeEnum conditionType)
    {
        ConditionDictionary.TryGetValue(conditionType, out TransitionCondition condition);
        if (condition == null) return false;
        else return condition.IsConditionValid();
    }

    private void Update() 
    {
        StateMachineCompo.CurrentState.UpdateState();
    }

    private void FixedUpdate() 
    {
        StateMachineCompo.CurrentState.FixedUpdateState();
    }
}
