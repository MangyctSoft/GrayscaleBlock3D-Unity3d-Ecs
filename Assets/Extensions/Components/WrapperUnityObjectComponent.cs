using UnityEngine;

namespace GrayscaleBlock3D.Extensions.Components
{
    public struct WrapperUnityObjectComponent<T> where T : Object
    {
        public T Value;
    }
}