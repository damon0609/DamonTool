using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IModule {
    HTFrameworkModuleType moduleType { get; }
    void OnInitialization ();
    void OnPreparatory ();
    void OnRefresh ();
    void OnTermination ();
    void OnPause ();
    void OnResume ();
}