﻿namespace zlib.managed.Tests.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    /// Transforms <see cref="SkippableFactAttribute"/> test methods into test cases.
    /// </summary>
    internal class SkippableFactDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>
        /// The diagnostic message sink provided to the constructor.
        /// </summary>
        private readonly IMessageSink diagnosticMessageSink;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkippableFactDiscoverer"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages.</param>
        public SkippableFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        /// <summary>
        /// Translates the types of exceptions that should be considered as "skip" exceptions into their full names.
        /// </summary>
        /// <param name="factAttribute">The <see cref="SkippableFactAttribute"/>.</param>
        /// <returns>An array of full names of types.</returns>
        public static string[] GetSkippableExceptionNames(IAttributeInfo factAttribute)
        {
            Requires.NotNull(factAttribute, nameof(factAttribute));
            var firstArgument = (object[]?)factAttribute.GetConstructorArguments().FirstOrDefault();
            var skippingExceptions = firstArgument?.Cast<Type>().ToArray() ?? Type.EmptyTypes;
            Array.Resize(ref skippingExceptions, skippingExceptions.Length + 1);
            skippingExceptions[skippingExceptions.Length - 1] = typeof(SkipException);

            var skippingExceptionNames = skippingExceptions.Select(ex => ex.FullName).ToArray();
            return skippingExceptionNames!;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            Requires.NotNull(factAttribute, nameof(factAttribute));
            string[] skippingExceptionNames = GetSkippableExceptionNames(factAttribute);
            yield return new SkippableFactTestCase(skippingExceptionNames, this.diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);
        }
    }
}
