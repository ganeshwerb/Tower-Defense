using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateWrapper
{
    public Type state;
    public int duration;
}

public class GameManager : MonoBehaviour
{
    private Dictionary<Type, IState> stateInstances = new Dictionary<Type, IState>();
    private IState currentState;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private EnemyState enemyState;
    private int Timer = 15;
    int stateIndex = 0;

    private StateWrapper[] states;

    #region Monobehaviour Callbacks
    private void Awake()
    {
        stateInstances[typeof(PlayerState)] = playerState;
        stateInstances[typeof(EnemyState)] = enemyState;
    }

    private void Start()
    {
        states = new StateWrapper[]
        {
        new StateWrapper { state = typeof(PlayerState), duration = 15 },
        new StateWrapper { state = typeof(EnemyState), duration = 25 },
        };

        StartCoroutine(StartGame());
    }

    private void OnEnable()
    {
        PlayerBase.OnGameOver.AddListener(GameOver);
    }

    private void OnDisable()
    {
        PlayerBase.OnGameOver.RemoveListener(GameOver);
    }

    private void Update()
    {
        currentState?.Tick();
    }
    #endregion
    #region State Management

    private void UpdateTimer()
    {
        Timer -= 1;
        UIManager.Instance.Timer.text = Timer.ToString();
    }

    private IEnumerator StartGame()
    {
        UIManager.Instance.ToggleCanvas(0, true);
        while (true)
        {
            int n = stateIndex % 2 == 0 ? 0 : 1;
            if (n == 1)
            {
                states[1].duration += 1;
                enemyState.enemyCount += 15;
            }
            ChangeState(states[n].state);
            CancelInvoke(nameof(UpdateTimer));
            Timer = states[n].duration;
            InvokeRepeating(nameof(UpdateTimer), 0, 1);

            yield return new WaitForSeconds(states[n].duration);
            stateIndex++;
        }
    }


    private void ChangeState(Type stateType)
    {
        if (stateInstances.TryGetValue(stateType, out var newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Init();
        }
        else
        {
            Debug.LogError($"State of type {stateType.Name} not found!");
        }
    }

    #endregion State Management
    public void GameOver()
    {
        StopAllCoroutines();
    }
}