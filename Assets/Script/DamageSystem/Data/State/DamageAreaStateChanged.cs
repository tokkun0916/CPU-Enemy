/// <summary>
/// ダメージエリアの状態変更時に通知されるイベントデータ。
/// 状態遷移後の状態と、その時点のダメージエリア情報を保持する。
/// </summary>
public readonly struct DamageAreaStateChanged
{
    /// <summary>
    /// 遷移後のダメージエリアの状態
    /// </summary>
    public DamageAreaState State { get;}

    /// <summary>
    /// 状態変更時点のダメージエリア情報
    /// </summary>
    public DamageAreaData Data { get;}

    public DamageAreaStateChanged(DamageAreaState state, DamageAreaData data)
    {
        State = state;
        Data = data;
    }
}
