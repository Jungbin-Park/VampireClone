using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillSelectPopup : UI_Base
{
    [SerializeField]
    Transform grid;

    List<UI_SkillCardItem> items = new List<UI_SkillCardItem>(); 

    void Start()
    {
        PopulateGrid();
    }

    void PopulateGrid()
    {
        // 그리드의 기존 아이템들을 날려줌
        foreach(Transform t in grid.transform)
        {
            Managers.Resource.Destroy(t.gameObject);
        }

        // 우선 3개. 개수가 많아지면 데이터로?
        // or 일단 다 만들어놓고 활성화/비활성화
        for(int i = 0; i < 3; i++)
        {
            var go = Managers.Resource.Instantiate("UI_SkillCardItem.prefab", pooling: false);
            UI_SkillCardItem item = go.GetOrAddComponent<UI_SkillCardItem>();

            item.transform.SetParent(grid.transform);

            items.Add(item);
        }
    }
}
