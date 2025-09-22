using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : 평타
// FireProjectile : 화염구
// PosionField : 장판
public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }
    public Define.SkillType SkillType {  get; set; } = Define.SkillType.None;
    public Data.SkillData SkillData { get; protected set; }

    // 0: 사용 안 함, 1 : 사용
    public int SkillLevel { get; set; } = 0;
    public bool IsLearnedSkill { get { return SkillLevel > 0; } }

    public int Damage { get; set; } = 100;

    public SkillBase(Define.SkillType skillType)
    {
        SkillType = skillType;
    }

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
