using Cysharp.Threading.Tasks;
using System;
using UniRx;

public abstract class DamageArea
{
    public abstract IObservable<DamageAreaStateChanged> OnStateChanged { get; }
    public abstract UniTask Execute();
}

public abstract class DamageArea<TShape> : DamageArea
    where TShape : DamageAreaShapeBaseData
{
    protected DamageAreaData _DamageAreaData;
    protected TShape _ShapeData;

    private readonly Subject<DamageAreaStateChanged> _stateChanged = new();

    public override IObservable<DamageAreaStateChanged> OnStateChanged => _stateChanged;

    public virtual void Initialize(DamageAreaData damageAreaData)
    {
        _DamageAreaData = damageAreaData;

        if (damageAreaData._ShapeData is not TShape shapeData)
        {
            throw new ArgumentException(
                $"Invalid shape data type. " +
                $"Expected {typeof(TShape).Name}, " +
                $"but got {damageAreaData._ShapeData?.GetType().Name ?? "null"}.");
        }
        _ShapeData = shapeData;
    }

    protected void ChangeState(DamageAreaState state)
    {
        _stateChanged.OnNext(
            new DamageAreaStateChanged(
                state,
                _DamageAreaData));
    }

    public override UniTask Execute()
    {
        throw new NotImplementedException();
    }
}
