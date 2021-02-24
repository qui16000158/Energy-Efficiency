using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Task", menuName = "New Task")]
public class Tasks : ScriptableObject
{
    public string taskName;

    public string taskDescription;

    private TextMeshProUGUI newTaskText;


    GameObject parentObject;


    public void InstantiateText(TextMeshProUGUI prefabText, float offset, string Location)
    {
        parentObject = GameObject.Find(Location.ToString());

        newTaskText = Instantiate(prefabText, parentObject.transform);
        newTaskText.transform.SetParent(parentObject.transform);

        newTaskText.transform.position -= Vector3.up * offset;

        newTaskText.name = taskName + " Task";

        newTaskText.text = taskDescription;
    }

    public void EndTask()
    {
        newTaskText.text = newTaskText.text + " - Done!";

        newTaskText.color = Color.green;
    }
}




