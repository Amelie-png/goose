using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test
public abstract class GameEvent
{
    public string eventDescription;
}//general game events (all have description)

public class TomatoGameEvent : GameEvent
{
    public string TomatoType;//Type of tomato (additonal parameter)

    public TomatoGameEvent(string type)
    {
        TomatoType = type;
    }
}//New tomato game event inheriting from game event