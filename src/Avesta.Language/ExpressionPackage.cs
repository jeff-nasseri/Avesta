using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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
