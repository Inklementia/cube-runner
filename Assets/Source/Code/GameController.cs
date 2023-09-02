using System.Collections;
using System.Collections.Generic;
using Source.Code;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //[SerializeField] private GameData gameData;
    public void StartGame()
    {
        GameActions.Instance.GameStart();
    }
    
    public void StopGame()
    {
        GameActions.Instance.GameStop();
    }
}
