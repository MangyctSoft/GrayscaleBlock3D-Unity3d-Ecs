using System;
using UnityEngine;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D.Components.Player
{
    [Serializable]
    internal struct RotateComponent
    {
        public GameObject MainPlace;
        public GameObject RotationPlace;
        public float Speed;
        public IBlockube CurrentBlockRotate;
        public IBlockube PreviewBlockRotate;
    }
}