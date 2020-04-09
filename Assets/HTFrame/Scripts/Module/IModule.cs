using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damon.Tool;

public interface IModule:ILog {
    HTFrameworkModuleType moduleType { get; }
    void OnInitialization ();
    void OnPreparatory ();
    void OnRefresh ();
    void OnTermination ();
    void OnPause ();
    void OnResume ();
}