using UnityEngine;
using Cysharp.Threading.Tasks;

public class HogeUseDamageArea : MonoBehaviour
{
    void Start()
    {
        DamageAreaData areaData = new DamageAreaData();
        areaData.ShapeData = new DamageAreaCircleData();
        areaData.TimeData = new DamageAreaTimeData
        {
            FadeInTime = 1.0f,
            AttackTime = 2.0f,
            FadeOutTime = 1.5f
        };

        // UniRxのSubjectを使ってDamageAreaの状態変化を監視し、状態に応じて処理を行う
        DamageAreaCircle damageAreaCircle = GetComponent<DamageAreaCircle>();
        damageAreaCircle.Initialize(areaData);

        DamageAreaCircleView circleView = GetComponent<DamageAreaCircleView>();
        circleView.Initialize(damageAreaCircle);

        UniTask executeTask = damageAreaCircle.Run();
    }
}
