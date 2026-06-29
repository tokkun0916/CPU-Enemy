using DG.Tweening;
using UnityEngine;
using UniRx;

public class DamageAreaRectView : DamageAreaView
{
    private DamageAreaRectData _rectData;
    public override void Initialize(DamageAreaRunner areaRunner)
    {
        Runner = areaRunner;
        TimeData = areaRunner.DamageAreaData.TimeData;
        _rectData = (DamageAreaRectData)areaRunner.DamageAreaData.ShapeData;

        Runner.OnStateChanged
            .Subscribe(state =>
            {
                switch (state._State)
                {
                    case DamageAreaState.FadeIn:
                        {
                            transform.localPosition = _rectData.StartingPos;

                            transform.localScale = new Vector3(0, _rectData.Size.y, _rectData.Size.z);
                            transform
                                .DOScaleX(_rectData.Size.x, TimeData.FadeInTime)
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
            });
    }
}
