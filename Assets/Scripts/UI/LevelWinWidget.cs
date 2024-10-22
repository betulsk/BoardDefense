using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinWidget : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
