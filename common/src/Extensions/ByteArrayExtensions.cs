using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common.Extensions
{
    /// <summary>
    /// Extension methods to byte arrays
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Looks for the next occurrence of a sequence in this byte array
        /// </summary>
        /// <param name="array">Array that will be scanned</param>
        /// <param name="start">Index in the array at which scanning will begin</param>
        /// <param name="sequence">Sequence the array will be scanned for</param>
        /// <returns>
        /// The index of the next occurrence of the sequence of -1 if not found
        /// </returns>
        /// <remarks>
        /// Written by Cygon (https://stackoverflow.com/questions/25400610/most-efficient-way-to-find-pattern-in-byte-array)
        /// </remarks>
        public static int IndexOf(this byte[] array, int start, byte[] sequence)
        {
            int end = array.Length - sequence.Length; // No match is possible past this point
            byte firstByte = sequence[0];             // Cached to tell compiler there's no aliasing

            while (start < end)
            {
                // Scan for first byte only (Compiler-friendly)
                if (array[start] == firstByte)
                {
                    // Scan for rest of sequence
                    for (int offset = 1; offset < sequence.Length; ++offset)
                    {
                        if (array[start + offset] != sequence[offset])
                        {
                            // Mismatch? continue scanning with next byte
                            break;
                        }
                        else if (offset == sequence.Length - 1)
                        {
                            // All bytes matched!
                            return start;
                        }
                    }
                }

                ++start;
            }

            // No match (end of array)
            return -1;
        }
    }
}
