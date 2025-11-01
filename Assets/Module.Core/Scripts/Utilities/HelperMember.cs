using System;
using System.Linq.Expressions;

namespace Module.Core.Utilities {
    public partial class Helper {
        public static class Member {
            public static string GetName<T>(Expression<Func<T>> memberAccess) {
                if (memberAccess.Body is MemberExpression memberExpression) {
                    return memberExpression.Member.Name;
                }

                throw new InvalidOperationException("Member expression expected");
            }

            public static string GetName(Action method) {
                return method.Method.Name;
            }
        }
    }
}