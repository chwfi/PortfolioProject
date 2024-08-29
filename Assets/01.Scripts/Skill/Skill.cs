using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public abstract class Skill : ScriptableObject
{
    [Header("SkillType")]
    [SerializeField] protected SkillTypeEnum _skillType;

    [Header("Cooldown")]
    [SerializeField] protected float _coolDown;

    public SkillTypeEnum SkillType => _skillType;

    protected Entity _owner;

    public void OnRegister(Entity owner)
    {
        _owner = owner;
    }
    
    public abstract void PlaySkill();
}
