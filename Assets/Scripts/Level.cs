using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private Button levelButton;
    [SerializeField] private Image lockIcon;
    [SerializeField] private TextMeshProUGUI levelNumberText;

    [Header("Stars num")]
    [SerializeField] private GameObject[] activeStars;


    public void SetupLevel(int levelNumber, bool isLocked, int starsCount)
    {
        if (levelNumberText != null)
        {
            levelNumberText.text = levelNumber.ToString();
        }

        if (isLocked)
        {
            levelButton.interactable = false;
            if (lockIcon != null) lockIcon.gameObject.SetActive(true);
            if (levelNumberText != null) levelNumberText.gameObject.SetActive(false);

            SetStars(0);
        }
        else
        {
            levelButton.interactable = true;
            if (lockIcon != null) lockIcon.gameObject.SetActive(false);
            if (levelNumberText != null) levelNumberText.gameObject.SetActive(true);

            SetStars(starsCount);
        }

        levelButton.onClick.RemoveAllListeners();
        levelButton.onClick.AddListener(() => OnLevelClick(levelNumber));
    }
    
    private void SetStars(int count)
    {
        count = Mathf.Clamp(count, 0, 3);

        for (int i = 0; i < activeStars.Length; i++)
        {
            if (activeStars[i] != null) activeStars[i].SetActive(i < count);
        }
    }

    public void OnLevelClick(int levelIndex)
    {
        Debug.Log($"Запуск уровня: {levelIndex}");
        SceneManager.LoadScene("Level1");
    }
    public void BackLevelClick(int levelIndex)
    {
        SceneManager.LoadScene("Levels");
    }

    public void MenuClick(int levelIndex)
    {
        SceneManager.LoadScene("Menu");
    }
}
