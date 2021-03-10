using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{

    public GameObject currentVantage;
    public List<GameObject> previousVantage;
    GameObject playerCamera;

    public float timeToTravel = 0.5f;
    public float timeToTurn = 0.5f;
    bool travelling;

    public UnityEvent OnStartMoving;
    public UnityEvent OnStopMoving;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToPreviousLocation();
        }
    }

    public void AssignLocation(GameObject nextVantage)
    {
        if (!travelling)
        {
            if (currentVantage != null)
            {
                previousVantage.Add(currentVantage);
            }
            currentVantage = nextVantage;

            StartCoroutine(MoveToNextVantage(nextVantage.transform, nextVantage.GetComponent<VantagePoint>().cameraRot));
        }
    }

    public void ReturnToPreviousLocation()
    {
        if (previousVantage.Count != 0)
        {
            AssignLocation(previousVantage[previousVantage.Count-1]);

            previousVantage.RemoveAt(previousVantage.Count - 1);
        }
    }

    IEnumerator MoveToNextVantage(Transform targetPos, Quaternion targetRot)
    {
        travelling = true;
        OnStartMoving.Invoke();
        Vector3 currentPos = transform.position;
        float timeStarted = Time.time;

        StartCoroutine(TurnCamera(targetRot));

        while (Time.time < timeStarted + 1)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos.position, 1 - ((timeStarted + 1) - Time.time));

            yield return null;
        }
        OnStopMoving.Invoke();
        travelling = false;
    }

    IEnumerator TurnCamera(Quaternion targetRot)
    {
        float timeStarted = Time.time;
        Quaternion currentRot = playerCamera.transform.rotation;

        while (Time.time < timeStarted + 1)
        {
            playerCamera.transform.rotation = Quaternion.Lerp(currentRot, targetRot, 1 - ((timeStarted + 1 ) - Time.time));

            yield return null;
        }
    }

    public void ConfirmStartMoving()
    {
        print("Starting movement.");
    }

    public void ConfirmStopMoving()
    {
        print("Stopping movement.");
    }
}
