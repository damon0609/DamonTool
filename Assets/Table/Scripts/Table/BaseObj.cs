using System;


public abstract class TableItem : System.Object
{

}

public class NPCItem : TableItem
{
    public int id;
    public string name;
    public string pos;
    public string task;
    public string message;
    public string resPath;

    public override string ToString()
    {
        return string.Format("id = {0},name = {1},pos = {2},task = {3},message = {4},resPath = {5}", id, name, pos, task, message, resPath);
    }
}

public class MonsterItem : TableItem
{

}

public class Item : TableItem
{

}
