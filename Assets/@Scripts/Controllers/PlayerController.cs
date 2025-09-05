using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 moveDir = Vector2.zero;

    float EnvCollectDist { get; set; } = 1.0f;

    [SerializeField]
    Transform indicator;
    [SerializeField]
    Transform fireSocket;

    public Vector2 MoveDir
    {
        get { return moveDir; }
        set {  moveDir = value.normalized; }    
    }

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        speed = 5.0f;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        StartProjectile();

        return true;
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

        if(moveDir != Vector2.zero)
        {
            indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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

        //Debug.Log($"SearchGems({findGems.Count}), TotalGems({gems.Count}");
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

    // TEMP
    #region FireProjectile

    Coroutine coFireProjectile;

    void StartProjectile()
    {
        if(coFireProjectile != null)
            StopCoroutine(coFireProjectile);

        coFireProjectile = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        
        while(true)
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(fireSocket.position, 1);
            pc.SetInfo(1, this, (fireSocket.position - indicator.position).normalized);

            yield return wait;
        }
    }

    #endregion
}
