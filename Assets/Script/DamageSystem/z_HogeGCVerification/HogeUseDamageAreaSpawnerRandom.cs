using UnityEngine;

/// <summary>
/// 見栄えのためにダメージエリアの生成場所をランダムにする
/// 沢山生成している感を視覚的に出すためのクラス
/// </summary>
public class HogeUseDamageAreaSpawnerRandom : MonoBehaviour
{
    [SerializeField] private DamageAreaSpwaner _damageAreaSpawner;
    [SerializeField] private int _spawnNumberPerSecond = 10000;
    [SerializeField] private int _targetSpawnCount = 100000;
    private int _spawnCount = 0;
    private float _spawnTimer;

    private int _spawnType = 1;

    // いずれはScriptableObject化して外部から設定できるようにする
    private DamageAreaData _damageAreaData;

    void Update()
    {
        _spawnTimer += Time.deltaTime * _spawnNumberPerSecond;

        while (_spawnTimer >= 1f)
        {
            _damageAreaData = SetDamageAreaData();
            _damageAreaSpawner.Spawn(_damageAreaData);
            GCPerformanceRecorder.Instance.AddSpawn();

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
        GCPerformanceRecorder.Instance.PrintResult();

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
                frontCenterPos: new Vector3(
                    UnityEngine.Random.Range(-5f, 5f),
                    0f,
                    UnityEngine.Random.Range(-5f, 5f)
                    ),
                size: new Vector3(
                    UnityEngine.Random.Range(-5f, 5f),
                    UnityEngine.Random.Range(-5f, 5f),
                    UnityEngine.Random.Range(-5f, 5f)
                    )
            ),
            new DamageAreaAttackData(),
            new DamageAreaTimeData(
                spawnTime: 1f,
                attackWaitTime: 1f,
                attackTime: 1f,
                fadeOutTime: 1f
            )
        );
            case 1:
                return new DamageAreaData(
            new DamageAreaCircleData(
                centerPosition: new Vector3(
                    UnityEngine.Random.Range(-5f, 5f),
                    0f,
                    UnityEngine.Random.Range(-5f, 5f)
                    ),
                height: 1f,
                radius: UnityEngine.Random.Range(1f, 5f),
                angle: 360f
            ),
            new DamageAreaAttackData(),
            new DamageAreaTimeData(
                spawnTime: 1f,
                attackWaitTime: 1f,
                attackTime: 1f,
                fadeOutTime: 1f
            )
        );

            default:
                throw new System.Exception("Invalid spawn type for damage area.");
        }
    }
}
