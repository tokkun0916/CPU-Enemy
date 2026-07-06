using UnityEngine;

public class DamageAreaRectData : DamageAreaShapeBaseData
{
    public override DamageAreaShapeType ShapeType => DamageAreaShapeType.Rect;

    public Vector3 FrontCenterPos;
    public Vector3 Size;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="frontCenterPos">ダメージエリア生成の基準となる手前側中央の座標</param>
    /// <param name="size">ダメージエリアのサイズ</param>
    public DamageAreaRectData(
        Vector3 frontCenterPos,
        Vector3 size)
    {
        FrontCenterPos = frontCenterPos;
        Size = size;
    }
}
