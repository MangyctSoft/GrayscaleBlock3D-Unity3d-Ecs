using UnityEngine;

namespace GrayscaleBlock3D.AppSettings
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "GrayscaleBlock3D/GameConfiguration", order = 0)]
    public class GameConfiguration : ScriptableObject
    {
        public Vector2 SizeField = default;
        public GameObject BlockubePrefab = default;
        public Vector3 CurrentBlockPosition = default;
        public Vector3 PreviewBlockPosition = default;
        public float SpeedMove = default;
        public float SpeedFall = default;
        public GameObject TextBlockMergeUndicatorPrefab = default;
        public float[] BlockColors = default;
        public float DelayMerge = default;
        public float DelayFall = default;
        [Space]
        [Header("Диапозон цветов.")]
        [Tooltip("Start color in the array BlockColors.")]
        public int StartColor = default;
        [Tooltip("End color in the array BlockColors.")]
        public int EndColor = default;
        [Space]
        [Header("Материал для цели.")]
        public Material Green = default;
        public Material Warning = default;
        public Material Normal = default;
    }
}
