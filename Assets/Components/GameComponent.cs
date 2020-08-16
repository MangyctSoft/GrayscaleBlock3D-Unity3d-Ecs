namespace GrayscaleBlock3D
{
    /// <summary>
    /// Главное управление игрой.
    /// </summary>
    struct GameComponent
    {
        /// <summary>
        /// True, если разрешен респавн блока.
        /// </summary>
        public bool isRespawn;
        /// <summary>
        /// True, если проверяються одинаковые блоки рядом с друг другом.
        /// </summary>
        public bool isMerge;
        /// <summary>
        /// True, если проверяеться линия на одинаковые блоки.
        /// </summary>
        public bool isLine;
        /// <summary>
        /// Задержка событий проверки поля.
        /// </summary>
        public float delayMerge;
        /// <summary>
        /// True, если проверяeтся все игровое поле.
        /// </summary>
        public bool isMergeGlobal;
        /// <summary>
        /// True, если есть необходимость проверить игровое поле.
        /// </summary>
        public bool isNextMerge;
        public float delayMergeGlobal;
    }
}