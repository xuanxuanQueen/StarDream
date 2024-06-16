using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPanel : MonoBehaviour
{
    public Slider loadingBar; // �󶨽�����UI���
    public Text loadingText; // ��������ʾ������Ϣ��Text���

    // Э�����ڼ�����һ������
    private IEnumerator LoadNextScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int currentSceneIndex = activeScene.buildIndex;

        // ȷ����ǰ����������Ч����������һ�������ɼ���
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // �첽������һ������
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);

            // ��ʾ���ؽ���
            ShowLoadingUI();

            while (!asyncLoad.isDone)
            {
                // ���㲢���¼��ؽ���
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                loadingBar.value = progress;
                loadingText.text = "Loading... " + (progress * 100).ToString("F2") + "%";

                // Э����ͣ���ȴ���һ֡����
                yield return null;
            }

            // ���ؼ��ؽ���
            HideLoadingUI();
        }
        else
        {
            Debug.LogError("No more scenes to load or the build index is out of range.");
        }
    }

    // ��ʾ���ؽ���ķ���
    void ShowLoadingUI()
    {
        loadingBar.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
    }

    // ���ؼ��ؽ���ķ���
    void HideLoadingUI()
    {
        loadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }

    // ��Ϸ��ʼʱ����Э��
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }
}