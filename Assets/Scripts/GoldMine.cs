using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GoldMine : MonoBehaviour
{
    private int goldCapacity;
    private Vector3 initialColliderSize;
    private Vector3 colliderSize;
    void Start()
    {
        goldCapacity = 3;
        initialColliderSize = GetComponent<BoxCollider>().size;
    }
    
    void Update()
    {
        switch (goldCapacity)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                colliderSize = initialColliderSize * (15f / 5f);
                ShrinkGoldMine(5f);
                break;
            case 2:
                colliderSize = initialColliderSize * (15f / 10f);
                ShrinkGoldMine(10f);
                break;
        }
    }

    public int GoldCapacity => goldCapacity;

    public void DecreaseGoldCapacity()
    {
        goldCapacity--;
    }
    
    private void ShrinkGoldMine(float size)
    {
        transform.localScale = new Vector3(size,size,size);
        GetComponent<BoxCollider>().size = colliderSize;
    }
}
