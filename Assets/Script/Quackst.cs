using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quackst : ScriptableObject
{
    [System.Serializable]

    public struct Info
    {
        public string name;
        public string description;
    }

    [Header("Info")] public Info information;

    [System.Serializable]

    public struct Gift
    {
        public string giftInfo;
    }

    [Header("Reward")] public Gift reward = new Gift { giftInfo = "This is a gift." };

    public bool completed { get; protected set; }

    public abstract class QuackstGoal : ScriptableObject
    {
        protected string description;
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public virtual string getDescription()
        {
            return description;
        }
    }

    public List<QuackstGoal> goals;
}

public class QuackstCompletedEvent : UnityEvent<Quackst>
{

}
