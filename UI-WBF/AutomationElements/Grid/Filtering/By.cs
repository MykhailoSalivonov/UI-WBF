namespace UI_WBF.AutomationElements.Grid.Filtering
{
    public class By : IDisposable
    {
        #region Constructors

        private By() 
        {
            Filters = new List<Filter>();
        }

        #endregion

        #region Properties

        internal List<Filter> Filters { get; }

        private static By Instance
        {
            get { return _instance ??= new By(); }
        }

        #endregion

        #region Methods

        public By And(By by)
        {
            return Instance;
        }

        public By Not(By by)
        {
            Instance.Filters.Last()
                .ConvertToOppositeType();

            return Instance;
        }

        public void Dispose()
        {
            Filters.Clear();
            _instance = null;
        }

        #endregion

        #region Static Methods

        public static By EmptyValue(string column)
        {
            Instance.Filters.Add(new Filter(Type.Equals, column, string.Empty));
            return Instance;
        }

        public static By EqualsValue(string column, string value)
        {
            Instance.Filters.Add(new Filter(Type.Equals, column, value));
            return Instance;
        }

        public static By ContainsValue(string column, string value)
        {
            Instance.Filters.Add(new Filter(Type.Contains, column, value));
            return Instance;
        }

        #endregion

        #region Private Fields

        private static By _instance;

        #endregion
    }
}
