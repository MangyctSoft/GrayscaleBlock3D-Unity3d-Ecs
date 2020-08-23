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
        [Space]
        [Tooltip("Пустой объект для хранения управляющих блоков.")]
        public GameObject MainBlocksPlace = default;
        [Space]
        public GameObject CurrentBlock = default;
        public GameObject PreviewBlock = default;
        [Space]
        [Tooltip("Пустой объект для хранения блоков для механизма рокировки.")]
        public GameObject RotatePlace = default;
        [Space]
        public GameObject CurrentBlockRotate = default;
        public GameObject PreviewBlockRotate = default;
        [Space]
        [Tooltip("Пустой объект для хранения всех блоков из игрового поля.")]
        public Transform PlaceBlocks = default;
        [Space]
        [Tooltip("Объект для указания места в поле.")]
        public GameObject Target = default;
        [Space]
        public Transform ExplosionPrefab = default;




    }
}