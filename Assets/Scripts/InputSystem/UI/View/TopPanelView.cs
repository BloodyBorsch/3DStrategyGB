using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


namespace InputSystem
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] TMP_Text _time;
        [SerializeField] Button _menuButton;

        public int Time
        {
            set
            {
                _time.text = TimeSpan.FromSeconds(value).ToString(); 
            }
        }

        public Button MenuButton => _menuButton;
    }
}