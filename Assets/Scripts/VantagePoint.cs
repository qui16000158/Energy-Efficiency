using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VantagePoint : MonoBehaviour
{
    public static bool isMoving = false;

    public GameObject cameraPointer;
    public Quaternion cameraRot;
    public UnityEvent OnPlayerEntersLocation;

    GameObject player;
    MeshRenderer arrowModel;
    CapsuleCollider clickCollider;
    bool currentlyHere = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowModel = transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>();
        clickCollider = transform.GetChild(0).GetComponent<CapsuleCollider>();

        cameraRot = cameraPointer.transform.rotation;
        cameraPointer.SetActive(false);
    }

    void Update()
    {
        PlayerLocationCheck();
        HandleArrow();
    }

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            player.GetComponent<PlayerView>().AssignLocation(this.gameObject);

            OnPlayerEntersLocation.Invoke();
        }
    }

    void PlayerLocationCheck()
    {
        if (player.GetComponent<PlayerView>().currentVantage == this.gameObject)
        {
            currentlyHere = true;
        }
        else
        {
            currentlyHere = false;
        }
    }

    void HandleArrow()
    {
        if (currentlyHere)
        {
            arrowModel.enabled = false;
            clickCollider.enabled = false;
        }
        else
        {
            arrowModel.enabled = true;
            clickCollider.enabled = true;
        }
    }

    public void ConfirmLocationEnter()
    {
        print("Entering" + gameObject.name);
    }
}
