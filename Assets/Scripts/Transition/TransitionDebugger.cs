using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransitionDebugger
{
    [MenuItem("传送系统/Hall->HallOutside")]
    static void Trisition1()
    {
        TransitionManager.instance.Transition("Hall","HallOutside");
    }
    
    [MenuItem("传送系统/HallOutside->Hall")]
    static void Trisition2()
    {
        TransitionManager.instance.Transition("HallOutside","Hall");
    }
}
