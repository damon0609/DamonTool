using System.Collections;
using System.Collections.Generic;
using Damon.Table;
using UnityEngine;
using UnityEngine.UI;

public class TableMenu : MonoBehaviour
{
    public GameObject prefab;
    private List<GameObject> list = new List<GameObject>();
    private TableFactory tableFactory;
    void Start()
    {
        TableFactory.dispatcher.Register("InitTableView", (System.Object obj) =>
        {
            Dictionary<TableFactory.TableType, BaseTable> dic = (Dictionary<TableFactory.TableType, BaseTable>)obj;
            InitView(dic);
        });
        tableFactory = TableFactory.Instance;
    }

    void InitView(Dictionary<TableFactory.TableType, BaseTable> dic)
    {
        foreach (BaseTable t in dic.Values)
        {
            GameObject go = Instantiate<GameObject>(prefab);
            go.transform.parent = transform;
            go.transform.localScale = Vector3.one;
            list.Add(go);
            go.GetComponentInChildren<Text>().text = t.name;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                tableFactory.ChangeTable(t);
                Debug.Log(t.name);
            });
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = list[i];
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(110 * i + 10, 0, 0);
        }
    }

    void Update()
    {

    }
}
