using System;
using System.Linq.Expressions;
using UnityEngine.Assertions;

namespace Module.Core.Utilities {
    public partial class Helper {
        public static class Math {
            public static float GetPercent01FromValueOfRange(float value, float min, float max) {
                Assert.IsFalse(max - min == 0);
                // (current - min) / (max - min)
                return (value - min) / (max - min);
            }

            public static float GetValueFromPercent01OfRange(float percent01, float min, float max) {
                // percent * (max - min) + min
                return percent01 * (max - min) + min;
            }
        }
    }
}