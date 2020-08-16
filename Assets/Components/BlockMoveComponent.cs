namespace GrayscaleBlock3D
{
    /// <summary>
    /// Контроль блоком.
    /// </summary>
    struct MoveBlockComponent
    {
        /// <summary>
        /// Направление движения.
        /// </summary>
        public BlockDirection direction;
        /// <summary>
        /// True, если можно перемещать блок.
        /// </summary>
        public bool isMove;
        /// <summary>
        /// True, если нажата кнопка перемещения.
        /// </summary>
        public bool isInput;
        /// <summary>
        /// True, если нажата кнопка сброса блока.
        /// </summary>
        public bool isFalling;
    }
}