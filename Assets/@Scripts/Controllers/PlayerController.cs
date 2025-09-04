using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 moveDir = Vector2.zero;

    float EnvCollectDist { get; set; } = 1.0f;

    public Vector2 MoveDir
    {
        get { return moveDir; }
        set {  moveDir = value.normalized; }    
    }

    void Start()
    {
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        speed = 5.0f;
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        }
    }

    void HandleOnMoveDirChanged(Vector2 dir)
    {
        MoveDir = dir;
    }

    void Update()
    {
        MovePlayer();
        CollectEnv();
    }

    void MovePlayer()
    {
        //moveDir = Managers.Game.MoveDir;
        Vector3 dir = moveDir * speed * Time.deltaTime;
        transform.position += dir;
    }

    void CollectEnv()
    {
        // 제곱값을 통해 비교 (루트 연산 부하 큼)
        float sqrCollectDist = EnvCollectDist * EnvCollectDist;

        // 스폰되어있는 모든 Gem들을 긁어옴
        List<GemController> gems =  Managers.Object.Gems.ToList();
        foreach(GemController gem in gems)
        {
            // 아이템 획득 거리보다 가까우면 획득
            Vector3 dir = gem.transform.position - transform.position;
            if(dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }
        }

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

        Debug.Log($"SearchGems({findGems.Count}), TotalGems({gems.Count}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();

        if (target == null) 
            return;
        if (target.isActiveAndEnabled == false)
            return;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Debug.Log($"OnDamaged {Hp}");

        // TEMP
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }
}
