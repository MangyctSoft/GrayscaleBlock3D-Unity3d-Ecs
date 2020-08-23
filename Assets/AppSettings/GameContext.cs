using UnityEngine;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D.AppSettings
{
    public enum GameStates
    {
        Play,
        Pause,
        GameOver,
        Restart,
        Exit
    }

    internal class GameContext
    {
        public GameStates GameState = default;
        public Blockube[,] GameField = default;
        public int[] RedLine = default;
        public ushort ONE_DIFF = 1;
    }
}
