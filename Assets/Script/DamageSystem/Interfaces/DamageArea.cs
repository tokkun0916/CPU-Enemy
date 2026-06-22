using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 1つのダメージエリアを管理するコンポーネント。
/// - オブジェクトプールから取得・返却される想定
/// - DoTweenで生成時にスケールアニメーションを再生
/// - 円形・矩形の判定に対応
/// </summary>
public class DamageArea : MonoBehaviour
{
    // ──────────────────────────────────────────────
    // フィールド
    // ──────────────────────────────────────────────

    [Header("参照（自動設定）")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter   _meshFilter;

    // 現在適用されている定義データ
    private DamageAreaDefinition _definition;

    // このエリアの発動者（味方ダメージを防ぐ等に利用可能）
    private GameObject _owner;

    // 残り有効時間カウンター
    private float _remainingLifetime;

    // DoTweenのアニメーション参照（Kill用）
    private Tween _spawnTween;

    // 同フレーム内で重複ダメージを与えないためのセット
    private readonly HashSet<IDamageable> _damagedThisFrame = new();

    // ──────────────────────────────────────────────
    // 公開API
    // ──────────────────────────────────────────────

    /// <summary>
    /// プールからオブジェクトを取得した後、初期化と生成アニメーションを開始する
    /// </summary>
    /// <param name="definition">適用するダメージエリア定義</param>
    /// <param name="spawnPosition">生成座標（Y軸は地面高さに合わせる）</param>
    /// <param name="owner">このエリアを発動したGameObject（プレイヤー or 敵）</param>
    public void Activate(DamageAreaDefinition definition, Vector3 spawnPosition, GameObject owner)
    {
        _definition = definition;
        _owner      = owner;

        transform.position = spawnPosition;
        _remainingLifetime = definition.Lifetime > 0 ? definition.Lifetime : float.MaxValue;

        ApplyVisual();
        PlaySpawnAnimation();
    }

    /// <summary>
    /// プールへ返却する前の後処理
    /// </summary>
    public void Deactivate()
    {
        _spawnTween?.Kill();
        gameObject.SetActive(false);
    }

    // ──────────────────────────────────────────────
    // Unity ライフサイクル
    // ──────────────────────────────────────────────

    private void Update()
    {
        _damagedThisFrame.Clear();

        // ライフタイム管理
        _remainingLifetime -= Time.deltaTime;
        if (_remainingLifetime <= 0f)
        {
            DamageAreaPool.Instance.Return(this);
            return;
        }

        // 判定実行
        switch (_definition.Shape)
        {
            case DamageAreaShape.Circle:
                CheckCircleOverlap();
                break;
            case DamageAreaShape.Rectangle:
                CheckRectangleOverlap();
                break;
        }
    }

    // ──────────────────────────────────────────────
    // プライベートメソッド
    // ──────────────────────────────────────────────

    /// <summary>円形OverlapSphereで範囲内のIDamageableにダメージを与える</summary>
    private void CheckCircleOverlap()
    {
        var hits = Physics.OverlapSphere(transform.position, _definition.SizeX);
        ProcessHits(hits);
    }

    /// <summary>矩形OverlapBoxで範囲内のIDamageableにダメージを与える</summary>
    private void CheckRectangleOverlap()
    {
        var halfExtents = new Vector3(_definition.SizeX, 1f, _definition.SizeZ);
        var hits = Physics.OverlapBox(transform.position, halfExtents, transform.rotation);
        ProcessHits(hits);
    }

    /// <summary>OverlapHitの結果を走査し、IDamageableにダメージを適用する</summary>
    private void ProcessHits(Collider[] hits)
    {
        foreach (var hit in hits)
        {
            // 発動者自身はスキップ
            if (hit.gameObject == _owner) continue;

            if (!hit.TryGetComponent<IDamageable>(out var damageable)) continue;

            // 同フレームで既にダメージ済みならスキップ
            if (_damagedThisFrame.Contains(damageable)) continue;

            damageable.TakeDamage(_definition.DamagePerSecond * Time.deltaTime, this);
            _damagedThisFrame.Add(damageable);
        }
    }

    /// <summary>定義データをMeshRendererに反映する</summary>
    private void ApplyVisual()
    {
        if (_meshRenderer == null) return;

        // MaterialPropertyBlockで共有マテリアルを汚染しない
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", _definition.AreaColor);
        _meshRenderer.SetPropertyBlock(block);
    }

    /// <summary>スケールを0→1にDoTweenでアニメーション</summary>
    private void PlaySpawnAnimation()
    {
        _spawnTween?.Kill();
        transform.localScale = Vector3.zero;
        _spawnTween = transform
            .DOScale(Vector3.one, _definition.SpawnAnimationDuration)
            .SetEase(Ease.OutBack);
    }
}
