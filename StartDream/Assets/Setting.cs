using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public GameObject originalWindow;
    public GameObject settingsWindow;
    public Image settingsMask;

    // 显示设置窗口并激活遮罩层
    public void ShowSettingsWindow()
    {
        settingsWindow.SetActive(true);
        settingsMask.enabled = true; // 或者 settingsMask.gameObject.SetActive(true);
    }

    // 隐藏设置窗口并禁用遮罩层
    public void HideSettingsWindow()
    {
        settingsWindow.SetActive(false);
        settingsMask.enabled = false; // 或者 settingsMask.gameObject.SetActive(false);
    }
}
