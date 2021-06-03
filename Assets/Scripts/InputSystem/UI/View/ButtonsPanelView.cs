﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Abstractions;
using System.Collections.Generic;
using System.Linq;
using TMPro;


public class ButtonsPanelView : MonoBehaviour
{
    [SerializeField] private Button _produceUnitButton;
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _stopButton;

    private Dictionary<Type, Button> _buttons;

    public Action<ICommandExecutor> OnClick;

    protected void Start()
    {       
        _buttons = new Dictionary<Type, Button>
        {
            { typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton },
            { typeof(CommandExecutorBase<IMoveCommand>), _moveButton },
            { typeof(CommandExecutorBase<IAttackCommand>), _attackButton },
            { typeof(CommandExecutorBase<IStopCommand>), _stopButton},
            { typeof(CommandExecutorBase<IPatrolCommand>), _produceUnitButton }
        };

        ClearButtons();
    }

    public void SetButtons(List<ICommandExecutor> commandExecutors)
    {
        if (commandExecutors == null) return;

        foreach (var executor in commandExecutors)
        {
            var button = _buttons.FirstOrDefault(kvp => kvp.Key.IsInstanceOfType(executor)).Value;            

            if (button != null)
            {
                if (button.GetComponentInChildren<TMP_Text>() != null)
                {
                    if (executor as CommandExecutorBase<IProduceUnitCommand>) 
                        button.GetComponentInChildren<TMP_Text>().SetText("Produce Unit");
                    if (executor as CommandExecutorBase<IPatrolCommand>) 
                        button.GetComponentInChildren<TMP_Text>().SetText("Patrol");                    
                }

                button.interactable = true;
                button.onClick.AddListener(() => OnClick?.Invoke(executor));
            }
            else Debug.LogError("No button founded for executor " + executor.GetType());
        }
    }

    public void ClearButtons()
    {
        foreach (var kvp in _buttons)
        {            
            kvp.Value.gameObject.GetComponent<Button>().interactable = false;
            kvp.Value.onClick.RemoveAllListeners();
        }
    }
}
