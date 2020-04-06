using System;

public class TableItem : System.Object {
    public int id;
    public string name;

    public int account;

    public float price;
    public string data;

    public override string ToString () {
        return string.Format ("id={0};name={1},account={2},price={3},data={4}", id, name, account, price, data);
    }
}