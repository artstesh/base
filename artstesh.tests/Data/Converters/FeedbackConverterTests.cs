using artstesh.data.Converters;
using artstesh.data.Entities;
using artstesh.data.Models;
using artstesh.tests.FakeFactories;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Converters
{
    public class FeedbackConverterTests
    {
        [Theory]
        [AutoMoqData]
        public void ToModel_Success(Feedback feedback)
        {
            var expected = new FeedbackModel
            {
                Created = feedback.Created, Id = feedback.Id, Email = feedback.Email, Name = feedback.Name,
                Message = feedback.Message
            };
            //
            var model = feedback.ToModel().AsSource().OfLikeness<FeedbackModel>();
            //
            Assert.True(model.Equals(expected));
        }

        [Theory]
        [AutoMoqData]
        public void FromModel_Success(FeedbackModel feedback)
        {
            var expected = new Feedback
            {
                Created = feedback.Created, Id = feedback.Id, Email = feedback.Email, Name = feedback.Name,
                Message = feedback.Message
            };
            //
            var model = feedback.FromModel().AsSource().OfLikeness<Feedback>();
            //
            Assert.True(model.Equals(expected));
        }

        [Fact]
        public void FromModel_Null()
        {
            var result = ((FeedbackModel) null).FromModel();
            Assert.Null(result);
        }

        [Fact]
        public void ToModel_Null()
        {
            var result = ((Feedback) null).ToModel();
            Assert.Null(result);
        }
    }
}