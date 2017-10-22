using UnityEngine;
public class CubeController : RubiksController
{
    public Vector3 mouseReference;
    public Vector3 mouseOffset;
    public float camAngle;
    float mouseDis;
    public float angleUp;
    public float angleFoward;
    public float angleRight;
    GameObject selectedCube;
    private bool isDraggingMouse;
    private int i = 0;
    int lastFace = -1;
    Vector3 mouseDirection;
    int targetCubeLayer;
    string  targetCubeTag;

    private void Update()
    {
        if (app.model.gameState == GameState.Shuffle)
        {
            if(!app.model.cubeShuffled)
            ShuffleCube();

            if (i == 10)
            {
                app.model.cubeShuffled = true;
            }
        }

        if(app.model.gameState == GameState.Play)
        {
            CubeSectionRotatinoControl();
            CheckMouseInput();
        }
    }

    public override void OnNotification(string eventPath, Object target, params object[] data)
    {
        switch (eventPath)
        {
            case "Rotate.CubeSection":
            default: { break; }
        }
    }

    private void CubeSectionRotatinoControl()
    {
        bool clockWise = false;

        if (!CubeView.isRotating)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                app.view.cubeView.RotateSection(Vector3.right, clockWise, false);
            }
            if (Input.GetKey(KeyCode.Keypad2))
            {
                app.view.cubeView.RotateSection(Vector3.up, clockWise, false);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                app.view.cubeView.RotateSection(Vector3.forward, clockWise, false);
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                app.view.cubeView.RotateSection(Vector3.left, clockWise, false);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                app.view.cubeView.RotateSection(Vector3.down, clockWise, false);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                app.view.cubeView.RotateSection(Vector3.back, clockWise, false);
            }
            if (Input.GetKey(KeyCode.Keypad7))
            {
                app.view.cubeView.RotateSection(Vector3.right, clockWise, true);
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                app.view.cubeView.RotateSection(Vector3.up, clockWise, true);
            }
            if (Input.GetKey(KeyCode.Keypad9))
            {
                app.view.cubeView.RotateSection(Vector3.forward, clockWise, true);
            }
        }

        if (isDraggingMouse)
        {
            CheckMouseInput();
        }
    }

    private void ShuffleCube()
    {

        int face = Random.Range(1, 9);
        if (face == lastFace)
        {
            return;
        }
        lastFace = face;
        bool clockWise = true;
        int dir = Random.Range(1, 2);
        if (dir == 1)
        {
            clockWise = false;
        }

        if (!CubeView.isRotating)
        {
            if (face == 1)
            {
                app.view.cubeView.RotateSection(Vector3.right, clockWise, false);
            }
            if (face == 2)
            {
                app.view.cubeView.RotateSection(Vector3.up, clockWise, false);
            }
            if (face == 3)
            {
                app.view.cubeView.RotateSection(Vector3.forward, clockWise, false);
            }
            if (face == 4)
            {
                app.view.cubeView.RotateSection(Vector3.left, clockWise, false);
            }
            if (face == 5)
            {
                app.view.cubeView.RotateSection(Vector3.down, clockWise, false);
            }
            if (face == 6)
            {
                app.view.cubeView.RotateSection(Vector3.back, clockWise, false);
            }
            if (face == 7)
            {
                app.view.cubeView.RotateSection(Vector3.right, clockWise, true);
            }
            if (face == 8)
            {
                app.view.cubeView.RotateSection(Vector3.up, clockWise, true);
            }
            if (face == 9)
            {
                app.view.cubeView.RotateSection(Vector3.forward, clockWise, true);
            }

            i++;
        }
    }

    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.layer >= 10 || hit.collider.gameObject.layer <= 15)
                {
                    selectedCube = hit.collider.gameObject;
                    isDraggingMouse = true;
                    mouseReference = Input.mousePosition;
                    targetCubeTag = hit.collider.gameObject.tag;
                    targetCubeLayer = hit.collider.gameObject.layer;
                    return;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDraggingMouse)
        {
            isDraggingMouse = false;
        }

        if (!CubeView.isRotating && isDraggingMouse)
        {
            CheckMouseDragDirection();
            if (mouseDis > 5)
            {
                if (targetCubeLayer == 10)
                {


                    if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                    {
                        Debug.Log("X >");
                        if (app.model.mouseDirection.x < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.x);
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, false, false);
                            }
                        }
                    }
                    else
                    {
                        if (app.model.mouseDirection.y < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.back, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.y);
                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.back, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, false, false);
                            }
                        }
                    }

                }//front
                if (targetCubeLayer == 11)
                {
                    Debug.Log("Layer Chck = " + targetCubeLayer.ToString() + " = " + LayerMask.LayerToName(targetCubeLayer));

                    if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                    {
                        Debug.Log("X >");
                        if (app.model.mouseDirection.x < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.x);
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, false, false);
                            }
                        }
                    }
                    else
                    {
                        if (app.model.mouseDirection.y < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.left, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.y);
                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.left, false, false);
                            }
                        }
                    }
                }//right
                if (targetCubeLayer == 13)//left)
                {  
                    if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                    {
                        if (app.model.mouseDirection.x < 0)
                        {

                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, true, false);
                            }
                        }
                        else
                        {
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, false, false);
                            }
                        }
                    }
                    else
                    {
                        if (app.model.mouseDirection.y < 0)
                        {

                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.back, true, false);
                            }
                        }
                        else
                        {
                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.forward, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.back, false, false);
                            }
                        }
                    }

                }//left
                if (targetCubeLayer == 15)//back
                {
                    Debug.Log("Layer Chck = " + targetCubeLayer.ToString() + " = " + LayerMask.LayerToName(targetCubeLayer));

                    if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                    {
                        Debug.Log("X >");
                        if (app.model.mouseDirection.x < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.x);
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.up, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.down, false, false);
                            }
                        }
                    }
                    else
                    {
                        if (app.model.mouseDirection.y < 0)
                        {
                            Debug.Log(mouseDirection.x);

                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.left, false, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, true, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, true, false);
                            }
                        }
                        else
                        {
                            Debug.Log(app.model.mouseDirection.y);
                            if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                            {
                                app.view.cubeView.RotateSection(Vector3.left, true, false);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, false, true);
                            }
                            if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                            {
                                app.view.cubeView.RotateSection(Vector3.right, false, false);
                            }
                        }
                    }

                }//front
                if (targetCubeLayer == 12 || targetCubeLayer == 14)// top or bottom
                {
                    Vector3 camPos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                    Vector3 cubePos = app.model.rubiksCube.transform.right;
                    app.model.camAngle.x = Vector3.Angle(camPos, cubePos);
                    camPos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                    cubePos = app.model.rubiksCube.transform.forward;
                    app.model.camAngle.y = Vector3.Angle(camPos, cubePos);


                    if(targetCubeLayer == 12)//top
                    {
                        if (app.model.camAngle.y < 65)//back
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                if (app.model.mouseDirection.x < 0)
                                {
                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                    }

                                }
                            }
                            else
                            {
                                if (app.model.mouseDirection.y < 0)
                                {

                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, false, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, true, false);
                                    }
                                }
                            }
                        }
                        else if (app.model.camAngle.y < 115) // left or right
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                if (app.model.camAngle.x < 90)
                                {
                                    if (app.model.mouseDirection.x < 0)
                                    {
                                        Debug.Log(mouseDirection.x);

                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, true, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, false, false);
                                        }

                                    }

                                }
                                else
                                {
                                    if (app.model.mouseDirection.x < 0)
                                    {
                                        Debug.Log(mouseDirection.x);

                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, false, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, true, false);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (app.model.camAngle.x < 90)
                                {
                                    if (app.model.mouseDirection.y < 0)
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, true, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, false, false);
                                        }

                                    }

                                }
                                else
                                {
                                    if (app.model.mouseDirection.y < 0)
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, false, false);
                                        }
                                    }
                                    else
                                    {
                                        
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, true, false);
                                        }
                                    }
                                }
                            }
                        }
                        else //back
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                if (app.model.mouseDirection.x < 0)
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                    }

                                }
                            }
                            else
                            {
                                if (app.model.mouseDirection.y < 0)
                                {
                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, true, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, false, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (app.model.camAngle.y < 65)//back
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                Debug.Log("X >");
                                if (app.model.mouseDirection.x < 0)
                                {
                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                    }

                                }
                            }
                            else
                            {
                                if (app.model.mouseDirection.y < 0)
                                {
                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, false, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, true, false);
                                    }
                                }
                            }
                        }//back
                        else if (app.model.camAngle.y < 115) // left or right
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                if (app.model.camAngle.x < 90)
                                {
                                    if (app.model.mouseDirection.x < 0)
                                    {
                                        Debug.Log(mouseDirection.x);

                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, false, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, true, false);
                                        }

                                    }
                                }
                                else
                                {
                                    if (app.model.mouseDirection.x < 0)
                                    {
                                        Debug.Log(mouseDirection.x);

                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, true, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.right, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.left, false, false);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (app.model.camAngle.x < 90)
                                {
                                    if (app.model.mouseDirection.y < 0)
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn ==  -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, true, false);
                                        }
                                    }
                                    else
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, false, false);
                                        }

                                    }

                                }
                                else
                                {
                                    if (app.model.mouseDirection.y < 0)
                                    {
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, false, false);
                                        }
                                    }
                                    else
                                    {
                                        
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                        }
                                        if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                        {
                                            app.view.cubeView.RotateSection(Vector3.back, true, false);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(app.model.mouseDirection.x) > Mathf.Abs(app.model.mouseDirection.y))
                            {
                                if (app.model.mouseDirection.x < 0)
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, false, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.back, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().collumn == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.forward, true, false);
                                    }

                                }
                            }
                            else
                            {
                                if (app.model.mouseDirection.y < 0)
                                {
                                    Debug.Log(mouseDirection.x);

                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, false, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, true, false);
                                    }
                                }
                                else
                                {
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, false);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == 0)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.right, true, true);
                                    }
                                    if (selectedCube.GetComponent<Cubeinfo>().row == -1)
                                    {
                                        app.view.cubeView.RotateSection(Vector3.left, false, false);
                                    }
                                }
                            }
                        }
                    }
                    

                }//Top and bottom 
                isDraggingMouse = false;
            }  
        }
    }
    private Vector3 CheckMouseDragDirection()
    {
        mouseOffset = (Input.mousePosition - mouseReference);
        mouseDis = Vector3.Distance(Input.mousePosition, mouseReference);
        app.model.mouseDirection = mouseOffset;
        return app.model.mouseDirection;
    }
}