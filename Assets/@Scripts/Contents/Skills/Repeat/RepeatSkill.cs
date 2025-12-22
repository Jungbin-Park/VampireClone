using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{
    public float CoolTime { get; set; } = 1.0f;


    #region CoSkill

    Coroutine coSkill;

    public override void ActivateSkill()
    {
        if (coSkill != null)
            StopCoroutine(coSkill);

        coSkill = StartCoroutine(CoStartSkill());
    }

    // 추상함수 -> 자식 클래스에서 반드시 구현
    protected abstract void DoSkillJob();

    protected virtual IEnumerator CoStartSkill()
    {
        WaitForSeconds wait = new WaitForSeconds(CoolTime);

        while(true)
        {
            DoSkillJob();

            yield return wait;
        }
    }

    #endregion
}
