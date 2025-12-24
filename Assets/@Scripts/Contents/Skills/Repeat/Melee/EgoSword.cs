using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Diagnostics;

public class EgoSword : RepeatSkill
{
    [SerializeField]
    ParticleSystem[] swingParticles;

    protected enum SwingType
    {
        First,
        Second,
        Third,
        Fourth,
    }

    public EgoSword() { }

    public override bool Init()
    {
        base.Init();

        SkillType = Define.SkillType.EgoSword;

        return true;
    }

    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    public override void OnLevelUp()
    {
        base.OnLevelUp();
    }


    protected override IEnumerator CoStartSkill()
    {
        WaitForSeconds wait = new WaitForSeconds(CoolTime);

        while(true)
        {
            SetParticles(SwingType.First);
            swingParticles[(int)SwingType.First].gameObject.SetActive(true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.First].main.duration);

            SetParticles(SwingType.Second);
            swingParticles[(int)SwingType.Second].gameObject.SetActive(true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Second].main.duration);

            SetParticles(SwingType.Third);
            swingParticles[(int)SwingType.Third].gameObject.SetActive(true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Third].main.duration);

            SetParticles(SwingType.Fourth);
            swingParticles[(int)SwingType.Fourth].gameObject.SetActive(true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Fourth].main.duration);

            yield return wait;
        }
    }

    

    void SetParticles(SwingType swingType)
    {
        if (Managers.Game.Player == null)
            return;

        Vector3 tempAngle = Managers.Game.Player.Indicator.transform.eulerAngles;
        transform.localEulerAngles = tempAngle;
        transform.position = Managers.Game.Player.transform.position;

        float radian = Mathf.Deg2Rad * tempAngle.z * -1;

        var main = swingParticles[(int)swingType].main;
        main.startRotation = radian;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.transform.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;

        mc.OnDamaged(Owner, TotalDamage);
    }

    protected override void DoSkillJob()
    {
        StartCoroutine(CoStartSkill());
    }
}
