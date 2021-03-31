using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateTask : MonoBehaviour
{
    public TextMeshProUGUI textPrefabObject;

    public TextMeshProUGUI boardTextPrefabObject;

    public Tasks[] receptionTasks;

    public Tasks[] kitchenTasks;

    public Tasks[] meetingRoomTasks;

    public Tasks[] mainOfficeTasks;

    private int receptionTaskNumber = 0;

    private int kitchenTaskNumber = 0;

    private int meetingRoomTaskNumber = 0;

    private int mainOfficeTaskNumber = 0;

    private float receptionOffset = 0;

    private float kitchenOffset = 0;        

    private float meetingRoomOffset = 0;

    private float mainOfficeOffset = 0;

    public GameObject meetingRoom;

    public GameObject mainOffice;

    public GameObject kitchen;

    public GameObject reception;

    private int allTasksNumber = 0;

    private int allTasksDoneNumber;

    public TextMeshProUGUI BoardTaskNumberText;

    private bool boardActive = true;

    public GameObject taskBoard;

    public GameObject taskCanvas;


    private void Awake()
    {
        InstantiateTasks(receptionTasks.Length, kitchenTasks.Length, meetingRoomTasks.Length, mainOfficeTasks.Length);
    }

    private void Start()
    {
        HideAllTasks();

        ToggleBoard();

        UpdateBoard();
    }

    void InstantiateTasks(int numberOfReceptionTasks, int numberOfKitchenTasks, int numberOfMeetingRoomTasks, int numberOfMainOfficeTasks) //Input amounts of tasks for each location by counting the number of elements in each array. There is a seperate array for each location (Reception, Kitchen, MeetingRoom and MainOffice)
    {
        for (int i = 0; i < numberOfReceptionTasks; i++) //There are 4 loops, each of them is for a different office location. Each loop will repeat the cycle depending on the number of tasks there are in the location specified for the loop. The purpose of this loop is to grab tasks in form of scriptable objects from the arrays and instantiate them in the form of text in the game. To do this, a public method is called from the scriptable objects within the arrays. Method name InstantiateText();
        {                                                
            receptionTasks[receptionTaskNumber].InstantiateText(textPrefabObject, boardTextPrefabObject, receptionOffset, "Reception"); //Reception Loop

            receptionTaskNumber += 1;

            receptionOffset += 13f; // Each loop has and offsett float number which increases everytime the loop runs.The purpose of this offset is to create spacing between the instantiated text objects.

            allTasksNumber += 1;
        }

        for (int i = 0; i < numberOfKitchenTasks; i++)
        {       
            kitchenTasks[kitchenTaskNumber].InstantiateText(textPrefabObject, boardTextPrefabObject, kitchenOffset, "Kitchen"); //Kitchen Loop

            kitchenTaskNumber += 1;

            kitchenOffset += 13f;

            allTasksNumber += 1;

        }

        for (int i = 0; i < numberOfMeetingRoomTasks; i++)
        { 
            meetingRoomTasks[meetingRoomTaskNumber].InstantiateText(textPrefabObject, boardTextPrefabObject, meetingRoomOffset, "MeetingRoom"); //MeetingRoom Loop

            meetingRoomTaskNumber += 1;

            meetingRoomOffset += 13f;

            allTasksNumber += 1;

        }

        for (int i = 0; i < numberOfMainOfficeTasks; i++)
        {
            mainOfficeTasks[mainOfficeTaskNumber].InstantiateText(textPrefabObject, boardTextPrefabObject, mainOfficeOffset, "MainOffice"); //MainOffice Loop

            mainOfficeTaskNumber += 1;

            mainOfficeOffset += 13f;

            allTasksNumber += 1;

        }
    }

    public void ShowMainOfficeTasks()
    {
        HideAllTasks();
        mainOffice.SetActive(true);
    }

    public void ShowKitchenTasks()
    {
        HideAllTasks();
        kitchen.SetActive(true);
    }

    public void ShowReceptionTasks()
    {
        HideAllTasks();
        reception.SetActive(true);
    }

    public void ShowMeetingRoomTasks()
    {
        HideAllTasks();
        meetingRoom.SetActive(true);
    }

    public void HideAllTasks() //Method used to hide all tasks when player is not in any of the rooms
    {
        mainOffice.SetActive(false);

        kitchen.SetActive(false);

        reception.SetActive(false);

        meetingRoom.SetActive(false);
    }

    public void UpdateBoardClick() //Refreshes the board once player presses a button
    {
        allTasksDoneNumber += 1;

        UpdateBoard();
    }

    private void UpdateBoard()
    {
        if(allTasksDoneNumber == allTasksNumber)
        {
            BoardTaskNumberText.color = Color.green;
        }

        BoardTaskNumberText.text = "Total Tasks Completed: " + allTasksDoneNumber + "/" + allTasksNumber;
    }

    public void ToggleBoard() //Method used to turn the task board On and Off
    {
        if (boardActive == true)
        {
            taskBoard.SetActive(false);
            boardActive = false;
        }
        else
        {
            taskBoard.SetActive(true);
            boardActive = true;
        }
    }

    public void TaskCanvasEnabled()
    {
        taskCanvas.SetActive(true);
    }

    public void TaskCanvasDisabled()
    {
        taskCanvas.SetActive(false);
    }
}
