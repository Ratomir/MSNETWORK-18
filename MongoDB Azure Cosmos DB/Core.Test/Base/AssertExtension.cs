using Xunit;

namespace Core.Test.Base
{
    public class AssertExtension : Assert
    {
        public static void Fail()
        {
            True(false);
        }

        public static void Success()
        {
            True(true);
        }
    }
}
