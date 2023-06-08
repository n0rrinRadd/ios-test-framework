using System;
using NUnit.Framework;

namespace Helpers
{
    /// <summary>
    /// RetryDynamicAttribute may be applied to test case in order
    /// to run it multiple times based on app setting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RetryDynamicAttribute : RetryAttribute
        {
            static Lazy<int> numberOfRetries = new Lazy<int>(() => {

                #if DEBUG
                    return 1;
                #else
                    return 1;  // Disabled retry value. Use AppVeyor retry mechanic instead.
                #endif
                //int count = 0;
                //return int.TryParse(ConfigurationManager.AppSettings["retryTest"], out count) ? count : DEFAULT_TRIES;
            });

            public RetryDynamicAttribute() :
                base(numberOfRetries.Value)
            {
            }

    }
}
