using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject foodPrefab;

    public Transform border_Bottom;
    public Transform border_Top;
    public Transform border_Left;
    public Transform border_Right;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        int h = (int)Random.Range(border_Bottom.position.y, border_Top.position.y);
        int v = (int)Random.Range(border_Left.position.x, border_Right.position.x);

        Instantiate(foodPrefab, new Vector2(v, h), Quaternion.identity);
    }
}
