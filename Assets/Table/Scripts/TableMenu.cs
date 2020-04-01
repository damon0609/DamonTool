using System.Collections;
using System.Collections.Generic;
using Damon.Table;
using UnityEngine;
using UnityEngine.UI;
public interface IDataSet { }

public class TableMenu : MonoBehaviour {
    public GameObject prefab;
    private List<GameObject> list = new List<GameObject> ();
    private TableFactory tableFactory;
    private int index = 0;
    void Start () {
        tableFactory = TableFactory.Instance;
        InitView (tableFactory);
    }
    //数据模块填充数据
    void InitView (IDataSet dataSet) {
        TableFactory mDataSet = dataSet as TableFactory;
        for (int i = 0; i < mDataSet.count; i++) {
            GameObject go = GameObject.Instantiate<GameObject> (prefab);
            go.transform.SetParent (this.transform);

            BaseTable table = mDataSet[i];
            go.GetComponentInChildren<Text>().text = table.name;
            go.AddComponent<Index>().index = i;

            go.GetComponent<Button>().onClick.AddListener(()=>{
                int mIndex = go.GetComponent<Index>().index;
                TableFactory.dispatcher.Trigger("MenuSelected",mIndex);
            });
            list.Add(go);
        }
        for (int i = 0; i < list.Count; i++) {
            GameObject go = list[i];
            go.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (110 * i + 10, 0, 0);
        }
        mDataSet[index].InitData();
    }
    void Update () {
    }
}