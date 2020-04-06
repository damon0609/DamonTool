using System.Collections;
using System.Collections.Generic;
using Damon.Table;
using Damon.Tool;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TableMenu : MonoBehaviour, ILog {
    public GameObject prefab;
    private TableFactory tableFactory;

    [SerializeField]
    private int index = 0;
    [SerializeField]
    private int subIndex = 0;

    public GameObject tableMenu;
    public GameObject tableItem;
    private List<GameObject> menuList;
    private List<GameObject> menuItemList;
    private BaseTable curTable;

    public Action<BaseTable> tableChanged;

    #region  布局逻辑
    void OnDrawLayoutMenu (List<GameObject> list) {
        GameObjectTool.SetPositionHorizontal (list, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
    }
    void OnDrawLayoutMenuItem (BaseTable table) {

        GameObjectTool.SetPositionHorizontal (menuItemList, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
    }
    #endregion

    #region  初始化视图的行为逻辑
    void InitView () {
        List<string> names = new List<string> ();
        for (int i = 0; i < tableFactory.count; i++) {
            names.Add (tableFactory[i].name);
        }
        SetMenu (menuList, names);

        names.Clear ();
        for (int i = 0; i < curTable.count; i++) {
            names.Add (curTable[i].name);
        }
        SetMenu (menuItemList, names);

        menuList[index].Disable<Button> ();
        menuList[index].GetComponentInChildren<Text> ().color = Color.red;
        menuItemList[subIndex].Disable<Button> ();
        menuItemList[subIndex].GetComponentInChildren<Text> ().color = Color.red;
    }
    void SetMenu (List<GameObject> list, List<string> strs) {

        for (int i = 0; i < list.Count; i++) {
            GameObject temp = list[i];
            temp.GetComponentInChildren<Text> ().text = strs[i];
            temp.AddComponent<Index> ().index = i;
            temp.GetComponent<Button> ().onClick.AddListener (() => {

                //这里有问题----------------
                int selectedIndex = temp.GetComponent<Index> ().index;
                if (selectedIndex == subIndex) return;
                ResetSelected (1);
                this.subIndex = temp.GetComponent<Index> ().index;
                SetSelected (1);
            });
        }
    }
    #endregion

    void SetSelected (int type) {
        if (type == 0) {
            this.d ("damon", "父对象 选中" + index, false);
            menuList[index].Disable<Button> ();
            BaseTable table = tableFactory[index];
            table.InitData ();
        } else if (type == 1) {
            this.d ("damon", "子对象对象 选中" + subIndex, false);
            menuItemList[subIndex].Disable<Button> ();
        }
    }
    void ResetSelected (int type) {
        if (type == 0) {
            menuList[index].Enable<Button> ();
            for (int i = 0; i < menuList.Count; i++) {
                menuList[i].DestroySelf ();
            }
            this.d ("damon", "父对象 重置选中" + index, false);
        } else if (type == 1) {
            menuItemList[subIndex].Enable<Button> ();
            this.d ("damon", "子对象对象 重置选中" + subIndex, false);
        }
    }

    void OnStart () {
        tableFactory = TableFactory.Instance;

        menuList = GameObjectTool.GenerateGos (prefab, tableFactory.count, tableMenu);
        OnDrawLayoutMenu (menuList);

        curTable = tableFactory[index];
        curTable.InitData ();

        menuItemList = GameObjectTool.GenerateGos (prefab, curTable.count, tableItem);
        OnDrawLayoutMenuItem (curTable);

        InitView ();
    }
    void Start () {
        OnStart ();
    }
}