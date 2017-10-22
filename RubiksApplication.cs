using UnityEngine;
public class RubiksElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public RubiksApplication app { get { return GameObject.FindObjectOfType<RubiksApplication>(); } }
}

// Entry point for the application
public class RubiksApplication : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public RubiksModel model;
    public RubiksView view;
    public RubiksController controller;

    void Awake()
    {
        Init();
    }

    // Assigning all referances.
    private void Init()
    {
        model = transform.Find("Model").gameObject.GetComponent<RubiksModel>();
        view = transform.Find("View").gameObject.GetComponent<RubiksView>();
        controller = transform.Find("Controller").gameObject.GetComponent<RubiksController>();
    }

    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        RubiksController[] controller_list = GetAllControllers();
        foreach (RubiksController c in controller_list)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }

    private RubiksController[] GetAllControllers()
    {
        return GameObject.FindObjectsOfType<RubiksController>();
    }
}
/// <summary>
/// The three non-face cube groups.
/// </summary>
public enum CenterRow { Standing, Mid, Equator }

