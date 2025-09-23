using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    // 일종의 스킬 매니저
    
    public List<SkillBase> Skills { get; } = new List<SkillBase>();

    public List<RepeatSkill> RepeatedSkills { get; } = new List<RepeatSkill>();

    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();
    

    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase
    {
        System.Type type = typeof(T);

        if(type == typeof(EgoSword))
        {
            var egoSword = Managers.Object.Spawn<EgoSword>(position, Define.EGO_SWORD_ID);
            egoSword.transform.SetParent(parent);
            egoSword.ActivateSkill();

            Skills.Add(egoSword);
            RepeatedSkills.Add(egoSword);

            return egoSword as T;
        }
        else if(type == typeof(FireballSkill))
        {
            var fireBall = Managers.Object.Spawn<FireballSkill>(position, Define.EGO_SWORD_ID);
            fireBall.transform.SetParent(parent);
            fireBall.ActivateSkill();

            Skills.Add(fireBall);
            RepeatedSkills.Add(fireBall);

            return fireBall as T;
        }
        else if(type.IsSubclassOf(typeof(SequenceSkill)))
        {
            var skill = gameObject.GetOrAddComponent<T>();
            Skills.Add(skill);
            SequenceSkills.Add(skill as SequenceSkill);

            return skill as T;
        }

        return null; 
    }

    int sequenceIndex = 0;
    
    public void StartNextSequenceSkill()
    {
        if (stopped)
            return;

        if (SequenceSkills.Count == 0)
            return;

        SequenceSkills[sequenceIndex].DoSkill(OnFinishedSequenceSkill);
    }

    void OnFinishedSequenceSkill()
    {
        // 현재 인덱스의 시퀀스 스킬이 끝났으면 들어옴

        sequenceIndex = (sequenceIndex + 1) % SequenceSkills.Count;

        StartNextSequenceSkill();
    }

    bool stopped = false;

    public void StopSkills()
    {
        stopped = true;

        foreach(var skill in RepeatedSkills)
        {
            skill.StopAllCoroutines(); 
        }
    }
}
