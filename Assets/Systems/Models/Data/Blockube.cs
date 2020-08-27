using System;
using UnityEngine;
using GrayscaleBlock3D.Pooling;

namespace GrayscaleBlock3D.Systems.Models.Data
{
    public class Blockube : IBlockube
    {
        public Vector2 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }
        public Color Color
        {
            //get => Color; - Получают общую ссылку на цвет, изменяя который изменяется у всех с таким же цветом.
            set => Renderer.material.SetColor("_Color", value);
        }
        public Transform Transform { get; }
        private Renderer Renderer { get; }
        public ushort NumberColor { get; set; }
        private readonly IPoolObject _poolObjectBlocks;

        public Blockube(GameObject gameObject, Color color, ushort numberColor, IPoolObject poolObjectBlocks = null)
        {
            Transform = gameObject.transform ? gameObject.transform : throw new ArgumentNullException(nameof(gameObject.transform));
            var renderer = gameObject.GetComponent<Renderer>();
            Renderer = renderer ? renderer : throw new ArgumentNullException(nameof(renderer));
            Color = color != null ? color : throw new ArgumentNullException(nameof(color));
            NumberColor = numberColor;
            _poolObjectBlocks = poolObjectBlocks;
        }

        public void MoveTo(in Vector2 vector2)
        {
            Transform.position += new Vector3(vector2.x, vector2.y, 0);
        }

        public void SetActive(bool active, GameObject gameObject = default)
        {
            // Transform.gameObject.SetActive(active);

            // if (!active && gameObject != null)
            // {
            //     var boom = GameObject.Instantiate(gameObject, Position, Quaternion.identity);

            //     var colliders = Physics.OverlapSphere(Position, 2f);
            //     foreach (var obj in colliders)
            //     {
            //         Rigidbody rb = obj.GetComponent<Rigidbody>();
            //         if (rb != null)
            //         {
            //             rb.AddExplosionForce(600f, Position, 1f);
            //         }
            //     }
            // }
        }

        public void Destroy()
        {
            if (_poolObjectBlocks != null)
            {
                _poolObjectBlocks.PoolTransform.gameObject.SetActive(false);
                _poolObjectBlocks.PoolRecycle();
                NumberColor = 0;
            }
            else
            {
                UnityEngine.Object.Destroy(Transform.gameObject);
            }
        }
    }
}