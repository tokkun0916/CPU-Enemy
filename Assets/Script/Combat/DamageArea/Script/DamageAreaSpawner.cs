using UnityEngine;

namespace DamageArea
{
    /// <summary>
    /// ダメージエリアを生成するクラス
    /// </summary>
    public class DamageAreaSpawner : MonoBehaviour
    {
        [Header("Circle Type")]
        [SerializeField] private GameObject _C_60;
        [SerializeField] private GameObject _C_120;
        [SerializeField] private GameObject _C_360;
        [Header("Rect Type")]
        [SerializeField] private GameObject _R_Center;
        [SerializeField] private GameObject _R_End;

        #region SpawnBase

        /// <summary>
        /// ダメージエリアを生成する
        /// </summary>
        /// <param name="prefab">生成するDamageAreaのプレハブ</param>
        /// <param name="Pos">生成位置</param>
        /// <param name="data">DamageAreaのデータ</param>
        public void Spawn(GameObject prefab, Vector3 Pos, DamageAreaData data)
        {
            GameObject area = Instantiate(prefab, Pos, transform.rotation);
            area.transform.localScale = Vector3.one * data.Size;

            if(area.TryGetComponent(out DamageAreaMotion motion))
                motion.SetData(data);
            if(area.TryGetComponent(out AttackBase attack))
                attack.SetData(data);
        }

        #endregion

        #region CircleType

        /// <summary>
        /// 60度のダメージエリアを生成する
        /// </summary>
        /// <param name="data">DamageAreaのデータ</param>
        public void Spawn60(DamageAreaData data)
            => Spawn(_C_60, Vector3.zero, data);

        /// <summary>
        /// 120度のダメージエリアを生成する
        /// </summary>
        /// <param name="data">DamageAreaのデータ</param>
        public void Spawn120(DamageAreaData data)
            => Spawn(_C_120, Vector3.zero, data);

        /// <summary>
        /// 360度のダメージエリアを生成する
        /// </summary>
        /// <param name="addPos">追加の生成位置</param>
        /// <param name="data">DamageAreaのデータ</param>
        public void Spawn360(Vector3 addPos, DamageAreaData data)
            => Spawn(_C_360, addPos, data);

        #endregion

        #region RectType

        /// <summary>
        /// 指定した長さのダメージエリアを生成する
        /// </summary>
        /// <param name="length">長さ</param>
        /// <param name="data">DamageAreaのデータ</param>
        public void SpawnRect(int length, DamageAreaData data)
        {
            Vector3 rectPos = Vector3.zero; // 生成する度に更新される位置
            float angle = transform.localEulerAngles.y * Mathf.Deg2Rad; // 回転を考慮して角度を計算
            for (int i = 1; i < length; i++)
            {
                float spawnDist = i * data.Size;
                rectPos.x = spawnDist * Mathf.Sin(angle);
                rectPos.z = spawnDist * Mathf.Cos(angle);
                SpawnRectCenter(rectPos, data);
            }
            SpawnRectEnd(rectPos, data);
        }

        private void SpawnRectCenter(Vector3 addPos, DamageAreaData data)
            => Spawn(_R_Center, addPos, data);

        private void SpawnRectEnd(Vector3 addPos, DamageAreaData data)
            => Spawn(_R_End, addPos, data);

        #endregion
    }
}