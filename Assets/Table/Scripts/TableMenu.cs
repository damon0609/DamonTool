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
    void Start () {
        tableFactory = TableFactory.Instance;
        InitView ();
    }
    //数据模块填充数据
    void InitView () {

        //初始化父表 
        List<GameObject> list = GameObjectTool.GenerateGos (prefab, tableFactory.count, tableMenu);
        GameObjectTool.SetPositionHorizontal (list, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
        for (int i = 0; i < list.Count; i++) {
            GameObject temp = list[i];
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
        BaseTable table01 = tableFactory[index];
        table01.InitData ();
        List<GameObject> childList = GameObjectTool.GenerateGos (prefab, table01.count, tableItem);
        GameObjectTool.SetPositionHorizontal (childList, Vector3.zero, new Vector3 (110, 0, 0), ComponentType.RectTransform);
        for (int i = 0; i < childList.Count; i++) {
            GameObject temp = childList[i];
            temp.GetComponentInChildren<Text> ().text = table01[i].name;
            temp.AddComponent<Index> ().index = i;
            temp.GetComponent<Button> ().onClick.AddListener (() => {
                int selectedIndex = temp.GetComponent<Index> ().index;
                if (selectedIndex == subIndex) return;
                ResetSelected (1);
                this.subIndex = temp.GetComponent<Index> ().index;
                SetSelected (1);
            });

        }
        list[index].Disable<Button> ();
        childList[subIndex].Disable<Button> ();
    }

    void SetSelected (int type) {
        if (type == 0) {
            this.d ("damon", "父对象 选中" + index, false);
            BaseTable table01 = tableFactory[index];
            table01.InitData ();
        } else if (type == 1) {
            this.d ("damon", "子对象对象 选中" + subIndex, false);
        }
    }
    void ResetSelected (int type) {
        if (type == 0) {
            this.d ("damon", "父对象 重置选中" + index, false);
        } else if (type == 1) {
            this.d ("damon", "子对象对象 重置选中" + subIndex, false);
        }
    }

    void Update () { }
}