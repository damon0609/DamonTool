using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
namespace Damon.Tool {
    public class SampleProfile : IDisposable
    {
        public SampleProfile(string name)
        {
            Profiler.BeginSample(name);
        }
        public SampleProfile(string name,UnityEngine.Object obj)
        {
            Profiler.BeginSample(name,obj);
        }
        public void Dispose()
        {
            Profiler.EndSample();
        }
    }

}