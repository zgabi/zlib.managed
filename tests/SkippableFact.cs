namespace zlib.managed.Tests
{
    using System;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    /// Attribute that is applied to a method to indicate that it is a fact that should
    /// be run by the test runner.
    /// The test may produce a "skipped test" result by calling
    /// <see cref="Skip.If(bool, string)"/> or otherwise throwing a <see cref="SkipException"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [XunitTestCaseDiscoverer("zlib.managed.Tests.Sdk.SkippableFactDiscoverer", "zlib.managed.Tests")]
    internal class SkippableFactAttribute : FactAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkippableFactAttribute"/> class.
        /// </summary>
        /// <param name="skippingExceptions">
        /// Exception types that, if thrown, should cause the test to register as skipped.
        /// </param>
        public SkippableFactAttribute(params Type[] skippingExceptions)
        {
        }
    }
}
