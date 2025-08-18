using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    Image background;

    [SerializeField]
    Image handler;

    float radius;
    Vector2 touchPosition;
    Vector2 moveDir;

    PlayerController player;

    void Start()
    {
        radius = background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        player = GameObject.Find("Slime_01").GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 최초 클릭 좌표
        touchPosition = eventData.position;
        background.transform.position = touchPosition;
        handler.transform.position = touchPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handler.transform.position = touchPosition;
        moveDir = Vector2.zero;

        // temp1
        //player.MoveDir = moveDir;
        // temp2
        Managers.moveDir = moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDir = (eventData.position - touchPosition);
        float moveDist = Mathf.Min(touchDir.magnitude, radius);
        moveDir = touchDir.normalized;

        Vector2 nesPos = touchPosition + moveDir * moveDist;
        handler.transform.position = nesPos;

        // temp1
        //player.MoveDir = moveDir;
        // temp2
        Managers.moveDir = moveDir;
    }

}
