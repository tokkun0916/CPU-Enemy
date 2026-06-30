using DG.Tweening;
using UnityEngine;
using UniRx;


public class DamageAreaCircleScaleMotion : DamageAreaScaleMotion
{
    public override void Initialize(DamageAreaRunner areaRunner)
    {
        Runner = areaRunner;
        TimeData = areaRunner.DamageAreaData.TimeData;

        Runner.OnStateChanged
            .Subscribe(state =>
            {
                switch (state._State)
                {
                    case DamageAreaState.Spawn:
                        {
                            transform.localScale = Vector3.zero;
                            transform
                                .DOScale(Vector3.one, TimeData.SpawnTime)
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
            });
    }
}
