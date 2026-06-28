using DG.Tweening;
using UnityEngine;
using UniRx;


public class DamageAreaCircleView : DamageAreaView
{
    public override void Initialize(DamageArea area)
    {
        DamageArea = area;
        DamageAreaTimeData = area.DamageAreaData.TimeData;

        DamageArea.OnStateChanged
            .Subscribe(state =>
            {
                switch (state._State)
                {
                    case DamageAreaState.Spawn:
                        {
                            transform.localScale = Vector3.zero;
                            transform
                                .DOScale(Vector3.one, DamageAreaTimeData.FadeInTime)
                                .SetEase(Ease.OutBack);
                        }
                        break;
                    case DamageAreaState.FadeOut:
                        {
                            transform
                                .DOScale(Vector3.zero, DamageAreaTimeData.FadeOutTime)
                                .SetEase(Ease.InBack);
                        }
                        break;
                }
            });
    }
}
