using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージエリアのオブジェクトプール
/// 1種類のPrefab(RectやCircleなど)を管理する。
/// </summary>
public class DamageAreaPool
{
    /// <summary>
    /// プール対象のPrefab
    /// </summary>
    private readonly GameObject _prefab;

    /// <summary>
    /// プールしたオブジェクトの親
    /// </summary>
    private readonly Transform _parent;

    /// <summary>
    /// 未使用オブジェクト
    /// </summary>
    private readonly Queue<DamageAreaRoot> _pool = new();

    public DamageAreaPool(
        GameObject prefab,
        Transform parent,
        int defaultCapacity = 0)
    {
        _prefab = prefab;
        _parent = parent;

        // 必要数を先に生成しておく
        for (int i = 0; i < defaultCapacity; i++)
        {
            CreateInstance();
        }
    }

    /// <summary>
    /// オブジェクトを取得する
    /// </summary>
    public DamageAreaRoot Get()
    {
        if (_pool.Count == 0)
        {
            CreateInstance();
        }

        DamageAreaRoot root = _pool.Dequeue();
        root.gameObject.SetActive(true);

        return root;
    }

    /// <summary>
    /// オブジェクトをプールへ返却する
    /// </summary>
    public void Release(DamageAreaRoot root)
    {
        root.ResetObject();

        root.gameObject.SetActive(false);

        _pool.Enqueue(root);
    }

    /// <summary>
    /// 新しいオブジェクトを生成してプールへ追加する
    /// </summary>
    private void CreateInstance()
    {
        DamageAreaRoot root =
             Object.Instantiate(_prefab, _parent)
            .GetComponent<DamageAreaRoot>();

        root.SetOwnerPool(this);

        root.gameObject.SetActive(false);

        _pool.Enqueue(root);
    }
}