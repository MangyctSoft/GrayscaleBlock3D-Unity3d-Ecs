using UnityEngine;

namespace GrayscaleBlock3D
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "GrayscaleBlock3D/BlockData", order = 0)]
    public class BlockData : ScriptableObject
    {
        public GameObject blockPrefab;
        public float defaultSpeed = 2f;

        public static BlockData LoadFromAssets() => GameObject.Instantiate(Resources.Load("BlockData")) as BlockData;

    }
}