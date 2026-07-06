using UnityEngine;

public class DamageAreaRoot : MonoBehaviour
{
    public DamageAreaRunner Runner {  get; private set; }
    public DamageAreaScaleMotion ScaleMotion { get; private set; }

    private DamageAreaPool _ownerPool;

    private void Awake()
    {
        Runner = GetComponent<DamageAreaRunner>();
        ScaleMotion = GetComponent<DamageAreaScaleMotion>();
    }

    public void Initialize(DamageAreaData data)
    {
        Runner.Initialize(data);
        ScaleMotion.Initialize(data);
    }

    public void ResetObject()
    {
        Runner.ResetObject();
        ScaleMotion.ResetObject();
    }

    public void Run()
    {
        _ = Runner.Run();
    }

    public void SetOwnerPool(DamageAreaPool ownerPool)
    {
        _ownerPool = ownerPool;
    }

    public void Release()
    {
        _ownerPool.Release(this);
    }
}
