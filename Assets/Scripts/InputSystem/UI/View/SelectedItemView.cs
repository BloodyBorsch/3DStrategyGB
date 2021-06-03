using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectedItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _healthBarText;
    [SerializeField] private TMP_Text _backGroundText;
    [SerializeField] private Slider _healthBar;

    public Sprite Icon { set => _icon.sprite = value; }
    public string Text { set => _healthBarText.text = value; }
    public string BackGroundText { set => _backGroundText.text = value; }
    public float Health { set => _healthBar.value = value; }
    public float MaxHealth { set => _healthBar.maxValue = value; }    
}
