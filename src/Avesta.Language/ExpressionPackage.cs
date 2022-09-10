using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Avesta.Language
{

    public class ExpressionPackage : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        [IndexerName("Item")]
        public string this[string exp] => Lang.T(exp);
        #endregion

        #region Constructor
        public ExpressionPackage()
        {
            Update();
            Lang.Changed += Update;
        }
        #endregion

        #region Internal Methods
        private void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
        }
        #endregion
    }
}
