using Damon.Tool;
using HT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HTFrameworkModuleType
{
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
public delegate IEnumerator CoroutineAction();
public delegate IEnumerator CoroutineAction<T>(T arg);
public delegate IEnumerator CoroutineAction<T1,T2>(T1 arg1, T2 arg2);

public sealed partial class Main : MonoBehaviour, ILog
{

    private Dictionary<HTFrameworkModuleType, InternalBaseModule> mModules = new Dictionary<HTFrameworkModuleType, InternalBaseModule>();

    public static AudioManager audioManager;
    public static DateSetManager dateManager;

    public static EventManager eventManager;

    public static EntityManager entityManager;
    public static ObjectPoolManager objectPoolManager;
    public static ReferencePoolManager referencePoolManager;
    public static ResourcesManager resourceManager;
    public static InputManager inputManager;
    public static Coroutiner coroutiner;

    private void InitModule()
    {
        InternalBaseModule[] modules = transform.GetComponentsInChildren<InternalBaseModule>(true);
        for (int i = 0; i < modules.Length; i++)
        {
            InternalBaseModule baseModule = modules[i];
            baseModule.OnInitialization();
            mModules[baseModule.moduleType] = baseModule;
        }
        resourceManager = mModules[HTFrameworkModuleType.Resource] as ResourcesManager;
        audioManager = mModules[HTFrameworkModuleType.Audio] as AudioManager;
        dateManager = mModules[HTFrameworkModuleType.DateSet] as DateSetManager;
        eventManager = mModules[HTFrameworkModuleType.Event] as EventManager;
        entityManager = mModules[HTFrameworkModuleType.Entity] as EntityManager;
        objectPoolManager = mModules[HTFrameworkModuleType.ObjectPool] as ObjectPoolManager;
        referencePoolManager = mModules[HTFrameworkModuleType.ReferencePool] as ReferencePoolManager;
        inputManager = mModules[HTFrameworkModuleType.Input] as InputManager;
        coroutiner = mModules[HTFrameworkModuleType.Coroutiner] as Coroutiner;
    }

    private void PreparatoryModule()
    {
        foreach (InternalBaseModule baseModule in mModules.Values)
        {
            if (baseModule.isPause)
                continue;
            baseModule.OnPreparatory();
        }
    }

    private void ModuleUpdate()
    {
        foreach (InternalBaseModule baseModule in mModules.Values)
        {
            if (baseModule.isPause)
                continue;
            baseModule.OnRefresh();
        }
    }

    private void OnTermination()
    {
        foreach (InternalBaseModule baseModule in mModules.Values)
        {
            if (baseModule.isPause)
                continue;
            baseModule.OnTermination();
        }
    }
}