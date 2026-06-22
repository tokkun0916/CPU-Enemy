using UnityEngine;

/// <summary>
/// ダメージエリアの形状種別
/// </summary>
public enum DamageAreaShape
{
    Circle,
    Rectangle
}

/// <summary>
/// ダメージエリアの設定データ（ScriptableObjectで管理し、Inspector上から柔軟に調整可能）
/// </summary>
[CreateAssetMenu(fileName = "DamageAreaDefinition", menuName = "Game/Damage Area Definition")]
public class DamageAreaDefinition : ScriptableObject
{
    [Header("形状設定")]
    [Tooltip("判定形状: 円形 or 矩形")]
    public DamageAreaShape Shape = DamageAreaShape.Circle;

    [Tooltip("円形の場合: 半径 / 矩形の場合: X方向の半サイズ")]
    public float SizeX = 2f;

    [Tooltip("矩形の場合のみ有効: Z方向の半サイズ")]
    public float SizeZ = 2f;

    [Header("ダメージ設定")]
    [Tooltip("エリア内に滞在し続けた場合、1秒ごとに与えるダメージ量")]
    public float DamagePerSecond = 10f;

    [Tooltip("ダメージエリアが有効な時間（秒）。0以下は無制限")]
    public float Lifetime = 3f;

    [Header("出現演出設定")]
    [Tooltip("DoTweenによるスケールアニメーションにかかる時間（秒）")]
    public float SpawnAnimationDuration = 0.3f;

    [Header("ビジュアル設定")]
    [Tooltip("エリアのマテリアル")]
    public Material AreaMaterial;

    [Tooltip("エリアの色")]
    public Color AreaColor = new Color(1f, 0f, 0f, 0.4f);
}
