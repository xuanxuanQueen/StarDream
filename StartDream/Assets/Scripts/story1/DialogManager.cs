using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.U2D;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //对话文本文件，csv格式
    public TextAsset dialogDataFile;
    public SpriteRenderer spriteRole;
    public TMP_Text nameText;
    public TMP_Text dialogText;

    public List<Sprite> sprites = new List<Sprite>();

    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    public Button nextButton;
    public GameObject optionButton;
    public Transform buttonGroup;//选项按钮父节点，用于自动排列
    public int dialogIndex;
    public string[] dialogRows;
    private void Awake()
    {
        imageDic["方衍"] = sprites[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        /*UpdateText("方衍", "你是谁");
        UpdatImage("方衍",true);*/
    }

    // Update is called once per frame
    void Update()
    {
        ReadText(dialogDataFile);
    }

    public void UpdateText(string _name,string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }
    public void UpdateImage(string _name)
    {
        if (name=="方衍"|| name == "我")
        {
            spriteRole.sprite = imageDic[_name];
        }
    }
    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
       /* foreach (var row in rows)
        {
            string[] cell = row.Split(',');
        }*/
        Debug.Log("读取成功");
    }
    public void ShowDialogRow()
    {
        for (int i=0;i<dialogRows.Length;i++)
        {
            string[] cells = dialogRows[i].Split(',');
            
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[4], cells[2]);
                UpdateImage(cells[4]);

                dialogIndex = int.Parse(cells[3]);
                nextButton.gameObject.SetActive(true);
                break;
            }
            else if (cells[0]=="&"&& int.Parse(cells[1]) == dialogIndex)
            {
                nextButton.gameObject.SetActive(false);
                GenerateOption(i);
            }
        }
    }
    public void OnClickNext()
    {
        //dialogIndex++;
        ShowDialogRow();
        
    }

    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[2];
            button.GetComponent<Button>().onClick.AddListener
                (
                delegate {
                    OnOptionClick(int.Parse(cells[3])); 
                    }
                );
            GenerateOption(_index + 1);
        }
        
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for(int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
            
        }
    }
}
