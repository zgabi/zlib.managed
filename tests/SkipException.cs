namespace zlib.managed.Tests
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception to throw to register a skipped test.
    /// </summary>
    [Serializable]
    public class SkipException : Exception
    {
        /// <inheritdoc cref="SkipException(string?, Exception)"/>
        public SkipException()
        {
        }

        /// <inheritdoc cref="SkipException(string?, Exception)"/>
        public SkipException(string? reason)
            : base(reason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipException"/> class.
        /// </summary>
        /// <param name="reason">The reason the test is skipped.</param>
        /// <param name="innerException">The inner exception.</param>
        public SkipException(string? reason, Exception innerException)
            : base(reason, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipException"/> class.
        /// </summary>
        /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
        protected SkipException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
