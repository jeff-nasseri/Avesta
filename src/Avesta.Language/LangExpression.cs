using Avesta.Global;
using System;
using System.Collections.Generic;

namespace Avesta.Language
{
    public class LangExpression : ObservableValue<string>
    {
        #region Internal Classes
        private class LangObserver : IDisposable, IObserver<ObservableValue<string>>
        {
            #region Fields
            private LangExpression parent;
            #endregion
            #region Constructors
            public LangObserver(LangExpression Parent)
            {
                parent = Parent;
            }
            #endregion
            #region Methods
            public void OnCompleted()
            {
            }
            public void OnError(Exception error)
            {
            }
           
            public void Dispose()
            {
                parent = null;
            }

            public void OnNext(ObservableValue<string> value)
            {
                throw new NotImplementedException();
            }
            #endregion
        }
        #endregion

        #region Fields
        public string Name { get; private set; }

        protected List<object> args = new List<object>();
        private LangObserver observer;
        private List<IDisposable> observeHandles = new List<IDisposable>();
        public List<IStringModifier> Modifiers { get; } = new List<IStringModifier>();
        #endregion

        #region Properties
        public override string Value
        {
            get
            {
                return ToString();
            }
            set
            {
                this.value = value;
            }
        }

        public string OriginalValue
        {
            get
            {
                return value;
            }
            set
            {
                Value = value;
            }
        }

        public object[] FormatArgs
        {
            get
            {
                return args.ToArray();
            }
            set
            {
                lock (args)
                    lock (observeHandles)
                    {
                        foreach (var oh in observeHandles)
                            oh.Dispose();
                        observeHandles.Clear();
                        observer?.Dispose();
                        observer = new LangObserver(this);
                        args.Clear();
                        foreach (var arg in value)
                        {
                            if (arg is LangExpression)
                            {
                                var le = (LangExpression)arg;
                                observeHandles.Add(le.Subscribe(observer));
                            }
                            args.Add(arg);
                        }
                    }
            }
        }
        #endregion

        #region Constructors

        public LangExpression(string name, string value = null, object owner = null, object[] args = null) : base(value, owner)
        {
            Name = name;
            if (args != null)
                FormatArgs = args;
        }

        public LangExpression(string name, IEnumerable<IStringModifier> modifiers)
        {
            Modifiers.AddRange(modifiers);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            var r = args.Count > 0 ? string.Format(value, args.ToArray()) : value;
            foreach (var m in Modifiers)
                r = m.Modify(r);
            return r;
        }

        public string Format(params object[] args)
        {
            return string.Format(ToString(), args);
        }

        public LangExpression DynamicFormat(string name = null, params object[] args)
        {
            return new LangExpression(name, Value, owner, args);
        }

        public LangExpression DependsOn(ObservableValue<string> val)
        {
            val.Changed += (ov, o) => Value = ov.Value;
            return this;
        }
        #endregion
    }
}
