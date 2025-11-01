using System;

namespace Module.Core.Utilities {
    public static class ExtensionString {
        public static string FormatNumber(this int value) {
            if (value >= 100000)
                return FormatNumber(value / 1000) + "K";
            if (value >= 10000) {
                return (value / 1000D).ToString("0.#") + "K";
            }

            return value.ToString("#,0");
        }

        public static string FormatTimer(this float value) {
            return TimeSpan.FromSeconds(value).ToString("mm':'ss");
        }
    }
}