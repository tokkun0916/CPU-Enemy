/// <summary>
/// ダメージエリアの状態変更時に通知されるイベントデータ。
/// 状態遷移後の状態と、その時点のダメージエリア情報を保持する。
/// </summary>
public class DamageAreaStateChanged
{
    /// <summary>
    /// 遷移後のダメージエリアの状態
    /// </summary>
    public DamageAreaState State { get; private set; }

    /// <summary>
    /// 状態変更時点のダメージエリア情報
    /// </summary>
    public DamageAreaData Data { get; private set; }

    public DamageAreaStateChanged(DamageAreaState state, DamageAreaData data)
    {
        State = state;
        Data = data;
    }
}
