using UnityEngine;

public abstract class DamageAreaView : MonoBehaviour
{
    protected DamageAreaRunner Runner;
    protected DamageAreaTimeData TimeData;

    public abstract void Initialize(DamageAreaRunner area);
}