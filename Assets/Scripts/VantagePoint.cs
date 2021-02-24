using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VantagePoint : MonoBehaviour
{
    public GameObject cameraPointer;
    public Quaternion cameraRot;

    GameObject player;
    MeshRenderer arrowModel;
    CapsuleCollider clickCollider;
    bool currentlyHere = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowModel = transform.GetChild(0).GetComponent<MeshRenderer>();
        clickCollider = GetComponent<CapsuleCollider>();

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
        print("clicked");
        player.GetComponent<PlayerView>().AssignLocation(this.gameObject);
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
}
