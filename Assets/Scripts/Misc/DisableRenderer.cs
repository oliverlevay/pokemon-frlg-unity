using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    public bool HideOnPlay = true;
    void Start()
    {
        GetComponent<Renderer>().enabled = !HideOnPlay;
    }
}
