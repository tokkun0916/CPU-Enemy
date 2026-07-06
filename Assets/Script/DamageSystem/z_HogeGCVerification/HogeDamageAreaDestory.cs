using UnityEngine;
using UniRx;

/// <summary>
/// GCがどの程度ラグに影響するか気になった為
/// あえて毎回削除する
/// </summary>
public class HogeDamageAreaDestory : MonoBehaviour
{
    private DamageAreaRunner _runner;

    private void Awake()
    {
        _runner = GetComponent<DamageAreaRunner>();

        _runner.OnStateChanged
            .Subscribe(OnStateChanged)
            .AddTo(this);
    }

    private void OnStateChanged(DamageAreaStateChanged state)
    {
        /*if (state.State == DamageAreaState.Released)
        {
            Destroy(gameObject);
        }*/
    }
}
