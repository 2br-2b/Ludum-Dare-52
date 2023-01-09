using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGround : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    [SerializeField] int gridSize= 5;
    [SerializeField] GameObject gameRunningManager;
    
    // Start is called before the first frame update
    void Start()
    {
        generateGround();
    }

    void generateGround()
    {
        int tileSize = 1;

        for (int i=0; i < gridSize; i++)
        {
            for(int j=0; j < gridSize; j++)
            {
                GameObject tile = Instantiate(groundPrefab, new Vector3((i * tileSize) - transform.position.x, (j * tileSize) - transform.position.y, 0) * -1, Quaternion.identity);
                tile.transform.parent = transform;
                tile.transform.rotation = Quaternion.Euler(0, 0, 0);
                tile.tag = "Ground";
                tile.GetComponent<PlantState>().gameRunningManager = gameRunningManager;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
