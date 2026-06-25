using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class HogeSubscribe : MonoBehaviour
{
    public void Bind(DamageArea area)
    {
        area.OnStateChanged
            .Subscribe(state =>
            {

            });
    }
}
