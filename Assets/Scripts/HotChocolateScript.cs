using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotChocolateScript : Store
{

    [SerializeField] GameObject player;

    void Start()
    {
        price = 10;
        cropSelling = CropType.NoCrop;
        
    }

    override public void buyCrop()
    {
        print("E");
        player.GetComponent<Move>().increaseSpeed(0.1f);
    }
}
