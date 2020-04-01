using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InternalModuleAttribute : Attribute
{
    public HTFrameworkModuleType moduleType
    {
        get; private set;
    }
    public InternalModuleAttribute(HTFrameworkModuleType moduleType)
    {
        this.moduleType = moduleType;
    }
}
