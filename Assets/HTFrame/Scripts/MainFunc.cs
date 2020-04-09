using System;
using System.Collections;
using System.Collections.Generic;
using HT;
using UnityEngine;
public enum HTFrameworkModuleType {
    AspectTrack,
    Audio,
    Controller,
    Coroutiner,
    CustomModule,
    DateSet,
    Debug,
    Entity,
    Event,
    ExceptionHandler,
    FSM,
    HotFix,
    Input,
    Main,
    NetWork,
    ObjectPool,
    Procedure,
    ReferencePool,
    Resource,
    StepEditor,
    UI,
    Utility,
    WebRequest,

}

public sealed partial class Main : MonoBehaviour {

    private Dictionary<HTFrameworkModuleType, InternalBaseModule> mModules = new Dictionary<HTFrameworkModuleType, InternalBaseModule> ();

    public static AudioManager audioManager;
    public static DateSetManager dateManager;

    public static EventManager eventManager;

    public static EntityManager entityManager;
    public static ObjectPoolManager objectPoolManager;
    public static ReferencePoolManager referencePoolManager;

    private void InitModule () {

        InternalBaseModule[] modules = transform.GetComponentsInChildren<InternalBaseModule> (true);
        for (int i = 0; i < modules.Length; i++) {
            InternalBaseModule baseModule = modules[i];
            baseModule.OnInitialization ();
            mModules[baseModule.moduleType] = baseModule;
        }
        audioManager = mModules[HTFrameworkModuleType.Audio] as AudioManager;
        dateManager = mModules[HTFrameworkModuleType.DateSet] as DateSetManager;
        eventManager = mModules[HTFrameworkModuleType.Event] as EventManager;
        entityManager = mModules[HTFrameworkModuleType.Entity] as EntityManager;
        objectPoolManager = mModules[HTFrameworkModuleType.ObjectPool] as ObjectPoolManager;
        referencePoolManager = mModules[HTFrameworkModuleType.ReferencePool] as ReferencePoolManager;

    }

    private void PreparatoryModule () {
        foreach (InternalBaseModule baseModule in mModules.Values) {
            if (baseModule.isPause)
                continue;
            baseModule.OnPreparatory ();
        }
    }

    private void ModuleUpdate () {
        foreach (InternalBaseModule baseModule in mModules.Values) {
            if (baseModule.isPause)
                continue;
            baseModule.OnRefresh ();
        }
    }

    private void OnTermination()
    {
         foreach (InternalBaseModule baseModule in mModules.Values) {
            if (baseModule.isPause)
                continue;
            baseModule.OnTermination();
        }
    }
}