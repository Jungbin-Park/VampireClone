using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;



public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }

    Define.SkillType skillType;
    public Define.SkillType SkillType 
    {  
        get
        {
            return skillType;
        }
        set
        {
            skillType = value;
        }
    }

    #region Level

    int level = 0;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    #endregion

    #region SkillData

    [SerializeField]
    public Data.SkillData skillData;
    public Data.SkillData SkillData 
    { 
        get { return skillData; }
        set
        {
            skillData = value;
        }
    }

    #endregion

    public float TotalDamage { get; set; } = 0;
    public bool IsLearnedSkill { get { return Level > 0; } }


    public virtual void ActivateSkill() { }
    public virtual void GenerateProjectile(int templateID, CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos) 
    {
        ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPos, templateID);
        pc.SetInfo(templateID, owner, dir);
    }


    #region Destroy

    Coroutine coDestroy;
    
    public void StartDestroy(float delaySeconds)
    {
        StopDestroy();
        coDestroy = StartCoroutine(CoDestroy(delaySeconds));
    }

    public void StopDestroy()
    {
        if(coDestroy != null)
        {
            StopCoroutine(coDestroy);
            coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if(this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }

    #endregion

}
