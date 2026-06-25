using UnityEngine;
using Cysharp.Threading.Tasks;

public class HogeUseDamageArea : MonoBehaviour
{
    void Start()
    {
        DamageAreaData areaData = new DamageAreaData();
        areaData._ShapeData = new DamageAreaCircleData();
        areaData._TimeData = new DamageAreaTimeData
        {
            _SpawnTime = 1.0f,
            _AttackTime = 2.0f,
            _FadeOutTime = 1.5f
        };

        DamageAreaCircle damageAreaCircle = new DamageAreaCircle();
        damageAreaCircle.Initialize(areaData);

        UniTask executeTask = damageAreaCircle.Execute();
    }
}
