using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

public abstract class DamageArea : MonoBehaviour
{
    public DamageAreaData DamageAreaData { get; protected set; }
    public abstract IObservable<DamageAreaStateChanged> OnStateChanged { get; }
}

public abstract class DamageArea<TShape> : DamageArea
    where TShape : DamageAreaShapeBaseData
{
    protected TShape ShapeData;

    private readonly Subject<DamageAreaStateChanged> _stateChanged = new();

    public override IObservable<DamageAreaStateChanged> OnStateChanged => _stateChanged;

    public virtual void Initialize(DamageAreaData damageAreaData)
    {
        DamageAreaData = damageAreaData;

        if (damageAreaData.ShapeData is not TShape shapeData)
        {
            throw new ArgumentException(
                $"Invalid shape data type. " +
                $"Expected {typeof(TShape).Name}, " +
                $"but got {damageAreaData.ShapeData?.GetType().Name ?? "null"}.");
        }
        ShapeData = shapeData;
    }

    protected void ChangeState(DamageAreaState state)
    {
        _stateChanged.OnNext(
            new DamageAreaStateChanged(
                state,
                DamageAreaData));
    }

    public abstract UniTask Run();
}
