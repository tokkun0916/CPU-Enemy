/// <summary>
/// ダメージエリアからダメージを受けられるオブジェクトが実装すべきインターフェース。
/// プレイヤー・敵の両方に実装することで、ダメージエリア側は受け手の種類を意識しなくて済む。
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ダメージを受けたときに呼ばれる
    /// </summary>
    /// <param name="damage">与えるダメージ量</param>
    /// <param name="source">ダメージ元のDamageArea（null許容）</param>
    void TakeDamage(float damage, DamageArea source = null);
}
