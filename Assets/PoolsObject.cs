using GrayscaleBlock3D.Pooling;

namespace GrayscaleBlock3D
{
    public class PoolsObject
    {
        private const string pathBlock = "Prefabs/Block";
        private const string pathBoom = "Prefabs/Boom";

        public PoolContainer Blocks { get; }
        public PoolContainer Booms { get; }

        public PoolsObject()
        {
            Blocks = PoolContainer.CreatePool<PoolObject>(pathBlock);
            Booms = PoolContainer.CreatePool<PoolObjectBoom>(pathBoom);
        }
    }
}