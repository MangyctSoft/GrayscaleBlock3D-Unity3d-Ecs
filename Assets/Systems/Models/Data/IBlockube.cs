using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Data
{
    public interface IBlockube
    {
        Vector2 Position { get; set; }
        ushort NumberColor { get; set; }
        Color Color { set; }
        void MoveTo(in Vector2 direction);
    }
}