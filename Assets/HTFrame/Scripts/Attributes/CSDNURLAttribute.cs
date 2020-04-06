using System;
[AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CSDNURLAttribute : Attribute {
    public string url;
    public string path;
    public CSDNURLAttribute (string url, string path) {
        this.url = url;
        this.path = path;
    }
}