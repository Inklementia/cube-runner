using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Source.Code
{
    public class GameActions : SingletonClass<GameActions>
    {
        public event Action OnGameStarted;

        public void GameStart()
        {
            if (OnGameStarted != null)
            {
                OnGameStarted();
            }
        }
        
        public event Action OnGameStopped;
        
        public void GameStop()
        {
            if (OnGameStopped != null)
            {
                OnGameStopped();
            }
        }
    }
}
