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

    void Start()
    {
        radius = background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���� Ŭ�� ��ǥ
        touchPosition = eventData.position;
        background.transform.position = touchPosition;
        handler.transform.position = touchPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handler.transform.position = touchPosition;
        moveDir = Vector2.zero;

        Managers.Game.MoveDir = moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 조이스틱 조종
        Vector2 touchDir = (eventData.position - touchPosition);
        float moveDist = Mathf.Min(touchDir.magnitude, radius);
        moveDir = touchDir.normalized;

        Vector2 nesPos = touchPosition + moveDir * moveDist;
        handler.transform.position = nesPos;

        // 플레이어 이동
        Managers.Game.MoveDir = moveDir;
    }

}
