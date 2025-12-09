 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ResourceManagement.Diagnostics;

public class MonsterController : CreatureController
{
    #region FSM

    Define.CreatureState creatureState = Define.CreatureState.Moving;
    public virtual Define.CreatureState CreatureState
    {
        get { return creatureState; }
        set
        {
            creatureState = value;
            // 상태 바뀌면 애니메이션 갱신
            UpdateAnimation();
        }
    }

    protected Animator animator;
    public virtual void UpdateAnimation()
    {

    }

    public override void UpdateController()
    {
        base.UpdateController();

        switch(creatureState)
        {
            case Define.CreatureState.Idle:
                UpdateIdle();
                break;
            case Define.CreatureState.Moving:
                UpdateMoving(); 
                break;
            case Define.CreatureState.Skill:
                UpdateSkill();
                break;
            case Define.CreatureState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateDead() { }

    #endregion

    public override bool Init()
    {
        base.Init();

        animator = GetComponent<Animator>();
        
        ObjType = Define.ObjectType.Monster;
        CreatureState = Define.CreatureState.Moving;

        return true;
    }

    void FixedUpdate()
    {
        if (CreatureState != Define.CreatureState.Moving)
            return;

        PlayerController pc = Managers.Object.Player;
        if (pc.IsValid() == false)
            return;

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * MoveSpeed;
        GetComponent<Rigidbody2D>().MovePosition( newPos );

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if(target.IsValid() == false) 
            return;
        if (this.IsValid() == false)
            return;

        if (coDotDamage != null)
            StopCoroutine(coDotDamage);

        // 코루틴 실행
        coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent< PlayerController>();
        if (target.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        // 코루틴 중지
        if (coDotDamage != null)
            StopCoroutine(coDotDamage);
        coDotDamage = null;
    }

    Coroutine coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while(true)
        {
            // TODO : 데미지 적용
            target.OnDamaged(this, 2);

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Game.KillCount++;

        // 코루틴 중지
        if (coDotDamage != null)
            StopCoroutine(coDotDamage);
        coDotDamage = null;

        // 아이템 드롭
        GemController gc = Managers.Object.Spawn<GemController>(transform.position);

        Managers.Object.Despawn(this);
    }
}
