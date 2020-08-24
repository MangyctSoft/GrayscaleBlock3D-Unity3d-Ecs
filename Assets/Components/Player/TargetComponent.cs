using System;
using UnityEngine;

namespace GrayscaleBlock3D.Components.Player
{
    [Serializable]
    internal struct TargetComponent
    {
        public GameObject Target;
        public Renderer Renderer;
    }
}