using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{
    public float CoolTime { get; set; } = 1.0f;

    public override bool Init()
    {
        base.Init();
        return true;
    }

    #region CoSkill

    Coroutine coSkill;

    public override void ActivateSkill()
    {
        base.ActivateSkill();
        if (coSkill != null)
            StopCoroutine(coSkill);

        gameObject.SetActive(true);
        coSkill = StartCoroutine(CoStartSkill());
    }

    // 추상함수 -> 자식 클래스에서 반드시 구현
    protected abstract void DoSkillJob();

    protected virtual IEnumerator CoStartSkill()
    {
        // 스킬데이터의 쿨타임 적용
        WaitForSeconds wait = new WaitForSeconds(SkillData.CoolTime);
        yield return wait;

        while (true)
        {
            DoSkillJob();

            yield return wait;
        }
    }

    #endregion
}
