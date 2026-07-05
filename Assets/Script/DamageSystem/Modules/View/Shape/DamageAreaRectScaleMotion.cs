using DG.Tweening;
using UnityEngine;
using UniRx;

public class DamageAreaRectScaleMotion : DamageAreaScaleMotion
{
    private DamageAreaRectData _rectData;

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
        _rectData = (DamageAreaRectData)areaData.ShapeData;
    }

    private void OnStateChanged(DamageAreaStateChanged state)
    {
        switch (state.State)
        {
            case DamageAreaState.Spawn:
                {
                    transform.localPosition = _rectData.FrontCenterPos;
                    transform.localScale = new Vector3(
                        0,
                        _rectData.Size.y,
                        _rectData.Size.z);
                    transform
                        .DOScaleX(_rectData.Size.x, TimeData.SpawnTime)
                        .SetEase(Ease.OutSine);
                }
                break;
            case DamageAreaState.FadeOut:
                {
                    transform
                        .DOScaleX(0, TimeData.FadeOutTime)
                        .SetEase(Ease.OutSine);
                }
                break;
        }
    }
}
