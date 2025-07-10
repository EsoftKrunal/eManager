namespace ShipSoft.CrewManager.BusinessObjects
{
    /// <summary>
    /// Determines the appraisal rating of a crew member.
    /// </summary>
    public enum AppraisalRating
    {
        /// <summary>
        /// Indicates an excellent
        /// </summary>
        Excellent = 1,
        /// <summary>
        /// Indicates a very good
        /// </summary>
        VeryGood = 2,
        /// <summary>
        /// Indicates a satisfactory
        /// </summary>
        Satisfactory = 3,
        /// <summary>
        /// Indicates a poor
        /// </summary>
        Poor = 4,
        /// <summary>
        /// Indicates a very poor
        /// </summary>
        VeryPoor = 5,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        NotSet = 0
    }
}