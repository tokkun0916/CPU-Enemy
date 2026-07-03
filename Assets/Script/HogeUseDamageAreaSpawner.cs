using UnityEngine;
using Cysharp.Threading.Tasks;

public class HogeUseDamageAreaSpawner : MonoBehaviour
{
    void Start()
    {
        DamageAreaData areaData = new DamageAreaData();
        areaData.ShapeData = new DamageAreaRectData
        {
            StartingPos = Vector3.zero,
            Size = new Vector3(1, 1, 3)
        };
        areaData.TimeData = new DamageAreaTimeData
        {
            SpawnTime = 1.0f,
            AttackTime = 2.0f,
            FadeOutTime = 1.5f
        };

        // UniRxのSubjectを使ってDamageAreaの状態変化を監視し、状態に応じて処理を行う
        DamageAreaRunner damageAreaRunner = GetComponent<DamageAreaRunner>();
        damageAreaRunner.Initialize(areaData);

        DamageAreaRectScaleMotion rectData = GetComponent<DamageAreaRectScaleMotion>();
        rectData.Initialize(damageAreaRunner);

        UniTask executeTask = damageAreaRunner.Run();
    }
}
