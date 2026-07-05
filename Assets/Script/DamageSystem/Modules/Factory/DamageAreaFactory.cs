using UnityEngine;

/// <summary>
/// ダメージエリアの初期化を行うクラス
/// ！将来的にオブジェクトプール！からダメージエリアを取得し、初期化して実行する
/// ！現在はGCの検証のためにInstantiateしている！
/// </summary>
public class DamageAreaFactory : MonoBehaviour
{
    private HogeDamageAreaInstantiate _hogeDamageAreaInstantiate;

    public void Initialize(HogeDamageAreaInstantiate hogeDamageAreaInstantiate)
    {
        _hogeDamageAreaInstantiate = hogeDamageAreaInstantiate;
    }

    /// <summary>
    /// ダメージエリアの初期化と実行を行う
    /// </summary>
    /// <param name="areaData"></param>
    public void Create(DamageAreaData areaData)
    {
        // Instantiateしているが、将来的にはオブジェクトプールから取得する
        GameObject damageArea = _hogeDamageAreaInstantiate.Instantiate(areaData.ShapeData);

        // ダメージエリアのライフサイクルをステイト管理するクラスを初期化する
        DamageAreaRunner runner = damageArea.GetComponent<DamageAreaRunner>();
        runner.Initialize(areaData);

        // ダメージエリアの各機能を初期化＋ステイトを監視する
        DamageAreaScaleMotion motion = damageArea.GetComponent<DamageAreaScaleMotion>();
        motion.Initialize(areaData);

        _ = runner.Run();
    }
}
