/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CheckDefineSymbol  {
    static List<string> SetNotActiveSymbol = new List<string> {
        // place define symbol name set not active
        "DEBUG_TEST",
        "DEBUG_DUMP",
        "TENTATIVE",
        "DEBUG_TESTBENCH_DATASET",
        "DEBUG_EVALUATION",
        "DEBUG_TESTBENCH",
        "DEBUG_TESTBENCH2",
    };

    static CheckDefineSymbol()
    {
        StringBuilder sb = new StringBuilder();
        bool DisplayMessage = false ;

        string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        List<string> allDefines = definesString.Split(';').ToList();

        foreach (string DefineSymbol in allDefines) {
            if (SetNotActiveSymbol.Contains(DefineSymbol)) {
                sb.Append("Symbol " + DefineSymbol + " is Defined.\n");
                DisplayMessage = true;
            }
        }

        Debug.Log(sb.ToString());
        if (DisplayMessage) EditorUtility.DisplayDialog("Warning Symbol Define", sb.ToString(), "OK");
    }
}

