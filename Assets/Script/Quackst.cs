using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool Completed { get; protected set; }
    public QuackstCompletedEvent quackstCompleted;

    public abstract class QuackstGoal : ScriptableObject
    {
        protected string description;
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent goalCompleted;

        public virtual string GetDescription()
        {
            return description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            goalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if(currentAmount >= requiredAmount)
            {
                Complete();
            }
        }

        public void Complete()
        {
            Completed = true;
            goalCompleted.Invoke();
            goalCompleted.RemoveAllListeners();
        }
    }

    public List<QuackstGoal> goals;

    public void Initialize()
    {
        Completed = false;
        quackstCompleted = new QuackstCompletedEvent();

        foreach(var goal in goals)
        {
            goal.Initialize();
            goal.goalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    public void CheckGoals()
    {
        Completed = goals.All(g => g.Completed);
        if (Completed)
        {
            quackstCompleted.Invoke(this);
            quackstCompleted.RemoveAllListeners();
        }
    }
}

public class QuackstCompletedEvent : UnityEvent<Quackst>
{

}
