using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPanel : MonoBehaviour
{
    public Slider loadingBar; // 绑定进度条UI组件
    public Text loadingText; // 绑定用于显示加载信息的Text组件

    // 协程用于加载下一个场景
    private IEnumerator LoadNextScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int currentSceneIndex = activeScene.buildIndex;

        // 确保当前场景索引有效，并且有下一个场景可加载
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // 异步加载下一个场景
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);

            // 显示加载界面
            ShowLoadingUI();

            while (!asyncLoad.isDone)
            {
                // 计算并更新加载进度
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                loadingBar.value = progress;
                loadingText.text = "Loading... " + (progress * 100).ToString("F2") + "%";

                // 协程暂停，等待下一帧继续
                yield return null;
            }

            // 隐藏加载界面
            HideLoadingUI();
        }
        else
        {
            Debug.LogError("No more scenes to load or the build index is out of range.");
        }
    }

    // 显示加载界面的方法
    void ShowLoadingUI()
    {
        loadingBar.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
    }

    // 隐藏加载界面的方法
    void HideLoadingUI()
    {
        loadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }

    // 游戏开始时调用协程
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }
}