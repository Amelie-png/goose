using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoGoal : Quackst.QuackstGoal
{
    public string Tomato;

    public override string GetDescription()
    {
        return $"Plant a {Tomato}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<TomatoGameEvent>(OnPlanting);
    }

    private void OnPlanting(TomatoGameEvent eventInfo)
    {
        if(eventInfo.TomatoType == Tomato)
        {
            currentAmount++;
            Evaluate();
        }
    }
}
