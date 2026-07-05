using UnityEngine;

/// <summary>
/// GCがどの程度ラグに影響するか気になった為
/// 限界までダメージエリアを生成削除を繰り返し
/// オブジェクトプールに変更した時のパフォーマンス改善の参考にする
/// </summary>
public class HogeUseDamageAreaSpawner : MonoBehaviour
{
    [SerializeField] private DamageAreaSpwaner _damageAreaSpawner;
    [SerializeField] private int _spawnNumberPerSecond = 10000;
    [SerializeField] private int _targetSpawnCount = 100000;
    private int _spawnCount = 0;
    private float _spawnTimer;

    private int _spawnType = 1;

    void Update()
    {
        _spawnTimer += Time.deltaTime * _spawnNumberPerSecond;

        while (_spawnTimer >= 1f)
        {
            DamageAreaData data = SetDamageAreaData();
            _damageAreaSpawner.Spawn(data);

            _spawnTimer -= 1f;

            _spawnCount++;
        }
        if (_spawnCount >= _targetSpawnCount)
        {
            EndTest();
        }
    }

    private void EndTest()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private DamageAreaData SetDamageAreaData()
    {
        // 検証用に同一の結果となるようにダメージエリアの形状を交互に切り替える
        _spawnType *= -1;
        switch (_spawnType)
        {
            case -1:
                return new DamageAreaData(
                    new DamageAreaRectData(
                        frontCenterPos: Vector3.zero,
                        size:           Vector3.one
                    ),
                    new DamageAreaAttackData(),
                    new DamageAreaTimeData(
                        spawnTime:      1f,
                        attackWaitTime: 1f,
                        attackTime:     1f,
                        fadeOutTime:    1f
                    )
                );
            case 1:
                return new DamageAreaData(
                    new DamageAreaCircleData(
                        centerPosition: Vector3.zero,
                        height: 1f,
                        radius: 1f,
                        angle:  360f
                    ),
                    new DamageAreaAttackData(),
                    new DamageAreaTimeData(
                        spawnTime:       1f,
                        attackWaitTime:  1f,
                        attackTime:      1f,
                        fadeOutTime:     1f
                    )
                );
            default:
                throw new System.Exception("Invalid spawn type for damage area.");
        }
    }
}
