using System;
using System.Collections.Generic;

/// <summary>
/// ダメージエリアのプールを管理するクラス
/// ShapeDataから適切なプールを選択する。
/// </summary>
public class DamageAreaPoolManager
{
    /// <summary>
    /// ダメージエリアの種類ごとのプール
    /// </summary>
    private readonly Dictionary<DamageAreaShapeType, DamageAreaPool> _pools = new();

    /// <summary>
    /// プールを登録する
    /// </summary>
    public void Register(
        DamageAreaShapeType shapeType,
        DamageAreaPool pool)
    {
        if (_pools.ContainsKey(shapeType))
        {
            throw new InvalidOperationException(
                $"{shapeType} は既に登録されています。");
        }

        _pools.Add(shapeType, pool);
    }

    /// <summary>
    /// ShapeDataに対応するダメージエリアを取得する
    /// </summary>
    public DamageAreaRoot Get(DamageAreaShapeBaseData shapeData)
    {
        if (!_pools.TryGetValue(shapeData.ShapeType, out DamageAreaPool pool))
        {
            throw new InvalidOperationException(
                $"{shapeData.ShapeType} のプールが登録されていません。");
        }

        return pool.Get();
    }
}