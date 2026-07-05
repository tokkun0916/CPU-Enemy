using DG.Tweening;
using UnityEngine;
using UniRx;


public class DamageAreaCircleScaleMotion : DamageAreaScaleMotion
{
    private DamageAreaCircleData _circleData;

    private void Awake()
    {
        Runner = GetComponent<DamageAreaRunner>();

        Runner.OnStateChanged
            .Subscribe(OnStateChanged)
            .AddTo(this);
    }

    public override void Initialize(DamageAreaData areaData)
    {
        TimeData = areaData.TimeData;
        _circleData = (DamageAreaCircleData)areaData.ShapeData;
    }

    private void OnStateChanged(DamageAreaStateChanged state)
    {
        switch (state.State)
        {
            case DamageAreaState.Spawn:
                {
                    transform.localPosition = _circleData.CenterPosition;
                    transform.localScale = Vector3.zero;
                    transform
                        .DOScale(Vector3.one * _circleData.Radius, TimeData.SpawnTime)
                        .SetEase(Ease.OutSine);
                }
                break;
            case DamageAreaState.FadeOut:
                {
                    transform
                        .DOScale(Vector3.zero, TimeData.FadeOutTime)
                        .SetEase(Ease.OutSine);
                }
                break;
        }
    }
}
