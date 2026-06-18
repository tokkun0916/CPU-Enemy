using UnityEngine;

namespace DamageArea
{
    [System.Serializable]
    public class CircleAreaData
    {
        [Min(0f)]
        public float Radius = 1f;

        [Range(0f, 360f)]
        public float Angle = 360f;
    }
}