using UnityEngine;
using UnityEngine.UI;

public class ControlsScreenScript : MonoBehaviour 
{
	[SerializeField] private Button _selectedButton;
	[SerializeField] private Button _onDisableSelect;

    private void OnEnable() => _selectedButton?.Select();
    private void OnDisable() => _onDisableSelect?.Select();
}
