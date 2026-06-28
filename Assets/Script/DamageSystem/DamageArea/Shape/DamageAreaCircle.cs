using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class DamageAreaCircle : DamageArea<DamageAreaCircleData>
{
    public override void Initialize(DamageAreaData damageAreaData)
    {
        base.Initialize(damageAreaData);
    }

    public override async UniTask Run()
    {
        ChangeState(DamageAreaState.Spawn);
        await UniTask.Delay((int)(DamageAreaData.TimeData.FadeInTime * 1000));

        ChangeState(DamageAreaState.Attack);
        await UniTask.Delay((int)(DamageAreaData.TimeData.AttackTime * 1000));

        ChangeState(DamageAreaState.FadeOut);
        await UniTask.Delay((int)(DamageAreaData.TimeData.FadeOutTime * 1000));

        ChangeState(DamageAreaState.Released);
    }
}
