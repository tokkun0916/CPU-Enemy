using UnityEngine;

namespace DamageSystem.Data
{
    [System.Serializable]
    public class DamageAreaData
    {
        // ダメージエリア

        public int Damage;
        public float AttackInterval;

        public float SpawnTime;
        public float AttackTime;
        public float DeleteTime;
    }
}