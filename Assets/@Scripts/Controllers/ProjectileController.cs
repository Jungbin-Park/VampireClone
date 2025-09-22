using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : SkillBase
{
    // 항상 주인 오브젝트를 들고 있어야 함
    CreatureController owner;
    Vector3 moveDir;
    float speed = 10.0f;
    float lifeTime = 10.0f;

    public ProjectileController() : base(Define.SkillType.None) { }

    public override bool Init()
    {
        base.Init();

        StartDestroy(lifeTime);

        return true;
    }

    public void SetInfo(int templateID, CreatureController _owner, Vector3 _moveDir)
    {
        if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData data) == false)
        {
            Debug.LogError("ProjectileController SetIfo Failed");
            return;
        }

        owner = _owner;
        moveDir = _moveDir;
        SkillData = data;
        // TODO : Data Parsing
    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;

        if (this.IsValid() == false)
            return;

        mc.OnDamaged(owner, SkillData.damage);

        StopDestroy();

        Managers.Object.Despawn(this);
    }
}
