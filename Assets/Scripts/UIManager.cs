using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject towerSelection;
    public TextMeshProUGUI baseHp;
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI Timer;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    public bool towerSelectionActive => towerSelection.activeSelf;
    private void OnEnable()
    {
        UpdateGold();
        PlayerBase.OnGameOver.AddListener(GameOver);
    }

    private void OnDisable()
    {
        PlayerBase.OnGameOver.RemoveListener(GameOver);
    }

    public void ToggleCanvas(int n,bool toggle)
    {
        switch (n)
        {
            case 0:
                hud.SetActive(toggle); break;
            case 1:
                towerSelection.SetActive(toggle); break;
        }
    }

    public void UpdateGold()
    {
        goldAmount.text = GoldManager.gold.ToString();
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
