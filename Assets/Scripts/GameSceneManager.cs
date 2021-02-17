
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public Text loadingText;

    public void LoadGame (string gameLoad)
    {
        StartCoroutine(LoadSceneAsync(gameLoad));
    }

    IEnumerator LoadSceneAsync ( string gameLoad )
    {
        loadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(gameLoad);

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
	    //Debug.Log(op.progress);
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            yield return null;

            
        }
    }
}
