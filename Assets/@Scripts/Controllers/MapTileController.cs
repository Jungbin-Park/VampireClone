using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맵 타일 하나에 대한 컨트롤러
public class MapTileController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera camera = collision.gameObject.GetComponent<Camera>();
        if (camera == null)
            return;

        // 카메라가 벗어났을 때 이동 방향
        Vector3 dir = camera.transform.position - transform.position;

        float dirX = dir.x < 0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        // x, y 중 더 멀리 떨어진 방향으로 타일 이동
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            transform.Translate(Vector3.right * dirX * 200);
        else
            transform.Translate(Vector3.up * dirY * 200);
    }
}
