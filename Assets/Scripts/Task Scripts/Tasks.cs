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

    public void InstantiateText(TextMeshProUGUI prefabText, TextMeshProUGUI boardPrefabText, float offset, string Location)
    {
        parentObject = GameObject.Find(Location.ToString());

        newTaskText = Instantiate(prefabText, parentObject.transform);
        newTaskText.transform.SetParent(parentObject.transform);

        newTaskText.transform.position -= Vector3.up * offset;

        newTaskText.name = taskName + " Task";

        newTaskText.text = taskDescription;

        InstantiateTaskBoardText(Location, boardPrefabText, offset);
    } 

    public void InstantiateTaskBoardText(string BoardLocation, TextMeshProUGUI boardPrefab, float boardOffset)
    {
        parentBoardObject = GameObject.Find("Board" + BoardLocation.ToString());

        newBoardTaskText = Instantiate(boardPrefab, parentBoardObject.transform);

        boardOffset -= 4f;

        newBoardTaskText.transform.position -= Vector3.up * boardOffset;

        newBoardTaskText.name = taskName + "Board Task";

        newBoardTaskText.text = taskDescription;
    }

    public void EndTask()
    {
        if (multipleObjectives == false)
        {
            newTaskText.text = newTaskText.text + " - Done!";

            newTaskText.color = Color.green;

            newBoardTaskText.text = newBoardTaskText.text + " - Done!";

            newBoardTaskText.color = Color.green;

            if (objectTag != "")
            {
                objectToDisable = GameObject.FindGameObjectsWithTag(objectTag)[0];

                objectToDisable.SetActive(false);
            }
        }
    }
}




 