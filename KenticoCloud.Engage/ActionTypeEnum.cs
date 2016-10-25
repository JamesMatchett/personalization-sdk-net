namespace KenticoCloud.Engage
{
    /// <summary>
    /// Defines type of action.
    /// </summary>
    public enum ActionTypeEnum : byte
    {
        /// <summary>
        /// User started new session.
        /// </summary>
        Session = 0,
        /// <summary>
        /// User visited page.
        /// </summary>
        PageVisit = 1,
        /// <summary>
        /// User submitted form.
        /// </summary>
        FormSubmit = 2,
        /// <summary>
        /// User triggered custom action.
        /// </summary>
        CustomAction = 3
    }
}