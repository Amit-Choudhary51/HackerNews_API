namespace HackerNews.Service.Models
{
    /// <summary>
    /// Story detail model.
    /// </summary>
    public class NewStoriesModel
    {
        /// <summary>
        /// Story ID.
        /// </summary>
        public int ID { get; set; }       

        /// <summary>
        /// Story Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Story Link.
        /// </summary>
        public string Url { get; set; }
    }
}
