using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        animator = GetComponent<Animator>();

        Hp = 10000;

        CreatureState = Define.CreatureState.Skill;

        Skills.AddSkill<Move>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.StartNextSequenceSkill();

        return true;
    }

    public override void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                animator.Play("Idle");
                break;
            case Define.CreatureState.Moving:
                animator.Play("Moving");
                break;
            case Define.CreatureState.Skill:
                //animator.Play("Skill");
                break;
            case Define.CreatureState.Dead:
                animator.Play("Death");
                break;
        }
    }

    //protected override void UpdateIdle()
    //{
        
    //}

    //// Boss Collider + Player Collider
    //float range = 2.0f;

    //protected override void UpdateMoving()
    //{
    //    PlayerController pc = Managers.Object.Player;
    //    if (pc.IsValid() == false)
    //        return;

    //    Vector3 dir = pc.transform.position - transform.position;

    //    if(dir.magnitude < range)
    //    {
    //        CreatureState = Define.CreatureState.Skill;

    //        // animator.runtimeAnimatorController.animationClips;
    //        float animLength = 0.41f;
    //        Wait(animLength);
    //    }
    //}

    //protected override void UpdateSkill()
    //{
    //    if (coWait == null)
    //        CreatureState = Define.CreatureState.Moving;
    //}

    protected override void UpdateDead()
    {
        if(coWait == null)
            Managers.Object.Despawn(this);
    }


    #region Wait Coroutine

    Coroutine coWait;

    void Wait(float waitSeconds)
    {
        if(coWait != null)
            StopCoroutine(coWait);
        coWait = StartCoroutine(CoStartWait(waitSeconds));
    }

    IEnumerator CoStartWait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        coWait = null;
    }

    #endregion 

    public override void OnDamaged(BaseController attacker, float damage = 0)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void OnDead()
    {
        CreatureState = Define.CreatureState.Dead;
        Wait(2.0f);
        
    }
}
