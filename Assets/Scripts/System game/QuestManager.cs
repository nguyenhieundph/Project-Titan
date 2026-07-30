using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Settings")]
    [SerializeField] private Quest _activeQuest;
    [SerializeField] private Inventory _playerInventory;

    private int _cunrrentKillCount;
    private bool _isCompleted;

    public event Action OnQuestProgressChanged;

    public int CunrrentKillCount => _cunrrentKillCount;
    public int RequiredKillCount => _activeQuest.requiredKillCount;
    public string QuestName => _activeQuest.questName;
    public bool IsCompleted => _isCompleted;


    private void OnEnable()
    {
        EnemyMovement.OnAnyEnemyDied += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyMovement.OnAnyEnemyDied -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled()
    {
        if (_activeQuest == null || _isCompleted)
            return;

        _cunrrentKillCount++;

        if (_cunrrentKillCount >= _activeQuest.requiredKillCount)
        {
            _isCompleted = true;

            for(int i = 0; i < _activeQuest.rewardQuantity; i++)
            {
                _playerInventory.AddItem(_activeQuest.rewardItem);
            }

            Debug.Log("Quest Completed!");
        }

        OnQuestProgressChanged?.Invoke();
    }

    public string GetSaveData_QuestId()
    {
        return _activeQuest.questId;
    }

    public int GetSaveData_KillCount()
    {
        return _cunrrentKillCount;
    }

    public bool GetSaveData_IsCompleted()
    {
        return _isCompleted;
    }

    public void LoadSaveData(int savedKillCount, bool savedIsCompleted)
    {
        _cunrrentKillCount = savedKillCount;
        _isCompleted = savedIsCompleted;
        OnQuestProgressChanged?.Invoke();
    }
}
