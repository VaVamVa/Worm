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

    int score = 0;

    private float Timer;
    private DelayTimeMain DelayCount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
        GameObject.FindWithTag("GM").SendMessage("Spawn");

        GameObject Board = GameObject.Find("Score").GetComponent<TextMesh>();
        Board.text = "점수: " + score;
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
        if (DelayCount.DelayCount == 0) {
            Timer = Timer + Time.deltaTime;
            text.text = string.Format("{0:N1}", Timer);
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
        Debug.Log(coll.name);
        if (coll.name.StartsWith("foodPrefab")) {
            socre += 10;
            ate = true;
            Destroy(coll.gameObject);
            GameObject.FindWithTag("GM").SendMessage("Spawn");
            Board.text = "점수: " + score;

        } else {
            Time.timeScale = 0;
        }
        
    }
}
