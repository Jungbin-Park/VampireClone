using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordController : SkillController
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

    public override bool Init()
    {
        base.Init();

        // Active 될 때까지 콜라이더 물리 적용 X
        for (int i = 0; i < swingParticles.Length; i++)
            swingParticles[i].GetComponent<Rigidbody2D>().simulated = false;

        for (int i = 0; i < swingParticles.Length; i++)
            swingParticles[i].gameObject.GetOrAddComponent<EgoSwordChild>().SetInfo(Managers.Object.Player, 100);

        return true;
    }

    public void ActivateSkill()
    {
        StartCoroutine(CoSwingSword());
    }

    float coolTime = 2.0f;

    IEnumerator CoSwingSword()
    {
        while(true)
        {
            yield return new WaitForSeconds(coolTime);

            SetParticles(SwingType.First);
            swingParticles[(int)SwingType.First].Play();
            TurnOnPhysics(SwingType.First, true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.First].main.duration);
            TurnOnPhysics(SwingType.First, false);

            SetParticles(SwingType.Second);
            swingParticles[(int)SwingType.Second].Play();
            TurnOnPhysics(SwingType.Second, true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Second].main.duration);
            TurnOnPhysics(SwingType.Second, false);

            SetParticles(SwingType.Third);
            swingParticles[(int)SwingType.Third].Play();
            TurnOnPhysics(SwingType.Third, true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Third].main.duration);
            TurnOnPhysics(SwingType.Third, false);

            SetParticles(SwingType.Fourth);
            swingParticles[(int)SwingType.Fourth].Play();
            TurnOnPhysics(SwingType.Fourth, true);
            yield return new WaitForSeconds(swingParticles[(int)SwingType.Fourth].main.duration);
            TurnOnPhysics(SwingType.Fourth, false);

        }
    }

    void SetParticles(SwingType swingType)
    {
        float z = transform.parent.transform.eulerAngles.z;
        float radian = (Mathf.PI / 180) * z - 1;

        var main = swingParticles[(int)swingType].main;
        main.startRotation = radian;
    }

    private void TurnOnPhysics(SwingType swingType, bool simulated)
    {
        for (int i = 0; i < swingParticles.Length; i++)
            swingParticles[i].GetComponent<Rigidbody2D>().simulated = false;

        swingParticles[(int)swingType].GetComponent<Rigidbody2D>().simulated = simulated;
    }
}
