namespace ShipSoft.CrewManager.BusinessObjects
{
    /// <summary>
    /// Determines the marital status of a crew member.
    /// </summary>
    public enum MaritalStatus
    {
        /// <summary>
        /// Indicates a single
        /// </summary>
        Single = 1,
        /// <summary>
        /// Indicates a married
        /// </summary>
        Married = 2,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        Widowed = 3,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        Separated = 4,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        Diavorced = 5,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        NotSet = 0
    }
}