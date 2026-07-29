using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private Transform _playertranform;
    [SerializeField] private ItemDatabase _itemDatabase;

    private string savePath => Path.Combine(Application.persistentDataPath, "savefile.json") ;
    
    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.playerPositionX = _playertranform.position.x;
        data.playerPositionY = _playertranform.position.y;
        data.playerPositionZ = _playertranform.position.z;

        data.playerHealth = _playerHealth.GetSaveData();

        data.inventorySlotDatas = _playerInventory.GetSaveData();

        data.activeQuestId = _questManager.GetSaveData_QuestId();
        data.QuestKillCount = _questManager.GetSaveData_KillCount();
        data.isQuestCompleted = _questManager.GetSaveData_IsCompleted();

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);
        
        Debug.Log($"Game saved to: {savePath}");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath) == false) 
        {
            Debug.Log("No save file found.");
            return;
        }
      
        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        CharacterController controller = _playertranform.GetComponent<CharacterController>();
        controller.enabled = false;
        _playertranform.position = new Vector3(data.playerPositionX, data.playerPositionY, data.playerPositionZ);
        controller.enabled = true;

        _playerHealth.LoadSaveData(data.playerHealth);
        _playerInventory.LoadSaveData(data.inventorySlotDatas, _itemDatabase);
        _questManager.LoadSaveData(data.QuestKillCount, data.isQuestCompleted);

        Debug.Log("Game loaded");


    }

    private void Update()
    {
        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            SaveGame();
        }
        if (Keyboard.current.f9Key.wasPressedThisFrame)
        {
            LoadGame();
        }
    }
}
