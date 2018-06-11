using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJson;
using System;

namespace SimpleJson {
    public static class SimpleJsonExtensions {
        public static int GetInt(this JsonObject obj, string key) {
            object result = null;
            if (obj.TryGetValue(key, out result))
                return (int)(long)result;
            return 0;
        }

        public static float GetFloat(this JsonObject obj, string key) {
            object result = null;
            if (obj.TryGetValue(key, out result))
                return Convert.ToSingle(result);
            return 0;
        }

        public static double GetDouble(this JsonObject obj, string key) {
            object result = null;
            if (obj.TryGetValue(key, out result))
                return (double)result;
            return 0;
        }

        public static bool GetBool(this JsonObject obj, string key) {
            object result = null;
            obj.TryGetValue(key, out result);
            return (bool?)result ?? false;
        }

        public static string GetString(this JsonObject obj, string key) {
            object result = null;
            obj.TryGetValue(key, out result);
            return result == null ? string.Empty : (string)result;
        }
    }
}
