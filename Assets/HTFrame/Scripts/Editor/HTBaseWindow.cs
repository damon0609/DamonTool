using UnityEditor;

public class HTBaseWindow : EditorWindow
{

    protected bool mActiveTitle = false;
    private void OnEnable()
    {
        Initialize();
    }
    private void OnGUI()
    {
        if (mActiveTitle)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            OnGUIWindowTitle();
            EditorGUILayout.EndHorizontal();
        }
        OnBodyGUI();
    }
    protected virtual void OnGUIWindowTitle()
    {

    }
    protected virtual void Initialize()
    {
        
    }
    protected virtual void OnBodyGUI()
    {

    }
}
