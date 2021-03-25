using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBlocker : MonoBehaviour
{
    public static List<GameObject> blockers = new List<GameObject>();

    public static bool IsBlocked
    {
        get
        {
            return blockers.Count > 0;
        }
    }

    void OnEnable()
    {
        if (!blockers.Contains(gameObject))
        {
            blockers.Add(gameObject);
        }
    }

    void OnDisable()
    {
        if (blockers.Contains(gameObject))
        {
            blockers.Remove(gameObject);
        }
    }
}
