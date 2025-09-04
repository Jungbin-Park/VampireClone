using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : 평타
// FireProjectile : 화염구
// PosionField : 장판
public class SkillController : BaseController
{
    public Define.SkillType SkillType {  get; set; }
    public Data.SkillData SkillData { get; protected set; }

    #region Destroy

    Coroutine coDestroy;
    
    public void StartDestroy(float delaySeconds)
    {

    }

    public void StopDestroy()
    {

    }

    IEnumerator CoDestroy(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if(this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }

    #endregion

}
