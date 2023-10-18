using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerSearch
{
    public static class Bfs
    {
        public static string obfs(string string_0, object[] object_0)
        {
            MinNumberPairwiseScalar minNumberPairwiseScalar = new MinNumberPairwiseScalar();
            minNumberPairwiseScalar.DictionaryNode = 0;
            foreach (object obj in object_0)
            {
                string text = obj as string;
                if (text == null)
                {
                    if (obj is int)
                    {
                        int num = (int)obj;
                        minNumberPairwiseScalar.DictionaryNode += num;
                    }
                    else if (obj is bool)
                    {
                        bool flag = (bool)obj;
                        minNumberPairwiseScalar.DictionaryNode += (flag ? 1 : 0);
                    }
                    else if (obj is char)
                    {
                        char c = (char)obj;
                        minNumberPairwiseScalar.DictionaryNode += (int)c;
                    }
                }
                else
                {
                    minNumberPairwiseScalar.DictionaryNode = text.Aggregate(minNumberPairwiseScalar.DictionaryNode, new Func<int, char, int>(get_DynamicDirectory.DangerousRelease.memid));
                }
            }
            return string_0.Aggregate(string.Empty, new Func<string, char, string>(minNumberPairwiseScalar.ComMemberType));
        }

        public static int[] MemberListType = new int[]
        {
        64
        };

        private sealed class MinNumberPairwiseScalar
        {
            internal MinNumberPairwiseScalar()
            {
            }

            internal string ComMemberType(string string_0, char char_0)
            {
                return string_0 + ((char)((int)char_0 ^ this.DictionaryNode)).ToString();
            }

            internal int DictionaryNode;
        }

        [Serializable]
        private sealed class get_DynamicDirectory
        {
            internal int memid(int int_0, char char_0)
            {
                return int_0 + (int)char_0;
            }

            internal get_DynamicDirectory()
            {
            }

            internal static Func<int, char, int> ReadUInt64;

            internal static readonly get_DynamicDirectory DangerousRelease = new get_DynamicDirectory();
        }

        public static string GetStr(string original, int numberToObf)
        {
            return obfs(original, new object[] { numberToObf });
        }
    }
}
