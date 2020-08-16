using UnityEngine;

namespace GrayscaleBlock3D
{
    [CreateAssetMenu(fileName = "GlobalVar", menuName = "GrayscaleBlock3D/GlobalVar", order = 0)]
    public sealed class GlobalVar : ScriptableObject
    {
        public int fieldWidth = 7;
        public int fieldHeight = 14;
        public float speedBlockMoving = 0.2f;
        public float speedBlockFalling = 0.1f;
        public Vector3 spawnPosition = new Vector3(4, 4, 0);
        public float[] colors = new float[6] { .5f, 0.01f, 0.2f, 0.4f, 0.7f, 1f };
        public float delayMerge = 0.1f;

        public int startColor = 1;
        public int endColor = 3;

    }
}