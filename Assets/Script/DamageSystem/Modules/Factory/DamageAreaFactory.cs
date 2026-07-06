using UnityEngine;

/// <summary>
/// ダメージエリアの初期化を行うクラス
/// ！将来的にオブジェクトプール！からダメージエリアを取得し、初期化して実行する
/// ！現在はGCの検証のためにInstantiateしている！
/// </summary>
public class DamageAreaFactory : MonoBehaviour
{
    private DamageAreaPoolManager _poolManager;
    private DamageAreaRoot damageAreaRoot;

    public void Initialize(DamageAreaPoolManager pool)
    {
        _poolManager = pool;
    }

    /// <summary>
    /// ダメージエリアの初期化と実行を行う
    /// </summary>
    /// <param name="areaData"></param>
    public void Create(DamageAreaData areaData)
    {
        damageAreaRoot = _poolManager.Get(areaData.ShapeData);

        // 各機能を初期化して実行する
        damageAreaRoot.Initialize(areaData);
        damageAreaRoot.Run();
    }
}
