using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SnakeScript : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    public GameObject snakePrefab;
    public GameObject tailPrefab;
    List<Transform> tail = new List<Transform>();
    bool ate = false;

    int score = 0;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
        GameObject.FindWithTag("GM").SendMessage("Spawn");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") && dir != Vector2.down) {
            dir = Vector2.up;
        } else if (Input.GetKey("s") && dir != Vector2.up) {
            dir = Vector2.down;
        } else if (Input.GetKey("a") && dir != Vector2.right) {
            dir = Vector2.left;
        } else if (Input.GetKey("d") && dir != Vector2.left) {
            dir = Vector2.right;
        }
    }

    void Move () {
        transform.Translate(dir);
        Vector2 v = transform.position;

        if (ate)
        {
            Debug.Log(snakePrefab);
            Debug.Log(tailPrefab);
            if (tail.Count >= 6) {
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
            }
            else {
                GameObject g = (GameObject)Instantiate(snakePrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
            }
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
        Debug.Log("isTrigger");
        Debug.Log(coll.name);
        if (coll.name.StartsWith("foodPrefab")) {
            Destroy(coll.gameObject);

            score += 10;
            GameObject Board = GameObject.Find("Score") as GameObject;
            Board.GetComponent<TextMesh>().text = "점수: " + score;

            if (score >= 200) {
                Time.timeScale = 0;
            }

            ate = true;
            GameObject.FindWithTag("GM").SendMessage("Spawn");

            CancelInvoke("Move");
            speed = score * 0.0015f;
            InvokeRepeating("Move", 0.3f - speed, 0.3f - speed);


        } else {
            Time.timeScale = 0;
        }
        
    }

}
