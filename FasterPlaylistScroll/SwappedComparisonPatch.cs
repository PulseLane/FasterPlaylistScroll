using FasterPlaylistScroll.Configuration;
using HarmonyLib;
using HMUI;
using PlaylistManager.HarmonyPatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace FasterPlaylistScroll
{
    [HarmonyPatch(typeof(TableViewScroller_HandleJoystickWasNotCenteredThisFrame), "SwappedComparison")]
    public class SwappedComparisonPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = instructions.ToList();
            for (int i = 0; i < newInstructions.Count; i++)
            {
                var instruction = newInstructions[i];
                if (instruction.opcode == OpCodes.Ldc_R4)
                {
                    newInstructions[i] = new CodeInstruction(OpCodes.Ldc_R4, PluginConfig.Instance.ScrollMultiplier * (float) instruction.operand);
                    break;
                }
            }
            return newInstructions;
        }
    }
}