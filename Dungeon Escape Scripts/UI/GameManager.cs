using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Player Player { get; private set; } /* private so that the player cant be reassigned */

    public bool HasKeyToCastle { get; set; } /* auto property */

}
