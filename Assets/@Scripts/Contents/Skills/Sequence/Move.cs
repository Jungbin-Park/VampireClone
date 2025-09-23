using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SequenceSkill
{
    Rigidbody2D rb;
    Coroutine coroutine;

    private void Awake()
    {
        
    }

    public override void DoSkill(Action callback = null)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CoMove(callback));
    }

    float Speed { get; } = 2.0f;
    string AnimationName { get; } = "Moving";

    IEnumerator CoMove(Action callback = null)
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().Play(AnimationName);
        // 경과 시간
        float elapsed = 0;

        while (true)
        {
            // 5초로 설정
            elapsed += Time.deltaTime;
            if (elapsed > 5.0f)
                break;

            Vector3 dir = ((Vector2)Managers.Game.Player.transform.position - rb.position).normalized;
            Vector2 targetPos = Managers.Game.Player.transform.position + dir * UnityEngine.Random.Range(1, 4);

            if (Vector3.Distance(rb.position, targetPos) <= 0.2f)
                continue;

            Vector2 dirVec = targetPos - rb.position;
            Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + nextVec);

            yield return null;
        }

        callback?.Invoke();
    }

}
