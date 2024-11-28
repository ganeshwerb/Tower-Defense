using UnityEngine;

public class PlayerState : MonoBehaviour,IState
{
    [Header("Grid Properties")]
    public int rows = 10;
    public int columns = 10;
    public float cellSize = 5f;
    public GameObject cellPrefab;
    public Material defaultMaterial;
    public Material highlightMaterial;
    private GameObject[,] gridArray;

    [Header("Grid Selection")]
    private GameObject lastHighlightedCell;
    public Tile currentSelectedTile;
    [SerializeField] private LayerMask hitLayerMask;
    public GameManager gameManager;
    [SerializeField] private TowerManager towerManager;

    #region Grid System
    private void Start()
    {
        GenerateGrid();
    }

    public void Init()
    {
        transform.SetPositionAndRotation(transform.position + Vector3.up * 10, transform.rotation);
    }

    public void Exit()
    {
        UIManager.Instance.ToggleCanvas(1, false);
        transform.SetPositionAndRotation(transform.position - Vector3.up * 10, transform.rotation);
    }


    private void GenerateGrid()
    {
        gridArray = new GameObject[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Vector3 position = new Vector3(x * cellSize + transform.position.x, 3, y * cellSize + transform.position.y);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.transform.localScale = Vector3.one / 10 * cellSize;
                cell.GetComponent<Renderer>().material = defaultMaterial;
                cell.tag = "GridCell"; 
                gridArray[x, y] = cell;
            }
        }
    }

    public void Tick()
    {
        if (UIManager.Instance.towerSelectionActive)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit,100,hitLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("GridCell"))
            {
                if (lastHighlightedCell != hitObject)
                {
                    ResetLastHighlightedCell();
                    lastHighlightedCell = hitObject;
                    hitObject.GetComponent<Renderer>().material = highlightMaterial;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    currentSelectedTile = hitObject.GetComponent<Tile>();
                    UIManager.Instance.ToggleCanvas(1, true);
                }
            }
        }
        else
        {
            ResetLastHighlightedCell();
        }
    }

    private void ResetLastHighlightedCell()
    {
        if (lastHighlightedCell != null)
        {
            lastHighlightedCell.GetComponent<Renderer>().material = defaultMaterial;
            lastHighlightedCell = null;
        }
    }

    private void ResetCells()
    {
        foreach (GameObject cell in gridArray)
        {
            if (cell != null)
                cell.GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    private void RemoveCellFromGrid(Vector3 cellPosition)
    {
        int xIndex = Mathf.RoundToInt(cellPosition.x / cellSize);
        int yIndex = Mathf.RoundToInt(cellPosition.z / cellSize);
        if (xIndex >= 0 && xIndex < rows && yIndex >= 0 && yIndex < columns)
        {
            gridArray[xIndex, yIndex] = null;
        }
    }
    #endregion

    #region Tower Placement
    public void OnSelectTower(int n)
    {
        switch (n)
        {
            case 0:
                if (GoldManager.gold < towerManager.Marshal.cost)
                    return;
                Instantiate(towerManager.Marshal, currentSelectedTile.transform.position, currentSelectedTile.transform.rotation);
                RemoveCellFromGrid(currentSelectedTile.SelectCell());
                lastHighlightedCell = null;
                GoldManager.SpendGold(towerManager.Marshal.cost);
                UIManager.Instance.UpdateGold();

                break;

            case 1:
                if (GoldManager.gold < towerManager.Odin.cost)
                    return;
                Instantiate(towerManager.Odin, currentSelectedTile.transform.position, currentSelectedTile.transform.rotation);
                RemoveCellFromGrid(currentSelectedTile.SelectCell());
                lastHighlightedCell = null;
                GoldManager.SpendGold(towerManager.Odin.cost);
                UIManager.Instance.UpdateGold();
                break;
        }
    }
    #endregion
}
