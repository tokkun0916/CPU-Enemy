using System.Collections.Generic;
using UnityEngine;

public class DamageAreaManager : MonoBehaviour
{
    [Header("オブジェクトプール設定")]
    [SerializeField] Transform _poolParent;
    [SerializeField] GameObject _prefabRect;
    [SerializeField] GameObject _prefabCircle;
    [SerializeField] int _poolSize = 6000;

    [Header("ダメージエリア生成設定")]
    [SerializeField] DamageAreaSpwaner _spawner;
    [SerializeField] DamageAreaFactory _factory;

    private DamageAreaPoolManager _manager;

    void Start()
    {
        // オブジェクトプールの初期化
        _manager = new();
        _manager.Register(
            DamageAreaShapeType.Rect,
            new DamageAreaPool(_prefabRect, _poolParent,
            _poolSize));
        _manager.Register(
            DamageAreaShapeType.Circle,
            new DamageAreaPool(_prefabCircle, _poolParent,
            _poolSize));

        _factory.Initialize(_manager);
        _spawner.Initialize(_factory);
    }
}
