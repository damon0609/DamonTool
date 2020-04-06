using System;

[AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class GitHubURLAttribute : Attribute {
    public string url;
    public GitHubURLAttribute (string url) {
        this.url = url;
    }
}