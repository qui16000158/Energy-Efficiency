using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{

    public GameObject currentVantage;
    public List<GameObject> previousVantage;
    GameObject playerCamera;

    public UnityEvent OnStartMoving;
    public UnityEvent OnStopMoving;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !VantagePoint.isMoving)
        {
            ReturnToPreviousLocation();
        }
    }

    public void AssignLocation(GameObject nextVantage, bool returning = false)
    {
        if (MovementBlocker.IsBlocked) return;

        if (!VantagePoint.isMoving)
        {
            if (currentVantage != null && !returning)
            {
                previousVantage.Add(currentVantage);
            }
            currentVantage = nextVantage;

            StartCoroutine(MoveToNextVantage(nextVantage.transform, nextVantage.GetComponent<VantagePoint>().cameraRot));
        }
    }

    public void ReturnToPreviousLocation()
    {
        if (MovementBlocker.IsBlocked || VantagePoint.isMoving) return;

        if (previousVantage.Count != 0)
        {
            AssignLocation(previousVantage[previousVantage.Count-1], true);

            previousVantage[previousVantage.Count - 1].GetComponent<VantagePoint>().OnPlayerEntersLocation.Invoke();

            previousVantage.RemoveAt(previousVantage.Count - 1);
        }
    }

    IEnumerator MoveToNextVantage(Transform targetPos, Quaternion targetRot)
    {
        VantagePoint.isMoving = true;
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
        VantagePoint.isMoving = false;
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
}
