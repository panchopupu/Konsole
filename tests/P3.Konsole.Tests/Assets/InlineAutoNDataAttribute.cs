using AutoFixture.Xunit2;

namespace P3.Konsole.Tests.Assets
{
    public class InlineAutoNDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoNDataAttribute(params object[] values)
            : base(new AutoNDataAttribute(), values) {
        }
    }
}
