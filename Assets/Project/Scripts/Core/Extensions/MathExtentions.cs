using UnityEngine;

namespace Project.Scripts.Core.Extensions
{
    public static class MathExtentions
    {
        public static float NormalizeToOne(this float value, float maxValue)
        {
            return value / maxValue;
        }
        
        public static Vector2 ArrayToVector2(this float[] value, int startingIndex)
        {
            var vector = new Vector2(value[0 + startingIndex], value[1 + startingIndex]);
            return vector;
        }
        
        public static Vector2 ArrayToVector2(this float[] value)
        {
            var vector = new Vector2(value[0], value[1]);
            return vector;
        }
    }
}