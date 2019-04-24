using AutoFixture.Xunit2;

namespace artstesh.tests.FakeFactories
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(FakeFactory.Fixture)
        {
        }
    }
}