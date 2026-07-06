using UnityEngine;

public abstract class DamageAreaScaleMotion : MonoBehaviour
{
    protected DamageAreaRunner Runner;
    protected DamageAreaTimeData TimeData;

    public abstract void Initialize(DamageAreaData area);

    public abstract void ResetObject();
}