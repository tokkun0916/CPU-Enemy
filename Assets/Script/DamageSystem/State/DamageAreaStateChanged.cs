using UnityEngine;

public class DamageAreaStateChanged
{
    public DamageAreaState _State { get; private set; }
    public DamageAreaData _Data { get; private set; }

    public DamageAreaStateChanged(DamageAreaState state, DamageAreaData data)
    {
        _State = state;
        _Data = data;
    }
}
