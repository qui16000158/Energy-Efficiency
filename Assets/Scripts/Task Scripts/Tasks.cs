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

    private TextMeshProUGUI newBoardTaskText;

    private GameObject objectToDisable;

    private GameObject taskObject;

    public string objectTag;

    private bool taskComplete;

    GameObject parentObject;

    GameObject parentBoardObject;

    public bool multipleObjectives;

    public void InstantiateText(TextMeshProUGUI prefabText, TextMeshProUGUI boardPrefabText, float offset, string Location) //This method is used to instantiate TMPro task text for the current room
    {
        parentObject = GameObject.Find(Location.ToString());

        newTaskText = Instantiate(prefabText, parentObject.transform);

        newTaskText.transform.SetParent(parentObject.transform);

        newTaskText.transform.position -= Vector3.up * offset;

        newTaskText.name = taskName + " Task";

        newTaskText.text = taskDescription;

        InstantiateTaskBoardText(Location, boardPrefabText, offset);
    } 

    public void InstantiateTaskBoardText(string BoardLocation, TextMeshProUGUI boardPrefab, float boardOffset) //This method is used to instantiate TMPro task text on the task board
    {
        parentBoardObject = GameObject.Find("Board" + BoardLocation.ToString());

        newBoardTaskText = Instantiate(boardPrefab, parentBoardObject.transform);

        boardOffset -= 2f;

        newBoardTaskText.transform.position -= Vector3.up * boardOffset;

        newBoardTaskText.name = taskName + "Board Task";

        newBoardTaskText.text = taskDescription;
    }

    public void EndTask() //This method is used to mark the task as complete, it will change the tasks text colour to green and add "done" word at the end.
    {
        if (multipleObjectives == false)
        {
            newTaskText.text = newTaskText.text + " - Done!";

            newTaskText.color = Color.green;

            newBoardTaskText.text = newBoardTaskText.text + " - Done!";

            newBoardTaskText.color = Color.green;

            if (objectTag != "") //This if statement is using tag to disable a gameobject once a task is done. 
            {
                objectToDisable = GameObject.FindGameObjectsWithTag(objectTag)[0];

                objectToDisable.SetActive(false);
            }
        }
    }
}




 