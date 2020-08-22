using System;
using UnityEngine;
using GrayscaleBlock3D.Extensions;

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

        public Blockube(GameObject gameObject, Color color, ushort numberColor)
        {

            Transform = gameObject.transform ? gameObject.transform : throw new ArgumentNullException(nameof(gameObject.transform));
            var renderer = gameObject.GetComponent<Renderer>();
            Renderer = renderer ? renderer : throw new ArgumentNullException(nameof(renderer));
            Color = color != null ? color : throw new ArgumentNullException(nameof(color));
            NumberColor = numberColor;
        }

        public void MoveTo(in Vector2 vector2)
        {
            Transform.position += new Vector3(vector2.x, vector2.y, 0);
        }

        public void SetActive(bool active)
        {
            Transform.gameObject.SetActive(active);
        }
    }
}