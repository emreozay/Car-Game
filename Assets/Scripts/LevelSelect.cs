using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject levetButton;
    private int levelNumber;

    private void Start()
    {
        levelNumber = SceneManager.sceneCountInBuildSettings - 2;

        for (int i = 0; i < levelNumber; i++)
        {
            var newLevelButton = Instantiate(levetButton);
            newLevelButton.transform.SetParent(transform);
            newLevelButton.transform.localScale = new Vector3(1, 1, 1);

            if(PlayerPrefs.GetInt("Level", 1) < i + 1)
            {
                newLevelButton.GetComponent<Button>().interactable = false;
            }

            int levelIndex = i + 1;
            newLevelButton.GetComponentInChildren<TextMeshProUGUI>().text = (levelIndex).ToString();
            newLevelButton.GetComponent<Button>().onClick.AddListener(delegate { SelectLevel(levelIndex); });
        }
    }

    private void SelectLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
