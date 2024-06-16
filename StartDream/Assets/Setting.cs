using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public GameObject originalWindow;
    public GameObject settingsWindow;
    public Image settingsMask;

    // ��ʾ���ô��ڲ��������ֲ�
    public void ShowSettingsWindow()
    {
        settingsWindow.SetActive(true);
        settingsMask.enabled = true; // ���� settingsMask.gameObject.SetActive(true);
    }

    // �������ô��ڲ��������ֲ�
    public void HideSettingsWindow()
    {
        settingsWindow.SetActive(false);
        settingsMask.enabled = false; // ���� settingsMask.gameObject.SetActive(false);
    }
}
