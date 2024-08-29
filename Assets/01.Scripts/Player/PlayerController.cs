using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("Skill")]
    [SerializeField] private List<Skill> _skillList;

    protected override void Awake()
    {
        base.Awake();

        SkillManagerCompo = new SkillManager();
        SkillManagerCompo.RegisterSkills(this, _skillList);
    }
}
