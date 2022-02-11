using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Vector3 deployDestination;

    private bool isDeployable;

    private GameObject originalObject;

    private float deploySpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        deployDestination = Vector3.zero;
        isDeployable = false;
        originalObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeployable)
        {
            transform.position = Vector3.Lerp(transform.position, deployDestination, deploySpeed * Time.deltaTime);
        }

        if ((transform.position - deployDestination).magnitude < 0.1f)
        {
            originalObject.GetComponent<MeshRenderer>().enabled = true;
            Destroy(gameObject);
        }
    }

    public Vector3 DeployDestination
    {
        get => deployDestination;
        set => deployDestination = value;
    }

    public bool IsDeployable
    {
        get => isDeployable;
        set => isDeployable = value;
    }

    public GameObject OriginalObject
    {
        get => originalObject;
        set => originalObject = value;
    }
}
