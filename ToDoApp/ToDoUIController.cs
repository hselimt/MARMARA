using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class ToDoUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField taskInputField;
    public TMP_InputField timeInputField;
    public Button addTaskButton;
    public RectTransform taskListContent;
    public GameObject todoItemPrefab;

    [Header("Alarm UI")]
    public GameObject alarmPanel; 
    public TMP_Text alarmText;   
    public Button closeAlarmButton;

    private DBManager _dbManager;
    private float _timer;

    private List<int> _alertedTaskIds = new List<int>();

    private void Awake()
    {
        _dbManager = DBManager.Instance;
        addTaskButton.onClick.AddListener(AddNewTask);

        if (closeAlarmButton != null)
            closeAlarmButton.onClick.AddListener(CloseAlarm);

        if (alarmPanel != null)
            alarmPanel.SetActive(false);
    }

    private void Start()
    {
        RefreshTaskList();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 5f)
        {
            CheckDeadlines();
            _timer = 0;
        }
    }

    public void AddNewTask()
    {
        if (!string.IsNullOrWhiteSpace(taskInputField.text))
        {
            ToDoItem newTask = new ToDoItem();
            newTask.Task = taskInputField.text;
            newTask.IsComplete = false;
            newTask.Time = string.IsNullOrWhiteSpace(timeInputField.text) ? "" : timeInputField.text;

            _dbManager.SaveTask(newTask);

            taskInputField.text = "";
            timeInputField.text = "";
            RefreshTaskList();
        }
    }

    public void RefreshTaskList()
    {
        foreach (Transform child in taskListContent)
        {
            Destroy(child.gameObject);
        }

        List<ToDoItem> allTasks = _dbManager.GetAllTasks();

        foreach (ToDoItem task in allTasks)
        {
            GameObject taskObject = Instantiate(todoItemPrefab, taskListContent);
            ToDoItemDisplay display = taskObject.GetComponent<ToDoItemDisplay>();

            display.taskText.text = task.Task;
            display.taskToggle.isOn = task.IsComplete;
            if (display.timeText != null) display.timeText.text = task.Time;

            display.taskToggle.onValueChanged.AddListener((isComplete) => OnToggleTask(task.Id, isComplete));
            display.deleteButton.onClick.AddListener(() => OnDeleteTask(task.Id));
        }
        CheckDeadlines();
    }

    private void CheckDeadlines()
    {
        ToDoItemDisplay[] displays = taskListContent.GetComponentsInChildren<ToDoItemDisplay>();

        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        foreach (var display in displays)
        {
            if (display.timeText != null && !string.IsNullOrEmpty(display.timeText.text) && !display.taskToggle.isOn)
            {
                try
                {
                    TimeSpan taskTime = TimeSpan.Parse(display.timeText.text);

                    if (currentTime > taskTime)
                    {
                        display.taskText.color = Color.red;
                        display.timeText.color = Color.red;


                        if (!alarmPanel.activeSelf)
                        {
                            ShowAlarm("Zamaný geldi !! " + display.taskText.text);
                            return;
                        }
                    }
                }
                catch { }
            }
        }
    }

    public void ShowAlarm(string message)
    {
        if (alarmPanel != null)
        {
            alarmPanel.SetActive(true);
            alarmText.text = message;   
        }
    }

    public void CloseAlarm()
    {
        if (alarmPanel != null)
        {
            alarmPanel.SetActive(false);
        }
    }

    public void OnToggleTask(int taskId, bool isComplete)
    {
        ToDoItem taskToUpdate = _dbManager.GetTaskById(taskId);
        if (taskToUpdate != null) { taskToUpdate.IsComplete = isComplete; _dbManager.UpdateTask(taskToUpdate); }
    }

    public void OnDeleteTask(int id)
    {
        _dbManager.DeleteTask(id); RefreshTaskList();
    }
}