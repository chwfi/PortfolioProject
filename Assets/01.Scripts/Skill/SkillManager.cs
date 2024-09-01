using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public enum SkillTypeEnum
{
    Roll,
}

public class SkillManager
{
    public Dictionary<SkillTypeEnum, Skill> SkillDictionary = new Dictionary<SkillTypeEnum, Skill>();

    public void RegisterSkills(Entity owner, List<Skill> skills)
    {
        foreach (var skill in skills)
        {
            skill.OnRegister(owner);
            SkillDictionary.Add(skill.SkillType, skill);
        }
    }

    public Skill GetSkill(SkillTypeEnum skillType)
    {
        SkillDictionary.TryGetValue(skillType, out Skill value);
        if (!value.Available)
            return null;
        else
            return value;
    }

    public void ManageSkill(Skill skill)
    {
        skill.Available = false;
        CoroutineUtil.CallWaitForSeconds(skill.Cooldown, () => skill.Available = true);
    }
}
