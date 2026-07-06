using UnityEngine;

public class DamageAreaCircleData : DamageAreaShapeBaseData
{
    public override DamageAreaShapeType ShapeType => DamageAreaShapeType.Circle;

    public Vector3 CenterPosition;
    public float Height;
    public float Radius;
    public float Angle;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="centerPosition">円の中心座標</param>
    /// <param name="height">円の高さ</param>
    /// <param name="radius">円の半径</param>
    /// <param name="angle">円の内角(0~360)</param>
    public DamageAreaCircleData(
        Vector3 centerPosition,
        float height,
        float radius,
        float angle)
    {
        CenterPosition = centerPosition;
        Height = height;
        Radius = radius;
        Angle = angle;
    }
}
