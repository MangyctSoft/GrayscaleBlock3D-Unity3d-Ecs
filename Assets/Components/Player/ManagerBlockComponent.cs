using UnityEngine;

namespace GrayscaleBlock3D.Components.Player
{
    internal struct ManagerBlockComponent
    {
        public Vector2 Position;
        public ushort NumberColor;
        public bool Active;
        public bool NeedScanField;
        public Vector2 ScanPosition;
    }
}