using System;
using UnityEngine;
using GrayscaleBlock3D.Pooling;

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
        private Rigidbody Rigidbody { get; }
        private readonly IPoolObject _poolObject;
        public Boom(Transform transform, Rigidbody rigidbody, IPoolObject poolObject = null)
        {
            Transform = transform ? transform : throw new ArgumentNullException(nameof(transform));
            Rigidbody = rigidbody ? rigidbody : throw new ArgumentNullException(nameof(rigidbody));
            _poolObject = poolObject;
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