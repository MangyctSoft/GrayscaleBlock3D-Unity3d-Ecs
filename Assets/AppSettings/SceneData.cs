using UnityEngine;
using UnityEngine.UI;
using GrayscaleBlock3D.Components;

namespace GrayscaleBlock3D.AppSettings
{
    public class SceneData : MonoBehaviour
    {
        public Camera Camera = default;
        public Canvas Canvas = default;

        public Canvas SplashScreen = default;
        public Text SplashScreenScore = default;

        public Text ScoreText = default;

        public GameObject CurrentBlockPrefab = default;
        public GameObject PreviewBlockPrefab = default;
        public Transform PlaceBlocks = default;

        public Transform ExplosionPrefab = default;

    }
}