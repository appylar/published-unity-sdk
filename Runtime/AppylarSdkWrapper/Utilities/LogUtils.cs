using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.AppylarSdkWrapper.Utilities {

  internal static class LogUtils {
    private static bool debug = true;

    public static void DebugLog(string value) {
      if (debug) {
        Debug.Log(value);
      }
    }

  }

}