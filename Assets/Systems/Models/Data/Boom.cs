using System;
using UnityEngine;
using GrayscaleBlock3D.Pooling;
using System.Collections.Generic;

namespace GrayscaleBlock3D.Systems.Models.Data
{
    public class Boom : IBaseObject
    {
        public Vector2 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }
        public Transform Transform { get; }
        private IEnumerable<Rigidbody> ChildRigidbody { get; }
        private readonly IPoolObject _poolObject;
        public Boom(Transform transform, IEnumerable<Rigidbody> childRigidbody, IPoolObject poolObject = null)
        {
            Transform = transform ? transform : throw new ArgumentNullException(nameof(transform));
            ChildRigidbody = childRigidbody != null ? childRigidbody : throw new ArgumentNullException(nameof(childRigidbody));
            _poolObject = poolObject;
        }
        public void SetBoom()
        {
            foreach (var item in ChildRigidbody)
            {
                if (item != null)
                {
                    item.AddExplosionForce(600f, Position, 1f);
                }
            }

        }
        public void Destroy()
        {
            if (_poolObject != null)
            {
                _poolObject.PoolTransform.gameObject.SetActive(false);
                _poolObject.PoolRecycle();
            }
            else
            {
                UnityEngine.Object.Destroy(Transform.gameObject);
            }
        }
    }
}