using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ResourceManagement.Diagnostics;

public class MonsterController : CreatureController
{
    public override bool Init()
    {
        if(base.Init())
            return false;

        // TODO
        ObjType = Define.ObjectType.Monster;

        return true;
    }

    void FixedUpdate()
    {
        PlayerController pc = Managers.Object.Player;
        if(pc == null ) return;

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * speed;
        GetComponent<Rigidbody2D>().MovePosition( newPos );

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();

        if(target == null) 
            return;

        if (coDotDamage != null)
            StopCoroutine(coDotDamage);

        // 코루틴 실행
        coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent< PlayerController>();
        if (target == null) 
            return;

        // 코루틴 중지
        if(coDotDamage != null)
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

        // 코루틴 중지
        if (coDotDamage != null)
            StopCoroutine(coDotDamage);
        coDotDamage = null;

        // 아이템 드롭
        GemController gc = Managers.Object.Spawn<GemController>(transform.position);

        Managers.Object.Despawn(this);
    }
}
