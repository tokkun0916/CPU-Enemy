using UnityEngine;

/// <summary>
/// ダメージエリアの初期化を行うクラス
/// オブジェクトプールからダメージエリアを取得し、初期化して実行する
/// </summary>
public class DamageAreaInitializer : MonoBehaviour
{
    private readonly HogeDamageAreaInstantiate _hogeDamageAreaInstantiate;

    public DamageAreaInitializer(HogeDamageAreaInstantiate hogeDamageAreaInstantiate)
    {
        _hogeDamageAreaInstantiate = hogeDamageAreaInstantiate;
    }

    public void DamageAreaInitialize(DamageAreaData areaData)
    {
        DamageAreaRunner runner = _hogeDamageAreaInstantiate.Instantiate(areaData.ShapeData);
        runner.Initialize(areaData);
        _ = runner.Run();
    }
}
