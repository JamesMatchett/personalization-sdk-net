namespace KenticoCloud.Engage
{
    /// <summary>
    /// Represents the details about user's action.
    /// </summary>
    public class ActionDetails
    {
        /// <summary>
        /// Specifies how many days ago action occured.
        /// </summary>
        public int? DaysAgo { get; set; }

        /// <summary>
        /// Specifies on which url action occured.
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// Specifies the action name.
        /// </summary>
        public string CustomActionName { get; set; }
    }
}