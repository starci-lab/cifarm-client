using System.Collections.Generic;
using UnityEngine;

namespace CiFarm.Utils
{
    public enum LogColor
    {
        [EnumStringValue("#4CAF50")] // Soft green for Success
        Success,

        [EnumStringValue("#FFEB3B")] // Soft golden yellow for Warning
        Warning,

        [EnumStringValue("#F44336")] // Balanced red for Error
        Error,

        [EnumStringValue("#00BCD4")] // Soft cyan for Verbose
        Verbose,

        [EnumStringValue("#9C27B0")] // Soft purple for Debug
        Debug,

        [EnumStringValue("#FFFFFF")] // White for Fatal (use with dark background)
        Fatal,
    }

    public enum LogScope
    {
        All, // All log types (every platform)
        None, // No logging
        WebGL, // Logging specifically for WebGL
        Editor // Logging specifically for Editor (in the Unity editor)
        ,
    }

    public static class ConsoleLogger
    {
        // Set the log scope (this would typically be set at runtime, e.g., through a config file or debug settings)
        public static LogScope LogScope { get; set; } = LogScope.All;

        private static Dictionary<LogColor, string> _logColorMap = new()
        {
            { LogColor.Success, "SUCCESS" },
            { LogColor.Warning, "WARNING" },
            { LogColor.Error, "ERROR" },
            { LogColor.Verbose, "VERBOSE" },
            { LogColor.Debug, "DEBUG" },
            { LogColor.Fatal, "FATAL" },
        };

        // Main method to log messages with color
        private static void LogMessage(string message, LogColor logColor)
        {
            // If logging scope is None, do nothing
            if (LogScope == LogScope.None)
                return;

            // If scope is WebGL or Editor, only log when appropriate
            if (LogScope == LogScope.WebGL && !IsWebGL())
                return;

            if (LogScope == LogScope.Editor && !IsEditor())
                return;

            // Log the message if scope allows
            Debug.Log(
                $"<b><color={logColor.GetStringValue()}>{_logColorMap[logColor]}:</color></b> {message}"
            );
        }

        // Helper method to check if we are in WebGL build
        private static bool IsWebGL()
        {
#if UNITY_WEBGL
            return true; // Only log on WebGL
#else
            return false;
#endif
        }

        // Helper method to check if we are in Unity Editor
        private static bool IsEditor()
        {
#if UNITY_EDITOR
            return true; // Only log in the Unity editor
#else
            return false;
#endif
        }

        public static void LogSuccess(string message)
        {
            LogMessage(message, LogColor.Success);
        }

        public static void LogWarning(string message)
        {
            LogMessage(message, LogColor.Warning);
        }

        public static void LogError(string message)
        {
            LogMessage(message, LogColor.Error);
        }

        public static void LogVerbose(string message)
        {
            LogMessage(message, LogColor.Verbose);
        }

        public static void LogDebug(string message)
        {
            LogMessage(message, LogColor.Debug);
        }

        public static void LogFatal(string message)
        {
            LogMessage(message, LogColor.Fatal);
        }
    }
}
