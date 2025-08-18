using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject snakePrefab;
    public GameObject slimePrefab;
    public GameObject goblinPrefab;

    GameObject snake;
    GameObject slime;
    GameObject goblin;

    void Start()
    {
        snake = GameObject.Instantiate(snakePrefab);
        slime = GameObject.Instantiate(slimePrefab);
        goblin = GameObject.Instantiate(goblinPrefab);

        GameObject go = new GameObject() { name = "@Monsters" };
        snake.transform.parent = go.transform;
        //_slime.transform.parent = go.transform;
        goblin.transform.parent = go.transform;

        snake.name = snakePrefab.name;
        slime.name = slimePrefab.name;
        goblin.name = goblinPrefab.name;

        slime.AddComponent<PlayerController>();
        Camera.main.GetComponent<CameraController>().target = slime;
    }

    void Update()
    {
        
    }
}
