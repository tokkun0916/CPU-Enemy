using UnityEngine;

namespace DamageArea
{
    /// <summary>
    /// 円形を指定角度の見た目に制御するクラス
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class CircleAreaRenderer : MonoBehaviour
    {
        private static readonly int AngleID = Shader.PropertyToID("_Angle");
        private static readonly int RadiusID = Shader.PropertyToID("_Radius");

        private Renderer _renderer;
        private MaterialPropertyBlock _propertyBlock;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _propertyBlock = new MaterialPropertyBlock();
        }

        /// <summary>
        /// 円形ダメージエリアの表示データを設定する
        /// </summary>
        public void SetData(CircleAreaData data)
        {
            _renderer.GetPropertyBlock(_propertyBlock);

            _propertyBlock.SetFloat(AngleID, data.Angle);
            _propertyBlock.SetFloat(RadiusID, data.Radius);

            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}