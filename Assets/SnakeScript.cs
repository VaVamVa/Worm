using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SnakeScript : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    public GameObject snakePrefab;
    List<Transform> tail = new List<Transform>();
    bool ate = false;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            dir = Vector2.up;
        } else if (Input.GetKey("s")) {
            dir = Vector2.down;
        } else if (Input.GetKey("a")) {
            dir = Vector2.left;
        } else if (Input.GetKey("d")) {
            dir = Vector2.right;
        }
    }

    void Move () {
        transform.Translate(dir);
        Vector2 v = transform.position;

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(snakePrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);
            ate = false;
        }

        else if (tail.Count > 0) 
        {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name.StartsWith("foodPrefab")) {
            ate = true;
            Destroy(coll.gameObject);
        } else {
            Time.timeScale = 0;
        }
        
    }
}
