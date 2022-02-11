using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPart : MonoBehaviour
{
    private bool isBuilded;
    // Start is called before the first frame update
    void Start()
    {
        isBuilded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsBuilded
    {
        get => isBuilded;
        set => isBuilded = value;
    }
}
