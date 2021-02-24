using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    public GameObject currentVantage;
    public GameObject camera;

    public float timeToTravel = 0.5f;
    public float timeToTurn = 0.5f;
    bool travelling;

    void Start()
    {
        
    }

    void Update()
    {
        //MovementCheck();
    }

    public void AssignLocation(GameObject nextVantage)
    {
        if (!travelling)
        {
            currentVantage = nextVantage;

            StartCoroutine(MoveToNextVantage(nextVantage.transform, nextVantage.GetComponent<VantagePoint>().cameraRot));
        }
    }

    void MovementCheck()
    {
        if (currentVantage != null)
        {
            if (transform.position != currentVantage.transform.position)
            {
                travelling = true;
            }
        }
    }

    IEnumerator MoveToNextVantage(Transform targetPos, Quaternion targetRot)
    {
        travelling = true;
        Transform currentPos = transform;
        float t = 0f;

        while (t < timeToTravel)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos.position, targetPos.position, Mathf.SmoothStep(0, timeToTravel, t));

            yield return null;
        }

        //StartCoroutine(TurnCamera(targetRot));
        camera.transform.rotation = targetRot;

        travelling = false;
    }

    IEnumerator TurnCamera(Quaternion targetRot)
    {
        float t2 = 0f;
        Quaternion currentRot = camera.transform.rotation;

        while (t2 < timeToTurn)
        {
            t2 += Time.deltaTime;
            camera.transform.rotation = Quaternion.Lerp(currentRot, targetRot, Mathf.SmoothStep(0, timeToTurn, t2));

            yield return null;
        }
    }
}
