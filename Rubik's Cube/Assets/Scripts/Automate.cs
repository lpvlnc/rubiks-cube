using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Automate : MonoBehaviour
{
    public static List<string> MoveList = new();
    private readonly List<string> _allMoves = new()
    {
        "U", "D", "L", "R", "F", "B",       // Clockwise moves
        "U2", "D2", "L2", "R2", "F2", "B2", // Half term moves
        "U'", "D'", "L'", "R'", "F'", "B'"  // Anti clockwise moves
    };
    private CubeState _cubeState;
    private ReadCube _readCube;
    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindObjectOfType<CubeState>();
        _readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveList.Count > 0 && !CubeState.AutoRotating && CubeState.Started)
        {
            // Do the move at the first index
            DoMove(MoveList[0]);

            // Remove the move at the first index
            MoveList.Remove(MoveList[0]);
        }
        
        if (MoveList.Count == 0)
        {
            CubeState.Shuffling = false;
            CubeState.Solving = false;
        }
    }

    public void Shuffle()
    {
        if (!CubeState.AutoRotating) 
        {
            CubeState.Shuffling = true;
            List<string> moves = new();
            int suffleLength = Random.Range(10, 30);
            for (int i = 0; i < suffleLength; i++)
            {
                int randomMove = Random.Range(0, _allMoves.Count);
                moves.Add(_allMoves[randomMove]);
            }
            MoveList = moves;
        }
    }

    void DoMove(string move)
    {
        _readCube.ReadState();
        CubeState.AutoRotating = true;

        if (move.Equals("U"))
            RotateSide(_cubeState.Up, -90);

        if (move.Equals("U'"))
            RotateSide(_cubeState.Up, 90);

        if (move.Equals("U2"))
            RotateSide(_cubeState.Up, -180);

        if (move.Equals("L"))
            RotateSide(_cubeState.Left, -90);

        if (move.Equals("L'"))
            RotateSide(_cubeState.Left, 90);

        if (move.Equals("L2"))
            RotateSide(_cubeState.Left, -180);

        if (move.Equals("D"))
            RotateSide(_cubeState.Down, -90);

        if (move.Equals("D'"))
            RotateSide(_cubeState.Down, 90);

        if (move.Equals("D2"))
            RotateSide(_cubeState.Down, -180);

        if (move.Equals("F"))
            RotateSide(_cubeState.Front, -90);

        if (move.Equals("F'"))
            RotateSide(_cubeState.Front, 90);

        if (move.Equals("F2"))
            RotateSide(_cubeState.Front, -180);

        if (move.Equals("R"))
            RotateSide(_cubeState.Right, -90);

        if (move.Equals("R'"))
            RotateSide(_cubeState.Right, 90);

        if (move.Equals("R2"))
            RotateSide(_cubeState.Right, -180);

        if (move.Equals("B"))
            RotateSide(_cubeState.Back, -90);

        if (move.Equals("B'"))
            RotateSide(_cubeState.Back, 90);

        if (move.Equals("B2"))
            RotateSide(_cubeState.Back, -180);
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        // Automatically rotate the side by the angle
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
