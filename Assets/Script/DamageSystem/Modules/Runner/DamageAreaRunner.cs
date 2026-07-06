using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

/// <summary>
/// ダメージエリアのライフサイクルを管理するクラス。
/// FadeIn → AttackWait → Attack → FadeOut → Released の順に状態を遷移し、
/// 各状態の変更を購読者へ通知する。
/// </summary>
public class DamageAreaRunner : MonoBehaviour
{
    public DamageAreaData DamageAreaData { get; private set; }

    private readonly Subject<DamageAreaStateChanged> _stateChanged = new();

    private DamageAreaRoot _root;

    /// <summary>
    /// ダメージエリアの状態変更通知
    /// </summary>
    public IObservable<DamageAreaStateChanged> OnStateChanged => _stateChanged;

    private void Awake()
    {
        _root = GetComponent<DamageAreaRoot>();
    }

    public void Initialize(DamageAreaData data)
    {
        DamageAreaData = data;
    }

    /// <summary>
    /// ダメージエリアのライフサイクルを実行する
    /// </summary>
    public async UniTask Run()
    {
        ChangeState(DamageAreaState.Spawn);
        await UniTask.Delay((int)(DamageAreaData.TimeData.SpawnTime * 1000));

        ChangeState(DamageAreaState.AttackWait);
        await UniTask.Delay((int)(DamageAreaData.TimeData.AttackWaitTime * 1000));

        ChangeState(DamageAreaState.Attack);
        await UniTask.Delay((int)(DamageAreaData.TimeData.AttackTime * 1000));

        ChangeState(DamageAreaState.FadeOut);
        await UniTask.Delay((int)(DamageAreaData.TimeData.FadeOutTime * 1000));

        // ダメージエリアのライフサイクルが終了したことを通知する
        _root.Release();
    }

    private void ChangeState(DamageAreaState state) 
    {
        _stateChanged.OnNext(new DamageAreaStateChanged(state, DamageAreaData));
    }

    public void ResetObject()
    {
        DamageAreaData = null;
    }

    private void OnDestroy()
    {
        _stateChanged.OnCompleted();
        _stateChanged.Dispose();
    }
}