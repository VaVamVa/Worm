using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

Vector2 dir = Vector2.right;

List<Transform> tail = new List<Transform>();

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")) {
            dir = Vector2.up;
        } else if (Input.GetKey("down")) {
            dir = Vector2.down;
        } else if (Input.GetKey("left")) {
            dir = Vector2.left;
        } else if (Input.GetKey("right")) {
            dir = Vector2.right;
        }
    }

    void Move () {
        Vector2 v = transform.position;

        transform.Translate(dir);

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(snakePrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);
            ate = false;
        }

        else if (tail.Count > 0) 
        {
            tail.Last().position = v;
            tail.insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void InTriggerEnter2D(Collider2D coll) {
        if (coll.name.StartsWith("Food_Prefab")) {
            ate = true;
            Destroy(coll.gameObject)
        } else {
            Time.timeScale = 0;
        }
    }
}
