using System;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Data
{
    public interface IBlockube : IBaseObject
    {
        ushort NumberColor { get; set; }
        Color Color { set; }
        void MoveTo(in Vector2 direction);
    }
}