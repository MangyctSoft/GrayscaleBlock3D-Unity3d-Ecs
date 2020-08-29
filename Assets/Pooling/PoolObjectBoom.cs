using UnityEngine;
using System.Collections.Generic;

namespace GrayscaleBlock3D.Pooling
{
    public class PoolObjectBoom : PoolObject
    {
        public IEnumerable<Rigidbody> ChildRigidbody { get; private set; }
        private void Awake()
        {
            var rigidbody = new List<Rigidbody>();
            foreach (Transform child in this.transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rigidbody.Add(rb);
                }
            }
            ChildRigidbody = rigidbody;
        }
    }
}