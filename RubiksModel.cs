using System.Collections.Generic;
using UnityEngine;

public enum GameState {Start, Shuffle, Play, Win, Loose}

public class RubiksModel : RubiksElement {

    public GameState gameState = GameState.Start;
    public GameObject rubiksCube;
    Dictionary<GameObject,Vector3> origonalCubeOrder;
    public GameObject[] cubes;             
    public float startTime;
    public float timer;
    public Vector3 mouseDirection;
    public Vector2 screenMouseDirection;
    public bool cubeShuffled = false;
    public Vector2 camAngle;
    private void Awake()
    {
        rubiksCube = GameObject.FindWithTag("RubiksCube");
    }

    private void Update()
    {

        switch (gameState)
        {
            case GameState.Start:

                break;

            case GameState.Shuffle:
                if (cubeShuffled)
                {
                    gameState = GameState.Play;
                }
                break;

            case GameState.Play:
                startTime = Time.time;

                if (CompareCubePosition())
                {
                    gameState = GameState.Win;
                    Debug.Log(gameState);
                }

                if (timer - Time.time <= 0)
                {
                    gameState = GameState.Loose;
                    Debug.Log(gameState);

                }
                break;

            case GameState.Win:
                Debug.Log(gameState);
                break;

            case GameState.Loose:
                break;

            default:
                break;
        }
    }

    private void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        origonalCubeOrder = GatherCubePositionList();
    }

    private bool  CompareCubePosition()
    {
        foreach(GameObject g in cubes)
        {
            Vector3 tmp;
            if(origonalCubeOrder.TryGetValue(g, out tmp))
            {
                if (tmp != g.transform.position)
                {
                    return false;
                }
            }
        }

        return true;
    }
    /// <summary>
    /// Returns the positions of every cube in the cube array at the start of the game.
    /// </summary>
    /// <returns></returns>
    private Dictionary<GameObject, Vector3> GatherCubePositionList()
    {
        Dictionary<GameObject, Vector3> allCubePositions = new Dictionary<GameObject, Vector3>();
        foreach (GameObject g in cubes)
        {
            allCubePositions.Add(g, g.transform.position);
        }

        return allCubePositions;
    }
}
