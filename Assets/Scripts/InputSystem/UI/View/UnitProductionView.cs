using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Abstractions;
using System;
using UniRx;


namespace View
{
    public class UnitProductionView : MonoBehaviour
    {
        [SerializeField] private List<Image> _images;

        [SerializeField] private Sprite _emptyIcon;
        [SerializeField] private Image _currentProduction;
        [SerializeField] private TMP_Text _currentTime;
        [SerializeField] private Slider _currentProgress;

        public void DisplayQueue(IList<IUnitProductionTask> productionTasks)
        {
            if (productionTasks.Count > 0)
            {
                var currentTask = productionTasks[0];
                _currentProduction.sprite = currentTask.Icon;

                currentTask.ProductionTimeLeft.Subscribe((timeLeft) =>
                {
                    UpdateTimeProgress(timeLeft, currentTask.ProductionTime);
                });
                
            }

            for (int i = 1; i < productionTasks.Count; i++)
            {
                _images[i - 1].sprite = productionTasks[0].Icon;
            }
        }

        private void UpdateTimeProgress(float timeLeft, float fullProdTime)
        {
            _currentTime.text = TimeSpan.FromSeconds((int)timeLeft).ToString();
            _currentProgress.maxValue = timeLeft;
            _currentProgress.value = timeLeft / fullProdTime;
        }

        public void ClearAll()
        {
            _currentProduction.sprite = _emptyIcon;
            _currentTime.text = TimeSpan.FromSeconds(0).ToString();
            _currentProgress.maxValue = 0;
            _currentProgress.value = 0;

            foreach (var image in _images)
            {
                image.sprite = _emptyIcon;
            }            
        }

        public void AddNewItem(CollectionAddEvent<IUnitProductionTask> newElement)
        {
            if (newElement.Index == 0)
            {
                var currentTask = newElement.Value;
                _currentProduction.sprite = currentTask.Icon;
                currentTask.ProductionTimeLeft.Subscribe((timeLeft) =>
                {
                    UpdateTimeProgress(timeLeft, currentTask.ProductionTime);
                });
            }
            else
            {
                _images[newElement.Index - 1].sprite = newElement.Value.Icon;
            }
        }
    }
}