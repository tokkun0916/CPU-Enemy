using UnityEngine;

// ============================================================
// 使用例：プレイヤー
// ============================================================

/// <summary>
/// プレイヤーのサンプル実装。
/// IDamageableを実装しているためダメージエリアからダメージを受けられる。
/// </summary>
public class PlayerExample : MonoBehaviour, IDamageable
{
    [Header("プレイヤー設定")]
    [SerializeField] private float _maxHealth = 100f;

    [Header("ダメージエリア設定")]
    [Tooltip("プレイヤーが発動するダメージエリアの定義")]
    [SerializeField] private DamageAreaDefinition _attackAreaDefinition;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        // テスト用：スペースキーで足元にダメージエリアを発動
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnDamageArea();
        }
    }

    /// <summary>ダメージエリアを足元に召喚する</summary>
    private void SpawnDamageArea()
    {
        DamageAreaPool.Instance.Get(
            definition:     _attackAreaDefinition,
            spawnPosition:  transform.position,
            owner:          gameObject
        );
    }

    // IDamageable の実装
    public void TakeDamage(float damage, DamageArea source = null)
    {
        _currentHealth -= damage;
        Debug.Log($"[Player] ダメージ受: {damage:F1} / 残HP: {_currentHealth:F1}");

        if (_currentHealth <= 0f) OnDead();
    }

    private void OnDead()
    {
        Debug.Log("[Player] 死亡");
        // 死亡処理...
    }
}


// ============================================================
// 使用例：敵
// ============================================================

/// <summary>
/// 敵キャラクターのサンプル実装。
/// プレイヤーと同じくIDamageableを実装するためダメージエリアの影響を受ける。
/// </summary>
public class EnemyExample : MonoBehaviour, IDamageable
{
    [Header("敵設定")]
    [SerializeField] private float _maxHealth = 50f;

    [Header("ダメージエリア設定")]
    [Tooltip("敵が発動するダメージエリアの定義")]
    [SerializeField] private DamageAreaDefinition _attackAreaDefinition;

    [Tooltip("ダメージエリアを発動する間隔（秒）")]
    [SerializeField] private float _attackInterval = 3f;

    private float _currentHealth;
    private float _attackTimer;

    private void Start()
    {
        _currentHealth = _maxHealth;
        // ランダムオフセットを加えて複数敵が同時発動しないようにする
        _attackTimer = Random.Range(0f, _attackInterval);
    }

    private void Update()
    {
        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f)
        {
            SpawnDamageArea();
            _attackTimer = _attackInterval;
        }
    }

    /// <summary>足元（または任意の座標）にダメージエリアを召喚する</summary>
    private void SpawnDamageArea()
    {
        DamageAreaPool.Instance.Get(
            definition:    _attackAreaDefinition,
            spawnPosition: transform.position,
            owner:         gameObject
        );
    }

    // IDamageable の実装
    public void TakeDamage(float damage, DamageArea source = null)
    {
        _currentHealth -= damage;
        Debug.Log($"[Enemy:{name}] ダメージ受: {damage:F1} / 残HP: {_currentHealth:F1}");

        if (_currentHealth <= 0f) OnDead();
    }

    private void OnDead()
    {
        Debug.Log($"[Enemy:{name}] 撃破");
        // 撃破処理...
        Destroy(gameObject);
    }
}
