using System.Collections;
using System.Collections.Generic;
using Damon.Table;
using Damon.Tool;
using UnityEngine;
using UnityEngine.UI;
public interface IDataSet { }

public class TableMenu : MonoBehaviour, ILog {
    public GameObject prefab;
    private List<GameObject> list = new List<GameObject> ();
    private TableFactory tableFactory;

    [SerializeField]
    private int index = 0;
    [SerializeField]
    private int subIndex = 0;

    public GameObject tableMenu;
    public GameObject tableItem;

    private List<GameObject> menuList;
    private List<GameObject> itemList;
    void Start () {
        tableFactory = TableFactory.Instance;
        InitView ();
    }
    //数据模块填充数据
    void InitView () {

        //初始化父表 
        menuList = GameObjectTool.GenerateGos (prefab, tableFactory.count, tableMenu);
        GameObjectTool.SetPositionHorizontal (menuList, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
        for (int i = 0; i < menuList.Count; i++) {
            GameObject temp = menuList[i];
            temp.GetComponentInChildren<Text> ().text = tableFactory[i].name;
            temp.AddComponent<Index> ().index = i;
            temp.GetComponent<Button> ().onClick.AddListener (() => {
                int selectedIndex = temp.GetComponent<Index> ().index;
                if (selectedIndex == index) return;
                ResetSelected (0);
                this.index = temp.GetComponent<Index> ().index;
                SetSelected (0);
            });
        }

        // 初始化子表
        BaseTable table = tableFactory[index];
        table.InitData ();
        SetMenuItem (table);
        menuList[index].Disable<Button> ();
        itemList[subIndex].Disable<Button> ();
    }

    void SetSelected (int type) {
        if (type == 0) {
            this.d ("damon", "父对象 选中" + index, false);
            menuList[index].Disable<Button> ();
            BaseTable table = tableFactory[index];
            table.InitData ();
            SetMenuItem (table);
        } else if (type == 1) {
            this.d ("damon", "子对象对象 选中" + subIndex, false);
            itemList[subIndex].Disable<Button> ();
        }
    }
    void ResetSelected (int type) {
        if (type == 0) {
            menuList[index].Enable<Button> ();
            for (int i = 0; i < itemList.Count; i++) {
                itemList[i].DestroySelf ();
            }
            this.d ("damon", "父对象 重置选中" + index, false);
        } else if (type == 1) {
            itemList[subIndex].Enable<Button> ();
            this.d ("damon", "子对象对象 重置选中" + subIndex, false);
        }
    }

    void SetMenuItem (BaseTable table) {
        itemList = GameObjectTool.GenerateGos (prefab, table.count, tableItem);
        GameObjectTool.SetPositionHorizontal (itemList, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
        for (int i = 0; i < itemList.Count; i++) {
            GameObject temp = itemList[i];
            temp.GetComponentInChildren<Text> ().text = table[i].name;
            temp.AddComponent<Index> ().index = i;
            temp.GetComponent<Button> ().onClick.AddListener (() => {
                int selectedIndex = temp.GetComponent<Index> ().index;
                if (selectedIndex == subIndex) return;
                ResetSelected (1);
                this.subIndex = temp.GetComponent<Index> ().index;
                SetSelected (1);
            });
        }
    }
    void Update () { }
}