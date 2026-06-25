using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class DamageAreaCircle : DamageArea<DamageAreaCircleData>
{
    public override void Initialize(DamageAreaData damageAreaData)
    {
        base.Initialize(damageAreaData);
    }

    public override async UniTask Execute()
    {
        ChangeState(DamageAreaState.Spawn);
        await UniTask.Delay((int)(_DamageAreaData._TimeData._SpawnTime * 1000));

        ChangeState(DamageAreaState.Attack);
        await UniTask.Delay((int)(_DamageAreaData._TimeData._AttackTime * 1000));

        ChangeState(DamageAreaState.FadeOut);
        await UniTask.Delay((int)(_DamageAreaData._TimeData._FadeOutTime * 1000));

        ChangeState(DamageAreaState.Released);
    }
}
