using Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;


namespace InputSystem
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField] MenuView _menuView;
        private ITimeModel _timeModel;
                
        public void Initialize(ITimeModel timeModel)
        {
            _timeModel = timeModel;
            _menuView.ContinueButton.OnClickAsObservable().Subscribe(unit => HandleContinueButtonClick());
        }

        private void HandleContinueButtonClick()
        {
            _timeModel.UnPause();
            gameObject.SetActive(false);
        }
    }
}