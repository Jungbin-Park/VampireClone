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
        else
        {

        }

        return null; 
    }
}
