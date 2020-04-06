using System;
using UnityEngine;
[System.Serializable]
public class BaseNPC {
    public int id;
    public string name;
    public string position;
    public string task;
    public string message;
    public string resPath;

    public override string ToString () {
        return string.Format ("id={0},name={1},position={2},task={3},message={4},resPath={5}", id, name, position, task, message, resPath);
    }
}