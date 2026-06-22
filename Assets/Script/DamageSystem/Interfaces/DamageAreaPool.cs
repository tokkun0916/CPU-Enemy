using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DamageAreaのオブジェクトプール。
/// シングルトンとして機能し、プレイヤー・敵・その他全てのシステムから利用可能。
/// </summary>
public class DamageAreaPool : MonoBehaviour
{
    public static DamageAreaPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [Header("プール設定")]
    [Tooltip("ダメージエリアのPrefab（DamageAreaコンポーネント付き）")]
    [SerializeField] private DamageArea _prefab;

    [Tooltip("起動時に事前生成する数")]
    [SerializeField] private int _initialPoolSize = 10;

    // 待機中（プールに戻っている）インスタンスのスタック
    private readonly Stack<DamageArea> _availableInstances = new();

    // ──────────────────────────────────────────────
    // Unity ライフサイクル
    // ──────────────────────────────────────────────

    private void Start()
    {
        PreWarm(_initialPoolSize);
    }

    // ──────────────────────────────────────────────
    // 公開API
    // ──────────────────────────────────────────────

    /// <summary>
    /// プールからダメージエリアを取得してアクティブ化する。
    /// プールが空の場合は新規生成する。
    /// </summary>
    /// <param name="definition">適用するダメージエリア定義</param>
    /// <param name="spawnPosition">生成する座標</param>
    /// <param name="owner">発動者（プレイヤーや敵のGameObject）</param>
    /// <returns>初期化済みのDamageAreaインスタンス</returns>
    public DamageArea Get(DamageAreaDefinition definition, Vector3 spawnPosition, GameObject owner)
    {
        var instance = _availableInstances.Count > 0
            ? _availableInstances.Pop()
            : CreateNewInstance();

        instance.gameObject.SetActive(true);
        instance.Activate(definition, spawnPosition, owner);
        return instance;
    }

    /// <summary>
    /// 使用済みのDamageAreaをプールに返却する。
    /// DamageArea自身が寿命切れ時に呼び出す。
    /// </summary>
    /// <param name="instance">返却するインスタンス</param>
    public void Return(DamageArea instance)
    {
        instance.Deactivate();
        _availableInstances.Push(instance);
    }

    // ──────────────────────────────────────────────
    // プライベートメソッド
    // ──────────────────────────────────────────────

    /// <summary>起動時に指定数だけインスタンスを事前生成しプールへ格納する</summary>
    private void PreWarm(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var instance = CreateNewInstance();
            instance.gameObject.SetActive(false);
            _availableInstances.Push(instance);
        }
    }

    /// <summary>新規インスタンスを生成する（プール不足時のフォールバック）</summary>
    private DamageArea CreateNewInstance()
    {
        var instance = Instantiate(_prefab, transform);
        instance.gameObject.SetActive(false);
        return instance;
    }
}
