using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class SolveTwoPhase : MonoBehaviour
{
    private CubeState _cubeState;
    private ReadCube _readCube;
    private bool _firstSolve = true;

    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindObjectOfType<CubeState>();
        _readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.Started && _firstSolve)
        {
            _firstSolve = false;
            Solver();
        }
    }


    public void Solver()
    {
        if (!CubeState.AutoRotating)
        {
            CubeState.Solving = true;
            _readCube.ReadState();

            // Get the state of the cube as a string
            string moveString = _cubeState.GetStateString();

            // Solve the cube
            string info = "";

            // First time build the tables
            //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

            // Every other time we will use the method the reads from the pre generated tables
            string solution = Search.solution(moveString, out info);

            // Convert the solved moves from a string to a list
            List<string> solutionList = StringToList(solution);

            // Automate the list
            Automate.MoveList = solutionList;
        }
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
