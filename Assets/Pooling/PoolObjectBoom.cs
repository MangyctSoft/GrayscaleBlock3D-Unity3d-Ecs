using UnityEngine;

namespace GrayscaleBlock3D.Pooling
{
    public class PoolObjectBoom : PoolObject
    {
        public Rigidbody Rigidbody { get; private set; }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    }
}