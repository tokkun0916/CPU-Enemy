public abstract class DamageArea
{
    public abstract void Execute();
}

public abstract class DamageArea<TShape> : DamageArea
    where TShape : DamageAreaShapeBaseData
{
    protected DamageAreaData _AttackData;
    protected TShape _ShapeData;

    public virtual void Initialize(DamageAreaData attackData)
    {
        _AttackData = attackData;
        _ShapeData = (TShape)attackData._ShapeData;
    }
}
