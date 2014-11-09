using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Solver : MonoBehaviour
{
    //https://code.google.com/p/constraint-thingy/source/browse/trunk/ConstraintThingy/Core/ConstraintThingySolver.cs
    //http://www.cs.northwestern.edu/~ian/GDCConstraintsHowTo.pdf

    private static int CurrentFramePointer;
    private static readonly Stack<PastVariableReference> PastVariableStack = new Stack<PastVariableReference>();
    private int LastSaveFramePointer = -1;
    private List<Variable> Variables;

    // Use this for initialization
    private void Start()
    {
    }

    private void SolveOne()
    {
        if (AllVariablesHaveOneValue(Variables))
        {
            Variables.ForEach(variable => PrintValue(variable.Values));
        }
        else
        {
            var variable = VariableWithMoreThanOneValue(Variables);
            foreach (var possibleValue in variable.Values) //Need to be able to enumerate values
            {
                if (variable.Narrow(possibleValue)) //if no var got narrowed to empty set
                {
                    SolveOne(); //keep going
                }
                else //This value breaks a constarint! Undo!
                {
                    //Undo all updates using the stack
                }
            }
        }

        //If all vars have one val "Sucsess" => Publish result

        //Otherwise
        //Select a var with >1 val
        //Foreach val 
        //Narrow(vars, value)
        //If no var is empty set
        //SolveOne
        //Else undo
    }

    private bool AllVariablesHaveOneValue(List<Variable> variables)
    {
        return variables.TrueForAll(variable => variable.IsUnique());
    }

    private void PrintValue(UInt64 value)
    {
        Debug.Log(value);
    }

    private Variable VariableWithMoreThanOneValue(IEnumerable<Variable> variables)
    {
        return variables.First(variable => !variable.IsUnique());
    }

    private void PushCurrentVariableToStack(Variable variable)
    {
        PastVariableStack.Push(new PastVariableReference
        {
            VariableReference = variable,
            Values = variable.Values
        });

        LastSaveFramePointer = CurrentFramePointer;
    }

    private void RestoreOldValuesFromStack(int framePointer)
    {
        while (PastVariableStack.Count > framePointer)
        {
            PastVariableStack.Pop().VariableReference.Values = PastVariableStack.Pop().Values;
        }
        CurrentFramePointer = framePointer;
    }

    public static int SaveValues()
    {
        return CurrentFramePointer = PastVariableStack.Count;
    }
}