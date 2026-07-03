using UnityEngine;

/// <summary>
/// ダメージエリアを生成したいと要求する中継クラス
/// 今後、生成条件等が増えた場合にここで制御することを想定している
/// </summary>
public class DamageAreaSpwaner : MonoBehaviour
{
    private DamageAreaInitializer _initializer;

    public void Initilize(DamageAreaInitializer initializer)
    {
        _initializer = initializer;
    }

    public void SpawnDamageArea(DamageAreaData areaData)
    {
        _initializer.DamageAreaInitialize(areaData);
    }
}
