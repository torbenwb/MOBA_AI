using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI))]
public class AIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AI ai = target as AI;
        for(int i = 0; i < ai.tasks.Count; i++)
        {
            if (i == ai.activeTask) GUILayout.Label($"Active: {ai.tasks[i].ToString()}");
            else GUILayout.Label(ai.tasks[i].ToString());
        }
    }
}

public class AI : MonoBehaviour
{
    public bool update = false;
    public float updateFrequency = 0.1f;
    public List<MonoBehaviour> tasks = new List<MonoBehaviour>();
    public int activeTask=0;


    public bool Update{
        get => update;
        set{
            if (!update && value) StartCoroutine(EvaluateTasks());
            update = value;
        }
    }

    public MonoBehaviour ActiveTask
    {
        get{
            if (activeTask < 0) return null;
            else return tasks[activeTask];
        }
    } 

    void Start(){
        for(int i = 0; i < tasks.Count; i++)
        {
            if (!(tasks[i] is ITask)) tasks.RemoveAt(i--);
        }
        Update = true;
    }

    IEnumerator EvaluateTasks()
    {
        update = true;
        
        while(update)
        {
            int newActiveTask = -1;
            for(int i = 0; i < tasks.Count; i++)
            {
                if ((tasks[i] as ITask).Evaluate()) {newActiveTask = i; break;}
            }
            activeTask = newActiveTask;
            yield return new WaitForSeconds(updateFrequency);
        }
    }   

    private void OnDisable()
    {
        update = false;
    }
}
