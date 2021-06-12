using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace InputSystem
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] Button _continue;

        public Button ContinueButton => _continue;
    }
}