using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public string PlayerName = "RED";
    public bool PlayerIsFemale;
    [HideInInspector]
    public Player Player;
    private void Start () {
        Player = FindObjectOfType<Player> ();
    }
}