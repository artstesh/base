using artstesh.data.Converters;
using artstesh.data.Entities;
using artstesh.data.Helpers;
using artstesh.data.Models;
using artstesh.tests.FakeFactories;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Converters
{
    public class SubscribeConverterTests
    {
        [Theory, AutoMoqData]
        public void ToModel_Success(Subscribe subscribe)
        {
            var expected = new SubscribeModel
            {
                BeginDate = subscribe.BeginDate, Id = subscribe.Id, Email = subscribe.Email, Name = subscribe.Name, IsActive = subscribe.IsActive, Secret = subscribe.Secret
            };
            //
            var model = subscribe.ToModel().AsSource().OfLikeness<SubscribeModel>();
            //
            Assert.True(model.Equals(expected));
        }
        
        [Fact]
        public void ToModel_Null()
        {
            var result = ((Subscribe) null).ToModel();
            Assert.Null(result);
        }
        
        [Theory, AutoMoqData]
        public void FromModel_Success(SubscribeModel subscribe)
        {
            var expected = new Subscribe
            {
                BeginDate = subscribe.BeginDate, Id = subscribe.Id, Email = subscribe.Email, Name = subscribe.Name, IsActive = subscribe.IsActive, Secret = subscribe.Secret
            };
            //
            var model = subscribe.FromModel().AsSource().OfLikeness<Subscribe>();
            //
            Assert.True(model.Equals(expected));
        }
        
        [Fact]
        public void FromModel_Null()
        {
            var result = ((SubscribeModel) null).FromModel();
            Assert.Null(result);
        }
    }
}