using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavPath))]
public class PathEditor : Editor
{
    bool placePoints = false;
    int selectedIndex = -1;
    public void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        HandleMouse();

        Tools.current = Tool.None;
        NavPath path = target as NavPath;

        for(int i = 0; i < path.pathPoints.Count; i++)
        {
            Vector3 point = path.pathPoints[i];
            Handles.Label(point + Vector3.up, path.pathPoints.IndexOf(point).ToString());
            if (Handles.Button(point, Quaternion.identity, 1f, 1f, Handles.SphereHandleCap)){
                if (placePoints)
                {
                    path.pathPoints.RemoveAt(i);
                    break;
                }
                selectedIndex = path.pathPoints.IndexOf(point);
                Event e = Event.current;
                if (e.button == 1) Debug.Log("right click");
            }
        }

        if (selectedIndex >= 0 && selectedIndex < path.pathPoints.Count)
        {
            path.pathPoints[selectedIndex] = Handles.PositionHandle(path.pathPoints[selectedIndex], Quaternion.identity);
        }

        for(int i = 0; i < path.pathPoints.Count; i++){
            Vector3 v = path.pathPoints[i];
            v.x = Mathf.RoundToInt(path.pathPoints[i].x / path.gridCellSize) * path.gridCellSize;
            v.y = Mathf.RoundToInt(path.pathPoints[i].y / path.gridCellSize) * path.gridCellSize; 
            v.z = Mathf.RoundToInt(path.pathPoints[i].z / path.gridCellSize) * path.gridCellSize;
            path.pathPoints[i] = v;
        }
    }

    void HandleMouse()
    {

        Event e = Event.current;
        switch(e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0 && placePoints) {
                    (target as NavPath).pathPoints.Add(Point());
                }
                break;
            case EventType.MouseDrag:
                if (e.button == 0 && placePoints) (target as NavPath).pathPoints[(target as NavPath).pathPoints.Count - 1] = Point();
                break;
            case EventType.KeyDown:
                if (e.keyCode == KeyCode.LeftControl) placePoints = true;
                break;
            case EventType.KeyUp:
                if (e.keyCode == KeyCode.LeftControl) placePoints = false;
                break;
        }
        
    }

    Vector3 Point()
    {
        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;
        Physics.Raycast(r, out hit);
        return hit.point;
    }
}

public class NavPath : MonoBehaviour
{
    public bool gridAlign = false;
    public float gridCellSize = 1f;
    public bool loop = true;
    public List<Vector3> pathPoints = new List<Vector3>();

    public int NextIndex(int index)
    {
        if (index < 0 && loop) return pathPoints.Count - 1;
        if (index >= pathPoints.Count && loop) return 0;
        return Mathf.Clamp(index, 0, pathPoints.Count - 1);
    }

    public Vector3 this[int index]
    {
        get => pathPoints[index];
    }

    private void OnDrawGizmos()
    {
        if (pathPoints.Count <= 0) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(pathPoints[0],0.5f);
        Gizmos.color = Color.cyan;
        for(int i = 1; i < pathPoints.Count; i++){
            
            Gizmos.DrawLine(pathPoints[i - 1], pathPoints[i]);
            Gizmos.DrawSphere(pathPoints[i], 0.5f);
        }    
    }
}
