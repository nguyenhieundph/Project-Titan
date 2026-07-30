using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private TextMeshProUGUI _questText;

    private void OnEnable()
    {
        _questManager.OnQuestProgressChanged += UpdateQuestText;
    }

    private void OnDisable()
    {
        _questManager.OnQuestProgressChanged -= UpdateQuestText;
    }

    private void Start()
    {
        UpdateQuestText();
    }

    private void UpdateQuestText()
    {
        // TODO: gán _questText.text theo định dạng:
        //   nếu chưa hoàn thành: "{QuestName}: {CunrrentKillCount}/{RequiredKillCount}"    
        //   nếu đã hoàn thành: "{QuestName}: Completed!"
        if (_questManager.IsCompleted == true) 
        {
            _questText.text = $"{_questManager.QuestName}: Completed!";
        }
        else 
        {
            _questText.text = $"{_questManager.QuestName}: {_questManager.CunrrentKillCount}/{_questManager.RequiredKillCount} ";
        }
    }
}
