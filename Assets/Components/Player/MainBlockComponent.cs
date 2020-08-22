using GrayscaleBlock3D.Systems.Models.Data;
using System;

namespace GrayscaleBlock3D.Components.Player
{
    [Serializable]
    internal struct MainBlockComponent
    {
        public IBlockube CurrentBlock;
        public IBlockube PreviewBlock;
    }
}