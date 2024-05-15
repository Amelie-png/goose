using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Quackst : ScriptableObject
{
    //Quackst = list of goals, goals have a progress check
    //A quackst is complete when all goals are complete

    [System.Serializable]
    public struct Info
    {
        public string name;
        public string description;
    }

    [Header("Info")]
    public Info information;

    [System.Serializable]
    public struct Gift
    {
        public string giftInfo;
    }

    [Header("Reward")]
    public Gift reward = new Gift { giftInfo = "This is a gift." };

    //uhh variables
    public bool Completed { get; protected set; } //State of goal
    public QuackstCompletedEvent quackstCompleted; //
    public List<QuackstGoal> goals; //List of goals

    //Goals and related methods
    public abstract class QuackstGoal : ScriptableObject
    {
        protected string description; //Goal description
        public int currentAmount { get; protected set; } //Player goal progress
        public int requiredAmount = 1; //Required goal progres
        public bool Completed { get; protected set; } //Flag to check if completed

        [HideInInspector]
        public UnityEvent goalCompleted;

        public virtual string GetDescription() => description;

        public virtual void Initialize()
        {
            Completed = false;
            goalCompleted = new UnityEvent();
        }//Initialize new goal

        protected void Evaluate()
        {
            if(currentAmount >= requiredAmount)
            {
                Complete();
            }
        }//if goal is complete call Complete()

        public void Complete()
        {
            Completed = true;
            goalCompleted.Invoke();
            goalCompleted.RemoveAllListeners();
        }//Flag as completed, invoke goal completed event + remove event
    }

    public void Initialize()
    {
        Completed = false;//Initial flag as incomplete
        quackstCompleted = new QuackstCompletedEvent();//Initiate new completedEvent

        foreach(var goal in goals)
        {
            goal.Initialize();
            goal.goalCompleted.AddListener(delegate { CheckGoals(); });
        }//Initiate each goal in list
    }//Initialize new quackst

    public void CheckGoals()
    {
        Completed = goals.All(g => g.Completed);
        if (Completed)
        {
            quackstCompleted.Invoke(this);
            quackstCompleted.RemoveAllListeners();
        }//if completed, invoke quest completed event + remove event
    }//Check if all goals in quackst are completed
}

public class QuackstCompletedEvent : UnityEvent<Quackst>
{

}

#if UNITY_EDITOR
[CustomEditor(typeof(Quackst))]
public class QuackstEditor : Editor
{
    SerializedProperty m_QuackstInfoProperty;//property of info to use for editor
    SerializedProperty m_QuackstStatProperty;//property of reward to use for editor

    List<string> m_QuackstGoalType;//List of goals
    SerializedProperty m_QuackstGoalListProperty;//Property of list

    [MenuItem("Assets/Quackst", priority = 0)]//Add Quackst object option in menu
    public static void CreateQuackst()
    {
        var newQuackst = CreateInstance<Quackst>();
        ProjectWindowUtil.CreateAsset(newQuackst, "quackst.asset");//create new Quackst editor
    }

    private void OnEnable()
    {
        m_QuackstInfoProperty = serializedObject.FindProperty(nameof(Quackst.information));//Initalize as property of info
        m_QuackstStatProperty = serializedObject.FindProperty(nameof(Quackst.reward));//Initialize as property of reward

        m_QuackstGoalListProperty = serializedObject.FindProperty(nameof(Quackst.goals));//Initialize as goals

        var Lookup = typeof(Quackst.QuackstGoal);//Store type of Quackst Goal

        //Initiate list of goal
        m_QuackstGoalType = System.AppDomain.CurrentDomain.GetAssemblies()//get goals by:
            .SelectMany(assembly => assembly.GetTypes())//get goals
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(Lookup))//check if: is class, not abstract and is subclass of quackst goal
            .Select(type => type.Name)//get names
            .ToList();//display in list
    }

    //UI of editor
    public override void OnInspectorGUI()
    {
        var child = m_QuackstInfoProperty.Copy();//get each element of info properties
        var depth = child.depth;//get length of elements
        child.NextVisible(true);//show first element

        EditorGUILayout.LabelField("Quackst info", EditorStyles.boldLabel);//header
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }//Keep displaying elements until through the entire list

        child = m_QuackstStatProperty.Copy();//get each element of stat properties
        depth = child.depth;//get length of elements
        child.NextVisible(true);//show first element

        EditorGUILayout.LabelField("Quackst reward", EditorStyles.boldLabel);//header
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }//Keep displaying elements until through the entire list

        //Drop down menu
        int choice = EditorGUILayout.Popup("Add new Quackst Goal", -1, m_QuackstGoalType.ToArray());
        //.Popup(Label, current choice (-1 = empty choice), displayed options)

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_QuackstGoalType[choice]);//make choice

            AssetDatabase.AddObjectToAsset(newInstance, target);//add to assets

            //get properties for goal editor
            m_QuackstGoalListProperty.InsertArrayElementAtIndex(m_QuackstGoalListProperty.arraySize);
            m_QuackstGoalListProperty.GetArrayElementAtIndex(m_QuackstGoalListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;
        }//choose new option from drop down menu

        Editor ed = null;//sub editor initialization (can change according to choice)
        int toDelete = -1;//delete index for sub editor delete option

        for (int i = 0; i < m_QuackstGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();//start horizontal display
            EditorGUILayout.BeginVertical();//start vertical display
            var item = m_QuackstGoalListProperty.GetArrayElementAtIndex(i);//set item as current in array
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);//edit properties of referenced object

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);//use ed to creat editor for each properties in choice

            ed.OnInspectorGUI();//create sub editor in inspector
            EditorGUILayout.EndVertical();//end vertical display

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }//if remove button pressed, add sub editor index to toDelete
            EditorGUILayout.EndHorizontal();//end horizontal display
        }//loop through and display all properties

        if (toDelete != -1)
        {
            var item = m_QuackstGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            //get reference value using index toDelete (unity stuff)
            DestroyImmediate(item, true);//delete

            m_QuackstGoalListProperty.DeleteArrayElementAtIndex(toDelete);//nullify entry
            m_QuackstGoalListProperty.DeleteArrayElementAtIndex(toDelete);//remove entry
        }//deleting sub editor

        serializedObject.ApplyModifiedProperties();//apply all changes of editor to inspector
    }

}
#endif//make custom editor yayyy