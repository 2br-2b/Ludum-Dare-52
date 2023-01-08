using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] public int price = 5;
    [SerializeField] public CropType cropSelling;
    [SerializeField] public GameObject buyable = null;

    public virtual void buyCrop()
    {
        if(buyable != null)
        {
            Instantiate(buyable, transform.position, Quaternion.identity);
        }
    }

}
