using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SequenceSkill
{
    Rigidbody2D rb;
    Coroutine coroutine;

    public override void DoSkill(Action callback = null)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CoDash(callback));
    }

    float WaitTime { get; } = 1.0f;
    float Speed { get; } = 10.0f;
    string AnimationName { get; } = "Charge";

    IEnumerator CoDash(Action callback = null)
    {
        rb = GetComponent<Rigidbody2D>();

        // 시전 준비 시간
        yield return new WaitForSeconds(WaitTime);

        GetComponent<Animator>().Play(AnimationName);

        // 돌진 
        Vector3 dir = ((Vector2)Managers.Game.Player.transform.position - rb.position).normalized;
        // 플레이어의 위치로만 포지션을 정하면 충돌박스 때문에 덜 가거나 더 갈 수 있음(일단 랜덤으로 보정)
        Vector2 targetPos = Managers.Game.Player.transform.position + dir * UnityEngine.Random.Range(1, 5);

        while (true)
        {
            if(Vector3.Distance(rb.position, targetPos) <= 0.2f)
                break;

            Vector2 dirVec = targetPos - rb.position;

            Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + nextVec);

            yield return null;
        }

        callback?.Invoke();
    }

}
