using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CubeView : RubiksView {

    //CubeRotation
    private GameObject[] sectionGroup;       // Container for the cubes belonging to the face to rotate
    public GameObject sectionPivot;          // Pivot objects
    private CubeSection[] sections;          // Reference to the objects to be rotated 
    private int i = 0;                       // Counter used for assigning cubes to the face group
    private float rotationAngle;
    public static bool isRotating = true;
    private Vector3 rotationDirection;
    private Vector3 pivotPos;
    private float timer;

    private void Awake()
    {
        sectionPivot = GameObject.FindWithTag("Pivot");
    }

    void Start()
    {
        i = 0;
        
        sectionGroup = new GameObject[9];
        sectionPivot.transform.parent = sectionPivot.transform;
    }

    private void Update()
    {
        if (isRotating)
        {
            CubeSelectionRotation(rotationDirection);
        }

    }

    
    public void RotateSection(Vector3 dir, bool clockWise, bool center)
    {
        Vector3 findalDir = dir;
        timer = Time.time;
        i = 0;
        sectionPivot.transform.rotation = Quaternion.identity;

        if (center)
        {
            GatherCenterSectionArray(dir);
        }
        else
        {
            GatherFaceSectionArray(dir);
        }
        if(clockWise)
        {
            findalDir *= -1;
        }
        rotationDirection = findalDir;
        isRotating = true;
    }

    /// <summary>
    /// Parents all the cubes int the cube section determined by dir.
    /// </summary>
    /// <param name="dir"></param>
    private void GatherCenterSectionArray(Vector3 dir)
    {
        foreach (GameObject g in app.model.cubes)
        {
            if(dir == Vector3.right)
            {
                if (g.transform.position.x == 0)
                {
                    g.transform.parent = sectionPivot.transform;
                    i++;
                }
            }

            if (dir == Vector3.up)
            {
                if (g.transform.position.y == 0)
                {
                    g.transform.parent = sectionPivot.transform;
                    i++;
                }
            }

            if (dir == Vector3.forward)
            {
                if (g.transform.position.z == 0)
                {
                    g.transform.parent = sectionPivot.transform;
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// Parents all the cubes int the cube section determined by dir.
    /// </summary>
    /// <param name="dir"></param>
    private void GatherFaceSectionArray(Vector3 dir)
    {
        Vector3 dirMask = new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z));
        foreach (GameObject g in app.model.cubes)
        {

            Vector3 maskedPosition = Vector3.Scale(dirMask,g.transform.position);

            if (dir == maskedPosition)
            {
                g.transform.SetParent(sectionPivot.transform);

                i++;
            }        
        }
        if (i < 9)
        {
            foreach (GameObject g in app.model.cubes)
            {
                g.transform.SetParent(app.model.rubiksCube.transform);
                g.transform.position = new Vector3(Mathf.Round(g.transform.position.x), Mathf.Round(g.transform.position.y), Mathf.Round(g.transform.position.z));
            }
            isRotating = false;
        }
    }

    /// <summary>
    /// The actual rotation of the face
    /// </summary>
    /// <param name="dir"></param>
    void CubeSelectionRotation( Vector3 dir)
    {
        float t = Time.time - timer;
        sectionPivot.transform.rotation = Quaternion.Slerp( Quaternion.identity, Quaternion.Euler(dir * 90), t * 3);
        isRotating = true;

        if (Quaternion.Angle(sectionPivot.transform.rotation, Quaternion.Euler(dir * 90)) < 1f )
        {
            sectionPivot.transform.rotation = Quaternion.Euler(dir * 90);
            foreach(GameObject g in app.model.cubes)
            {
                g.transform.SetParent(app.model.rubiksCube.transform);
                g.transform.position = new Vector3(Mathf.Round(g.transform.position.x), Mathf.Round(g.transform.position.y), Mathf.Round(g.transform.position.z));
            }
            isRotating = false;
        }
    }
}

//TODO Remove this if it's not nessisary
// Dont think we will be needing this one here;
//  |
//  V
/// <summary>
/// Used to select a ever changing cube groups.
/// </summary>
public struct CubeSection
{
    public CubeSection(Vector3 _direction, string _name)
    {
        this.positionMask = this.direction = _direction;
        name = _name;
    }
    public CubeSection(CenterRow row, string _name)
    {
        switch (row)
        {
            case CenterRow.Standing:
                direction = Vector3.forward;
                positionMask = new Vector3(1,1,0);
                break;
            case CenterRow.Mid:
                direction =  Vector3.right;
                positionMask = new Vector3(0,1,1);
                break;
            case CenterRow.Equator:
                direction = Vector3.up;
                positionMask = new Vector3(1,0,1);

                break;
            default:
                positionMask = direction = Vector3.zero;
                break;
        }
        name =_name;
    }
    public string name;
    public Vector3 direction;
    public Vector3 positionMask;
   

}
