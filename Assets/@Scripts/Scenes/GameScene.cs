using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject snakePrefab;
    public GameObject slimePrefab;
    public GameObject goblinPrefab;
    public GameObject joystickPrefab;

    GameObject snake;
    GameObject slime;
    GameObject goblin;
    GameObject joystick;

    void Start()
    {
        snake = GameObject.Instantiate(snakePrefab);
        slime = GameObject.Instantiate(slimePrefab);
        goblin = GameObject.Instantiate(goblinPrefab);
        joystick = GameObject.Instantiate(joystickPrefab);

        GameObject go = new GameObject() { name = "@Monsters" };
        snake.transform.parent = go.transform;
        //_slime.transform.parent = go.transform;
        goblin.transform.parent = go.transform;

        snake.name = snakePrefab.name;
        slime.name = slimePrefab.name;
        goblin.name = goblinPrefab.name;

        slime.AddComponent<PlayerController>();
        Camera.main.GetComponent<CameraController>().target = slime;
        joystick.name = "@UI_Joystick";
    }

    void Update()
    {
        
    }
}
