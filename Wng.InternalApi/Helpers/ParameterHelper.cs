using System;

namespace Wng.InternalApi.Helpers
{
    public abstract class ParameterHelper
    {
        private static ParameterHelper _current;

        static ParameterHelper()
        {
            ParameterHelper._current = new DefaultParameterHelper();
        }

        public static ParameterHelper Current
        {
            get { return ParameterHelper._current; }
            set
            {
                // Not thread safe - only intended for use by testing code

                if (value == null)
                {
                    throw new ArgumentException("ParameterHelper cannot be set to null.");
                }

                ParameterHelper._current = value;
            }
        }

        // Not thread safe - only intended for use by testing code
        public static void ResetToDefault()
        {
            ParameterHelper._current = new DefaultParameterHelper();
        }

        // Connection Strings

        public abstract string CarterClaimDatabaseConnectionString { get; }

        // App Settings

    }

    public class DefaultParameterHelper : ParameterHelper
    {        
        // Connection Strings

        public override string CarterClaimDatabaseConnectionString
        {
            get { return ParameterHelperMethods.GetConnectionString("CarterClaimDatabaseConnectionString"); }
        }

    }
}
