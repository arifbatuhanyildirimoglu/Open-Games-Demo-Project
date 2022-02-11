using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int woodCapacity;
    private Vector3 initialColliderSize;
    private Vector3 colliderSize;

    void Start()
    {
        woodCapacity = 3;
        initialColliderSize = GetComponent<BoxCollider>().size;
    }

    void Update()
    {
        switch (woodCapacity)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                colliderSize = initialColliderSize * (15f / 5f);
                ShrinkTree(5f);
                break;
            case 2:
                colliderSize = initialColliderSize * (15f / 10f);
                ShrinkTree(10f);
                break;
        }
    }

    public int WoodCapacity => woodCapacity;

    public void DecreaseWoodCapacity()
    {
        woodCapacity--;
    }

    private void ShrinkTree(float size)
    {
        transform.localScale = new Vector3(size, size, size);

        GetComponent<BoxCollider>().size = colliderSize;
    }
}