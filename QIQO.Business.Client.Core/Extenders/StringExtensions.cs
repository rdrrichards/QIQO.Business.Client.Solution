namespace QIQO.Common.Core
{
    ///<summary>
    /// Extension methods for the <see cref="string"/> class.
    ///</summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Returns the result of calling <seealso cref="string.Format(string,object[])"/> with the supplied arguments.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="CultureInfo.InvariantCulture"/> to format
        /// </remarks>
        /// <param name="formatString">The format string</param>
        /// <param name="args">The values to be formatted</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string formatString, params object[] args)
        {
            return args == null || args.Length == 0 ? formatString : string.Format(formatString, args);
        }

        ///<summary>
        /// Tests the string to see if it is null or "".
        ///</summary>
        ///<returns>True if null or "".</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        ///<summary>
        /// Tests the string to see if it is null, empty or consists only of whitespace.
        ///</summary>
        ///<returns>True if null or "".</returns>
        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}

