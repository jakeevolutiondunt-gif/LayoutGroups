using System.Collections.Generic;
using UnityEngine;

public class LevelMenuGenerator : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private int totalLevels = 20;

    [Header("Look settings")]
    [Tooltip("Порядковый номер, с которого уровни заблокированы")]
    [SerializeField] private int lockStartingFrom = 18;

    [Header("Stars count")]
    [Tooltip("Задать определённое число звёзд вручную. Если список пуст, то сгенерируется случайное число от 0 до 3")]
    [SerializeField] private List<int> specificStarsPerLevel = new List<int>();
    
    private bool isLocked = false;

    private void Start()
    {
        GenerateMenu();
    }

    public void GenerateMenu()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        lockStartingFrom = Mathf.Clamp(lockStartingFrom, 1, totalLevels + 1);

        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject newLevelObj = Instantiate(levelPrefab, container);
            Level levelItem = newLevelObj.GetComponent<Level>();

            if (levelItem != null)
            {
                isLocked = i >= lockStartingFrom;

                int starsCount = 0;
                if (!isLocked)
                {
                    if (specificStarsPerLevel != null && (i - 1) < specificStarsPerLevel.Count) starsCount = specificStarsPerLevel[i - 1];
                    else starsCount = Random.Range(0, 3);
                }
                levelItem.SetupLevel(i, isLocked, starsCount);
            }
        }
    }
}
