using artstesh.data.Entities;
using artstesh.data.Models;

namespace artstesh.data.Converters
{
    public static class FeedbackConverter
    {
        public static FeedbackModel ToModel(this Feedback feedback)
        {
            if (feedback == null) return null;
            return new FeedbackModel
            {
                Created = feedback.Created, Id = feedback.Id, Email = feedback.Email, Name = feedback.Name,
                Message = feedback.Message
            };
        }

        public static Feedback FromModel(this FeedbackModel feedback)
        {
            if (feedback == null) return null;
            return new Feedback
            {
                Created = feedback.Created, Id = feedback.Id, Email = feedback.Email, Name = feedback.Name,
                Message = feedback.Message
            };
        }
    }
}