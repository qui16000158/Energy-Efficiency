using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{

    public GameObject currentVantage;
    GameObject previousVantage;
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

    }

    public void AssignLocation(GameObject nextVantage)
    {
        if (!travelling)
        {
            currentVantage = nextVantage;

            StartCoroutine(MoveToNextVantage(nextVantage.transform, nextVantage.GetComponent<VantagePoint>().cameraRot));
        }
    }

    IEnumerator MoveToNextVantage(Transform targetPos, Quaternion targetRot)
    {
        travelling = true;
        OnStartMoving.Invoke();
        Transform currentPos = transform;
        float t = 0f;

        while (t < timeToTravel)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos.position, targetPos.position, Mathf.SmoothStep(0, timeToTravel, t));

            yield return null;
        }

        //StartCoroutine(TurnCamera(targetRot));
        playerCamera.transform.rotation = targetRot;
        OnStopMoving.Invoke();
        travelling = false;
    }

    IEnumerator TurnCamera(Quaternion targetRot)
    {
        float t2 = 0f;
        Quaternion currentRot = playerCamera.transform.rotation;

        while (t2 < timeToTurn)
        {
            t2 += Time.deltaTime;
            playerCamera.transform.rotation = Quaternion.Lerp(currentRot, targetRot, Mathf.SmoothStep(0, timeToTurn, t2));

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
