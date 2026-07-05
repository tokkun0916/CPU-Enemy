using UnityEngine;

/// <summary>
/// GCがどの程度ラグに影響するか気になった為
/// 限界までダメージエリアを生成削除を繰り返し
/// オブジェクトプールに変更した時のパフォーマンス改善の参考にする
/// </summary>
public class HogeUseDamageAreaSpawner : MonoBehaviour
{
    [SerializeField] private DamageAreaSpwaner _damageAreaSpawner;
    [SerializeField] private int _spawnNumberPerSecond = 20;
    private float _spawnTimer;

    void Update()
    {
        _spawnTimer += Time.deltaTime * _spawnNumberPerSecond;

        while (_spawnTimer >= 1f)
        {
            DamageAreaData data = RandomSetDamageAreaData();
            _damageAreaSpawner.Spawn(data);

            _spawnTimer -= 1f;
        }
    }

    private DamageAreaData RandomSetDamageAreaData()
    {
        // ランダムで矩形か円形のダメージエリアを生成する
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                return new DamageAreaData(
                    new DamageAreaRectData(
                        frontCenterPos: new Vector3(
                            Random.Range(-5f, 5f),
                            0f,
                            Random.Range(-5f, 5f)),
                        size: new Vector3(
                            Random.Range(1f, 5f),
                            Random.Range(1f, 5f),
                            Random.Range(1f, 5f)
                        )
                    ),
                    new DamageAreaAttackData(),
                    new DamageAreaTimeData(
                        spawnTime:       Random.Range(0.5f, 2f),
                        attackWaitTime:  Random.Range(0.5f, 2f),
                        attackTime:      Random.Range(0.5f, 2f),
                        fadeOutTime:     Random.Range(0.5f, 2f)
                    )
                );
            case 1:
                return new DamageAreaData(
                    new DamageAreaCircleData(
                        centerPosition: new Vector3(
                            Random.Range(-5f, 5f),
                            0f,
                            Random.Range(-5f, 5f)),
                        height: Random.Range(1f, 5f),
                        radius: Random.Range(1f, 5f),
                        angle: 360f
                    ),
                    new DamageAreaAttackData(),
                    new DamageAreaTimeData(
                        spawnTime:       Random.Range(0.5f, 2f),
                        attackWaitTime:  Random.Range(0.5f, 2f),
                        attackTime:      Random.Range(0.5f, 2f),
                        fadeOutTime:     Random.Range(0.5f, 2f)
                    )
                );
            default:
                throw new System.Exception("Invalid random value for damage area type.");
        }
    }
}
