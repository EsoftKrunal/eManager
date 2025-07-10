namespace ShipSoft.CrewManager.BusinessObjects
{
    /// <summary>
    /// Determines the type of remittance.
    /// </summary>
    public enum TypeOfRemittance
    {
        /// <summary>
        /// Indicates a cheque
        /// </summary>
        Cheque = 1,
        /// <summary>
        /// Indicates a bank draft
        /// </summary>
        BankDraft = 2,
        /// <summary>
        /// Indicates a payorder
        /// </summary>
        Payorder = 3,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        NotSet = 0
    }
}