using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RZAccountManagerV8.Core.Utils {
    public static class CollectionUtils {
        public static void AddAll(this IList destination, IEnumerable itemsToAdd) {
            foreach (object x in itemsToAdd) {
                destination.Add(x);
            }
        }

        public static void AddAll<T>(this IList<T> destination, IEnumerable<T> itemsToAdd) {
            foreach (T t in itemsToAdd) {
                destination.Add(t);
            }
        }

        public static string JoinToString<T>(this IEnumerable<T> list, string delimiter, int limit = int.MaxValue) {
            return JoinToString(list, delimiter, x => x.ToString(), limit);
        }

        public static string JoinToString<T>(this IEnumerable<T> list, string delimiter, Func<T, string> toStringFunc, int limit = int.MaxValue) {
            StringBuilder sb = new StringBuilder(128);
            using (IEnumerator<T> enumerator = list.GetEnumerator()) {
                if (--limit >= 0 && enumerator.MoveNext()) {
                    sb.Append(toStringFunc(enumerator.Current));
                }

                while (--limit >= 0 && enumerator.MoveNext()) {
                    sb.Append(delimiter).Append(toStringFunc(enumerator.Current));
                }
            }

            return sb.ToString();
        }

        public static string JoinToStringReadable<T>(this IList<T> list, string delimiter, Func<T, string> toStringFunc) {
            switch (list.Count) {
                case 0: return "";
                case 1: return toStringFunc(list[0]);
                case 2: return toStringFunc(list[0]) + delimiter + toStringFunc(list[1]);
                default: return toStringFunc(list[0]) + delimiter + toStringFunc(list[1]) + "... etc";
            }
        }
    }
}