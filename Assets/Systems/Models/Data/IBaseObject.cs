using System;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Data
{
    public interface IBaseObject
    {
        Vector2 Position { get; set; }
        void Destroy();
    }
}