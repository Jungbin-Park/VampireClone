using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cell
{
    public HashSet<GameObject> Objects { get; } = new HashSet<GameObject>();
}

public class GridController : BaseController
{
    Grid grid;

    Dictionary<Vector3Int, Cell> cells = new Dictionary<Vector3Int, Cell>();

    public override bool Init()
    {
        base.Init();

        grid = gameObject.GetOrAddComponent<Grid>();

        return true;
    }

    public void Add(GameObject go)
    {
        // 월드좌표를 보내주면 그에 해당하는 셀 번호 반환
        Vector3Int cellPos = grid.WorldToCell(go.transform.position);

        Cell cell = GetCell(cellPos);
        if (cell == null)
            return;

        cell.Objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        Vector3Int cellPos = grid.WorldToCell(go.transform.position);

        Cell cell = GetCell(cellPos);
        if (cell == null)
            return;

        cell.Objects.Remove(go);
    }

    Cell GetCell(Vector3Int cellPos)
    {
        Cell cell = null;

        if(cells.TryGetValue(cellPos, out cell) == false)
        {
            cell = new Cell();
            cells.Add(cellPos, cell);
        }

        return cell;
    }

    public List<GameObject> GatherObjects(Vector3 pos, float range)
    {
        if (grid == null)
        {
            grid = gameObject.GetComponent<Grid>();
        }

        List<GameObject> objects = new List<GameObject>();

        Vector3Int left = grid.WorldToCell(pos + new Vector3(-range, 0));
        Vector3Int right = grid.WorldToCell(pos + new Vector3(+range, 0));
        Vector3Int bottom = grid.WorldToCell(pos + new Vector3(0, -range));
        Vector3Int top = grid.WorldToCell(pos + new Vector3(0, +range));

        int minX = left.x;
        int maxX = right.x;
        int minY = bottom.y;
        int maxY = top.y;

        for(int x = minX; x <= maxX; x++)
        {
            for(int y = minY; y <= maxY; y++)
            {
                if (cells.ContainsKey(new Vector3Int(x, y, 0)) == false)
                    continue;

                objects.AddRange(cells[new Vector3Int(x, y, 0)].Objects);
            }
        }

        return objects;
    }
}
