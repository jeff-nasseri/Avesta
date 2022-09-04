namespace Avesta.Language
{
    public class StringCaseModifier : IStringModifier
    {
        #region Fields

        private bool upper;
        #endregion

        #region Constructors

        public StringCaseModifier(bool upper = true)
        {
            this.upper = upper;
        }
        #endregion

        #region Methods
        public string Modify(string s)
        {
            return upper ? s?.ToUpperInvariant() : s?.ToLowerInvariant();
        }
        #endregion
    }
}
