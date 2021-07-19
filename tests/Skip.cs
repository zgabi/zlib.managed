namespace zlib.managed.Tests
{
    internal static class Skip
    {
        
        public static void If(bool condition)
            => If(condition, "This test was skipped.");

        public static void If(bool condition, string reason)
        {
            if (condition)
            {
                throw new SkipException(reason);
            }
        }

        public static void IfNot(bool condition)
            => IfNot(condition, "This test was skipped.");

        public static void IfNot(bool condition, string reason)
            => If(!condition, reason);
    }
}
