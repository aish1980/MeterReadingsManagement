using MeterReadingsManagement.Common;
using Moq;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace MeterReadingsXUnitTest
{
    public class MeterReadingUnitTest
    {
        [Fact]
        public void Validate_Reading_Value_Return_True()
        {
            var dependency = Mock.Of<IValidation>();

            var actual = dependency.ValidateReadingValue(1111);

            Assert.True(actual);
        }
    }
}
