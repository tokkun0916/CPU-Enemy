using UnityEngine;

public abstract class DamageAreaView : MonoBehaviour
{
    protected DamageArea DamageArea;
    protected DamageAreaTimeData DamageAreaTimeData;

    public abstract void Initialize(DamageArea area);
}