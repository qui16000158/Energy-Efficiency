using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateTask : MonoBehaviour
{
    public TextMeshProUGUI textPrefabObject;


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


    private void Start()
    {
        InstantiateTasks(receptionTasks.Length, kitchenTasks.Length, meetingRoomTasks.Length, mainOfficeTasks.Length);
    }

    void InstantiateTasks(int numberOfReceptionTasks, int numberOfKitchenTasks, int numberOfMeetingRoomTasks, int numberOfMainOfficeTasks) //Input amounts of tasks for each location by counting the number of elements in each array. There is a seperate array for each location (Reception, Kitchen, MeetingRoom and MainOffice)
    {
        for (int i = 0; i < numberOfReceptionTasks; i++) //There are 4 loops, each of them is for a different office location. Each loop will repeat the cycle depending on the number of tasks there are in the location specified for the loop. The purpose of this loop is to grab tasks in form of scriptable objects from the arrays and instantiate them in the form of text in the game. To do this, a public method is called from the scriptable objects within the arrays. Method name InstantiateText();
        {                                                
            receptionTasks[receptionTaskNumber].InstantiateText(textPrefabObject, receptionOffset, "Reception"); //Reception Loop

            receptionTaskNumber += 1;

            receptionOffset += 20f; // Each loop has and offsett float number which increases everytime the loop runs.The purpose of this offset is to create spacing between the instantiated text objects.
        }

        for (int i = 0; i < numberOfKitchenTasks; i++)
        {       
            kitchenTasks[kitchenTaskNumber].InstantiateText(textPrefabObject, kitchenOffset, "Kitchen"); //Kitchen Loop

            kitchenTaskNumber += 1;

            kitchenOffset += 20f;
        }

        for (int i = 0; i < numberOfMeetingRoomTasks; i++)
        { 
            meetingRoomTasks[meetingRoomTaskNumber].InstantiateText(textPrefabObject, meetingRoomOffset, "MeetingRoom"); //MeetingRoom Loop

            meetingRoomTaskNumber += 1;

            meetingRoomOffset += 20f;
        }

        for (int i = 0; i < numberOfMainOfficeTasks; i++)
        {
            mainOfficeTasks[mainOfficeTaskNumber].InstantiateText(textPrefabObject, mainOfficeOffset, "MainOffice"); //MainOffice Loop

            mainOfficeTaskNumber += 1;

            mainOfficeOffset += 20f;
        }
    }
}
