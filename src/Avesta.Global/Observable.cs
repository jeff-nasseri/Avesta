using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Global
{
    public interface IStorableValue
    {
        void CopyFromStorable(IStorableValue value);

        object SerializeStorable();

        void DeserializeStorable(object value);
    }

    public interface IValueContainer
    {

        object ContainedValue { get; }
    }


    public delegate void ObservableValueChange<TValue>(TValue ov, object owner);

    public class ObservableSubscription<T> : IDisposable
    {
        #region Fields
        private ObservableValue<T> observable;
        private IObserver<ObservableValue<T>> observer;
        #endregion

        #region Constructors
        public ObservableSubscription(ObservableValue<T> observable, IObserver<ObservableValue<T>> observer)
        {
            this.observable = observable;
            this.observer = observer;
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            lock (this)
            {
                observable?.Unsubscribe(observer);
                observable = null;
                observer = null;
            }
        }
        #endregion
    }

    public class ObservableValue<TValue> : IObservable<ObservableValue<TValue>>, IStorableValue, IComparable, IValueContainer
    {
        #region Fields
        protected object owner;
        protected TValue value;
        private HashSet<IObserver<ObservableValue<TValue>>> observers = new HashSet<IObserver<ObservableValue<TValue>>>();
        private int LockCount = 0;
        #endregion

        #region Events
        public event ObservableValueChange<ObservableValue<TValue>> Changed;
        #endregion

        #region Constructors
        public ObservableValue(TValue value = default(TValue), object owner = null)
        {
            this.owner = owner;
            this.value = value;
        }

        public ObservableValue()
        {
        }
        #endregion

        #region Properties
        public virtual TValue Value
        {
            get { return value; }
            set
            {
                if (this.value?.Equals(value) == true)
                    return;
                this.value = value;
            }
        }

        public object ContainedValue => Value;

        #endregion

        #region Internal Methods
        private void InvokeChangedForced()
        {
            lock (this)
                if (LockCount > 0)
                    return;
            Changed?.Invoke(this, owner);
            lock (observers)
                foreach (var observer in observers)
                    observer.OnNext(this);
        }


        #endregion

        #region Methods



        public void Subscribe(ObservableValueChange<ObservableValue<TValue>> cb)
        {
            Changed += cb;
        }

        public void Unsubscribe(ObservableValueChange<ObservableValue<TValue>> cb)
        {
            Changed -= cb;
        }

        public IDisposable Subscribe(IObserver<ObservableValue<TValue>> observer)
        {
            lock (observers)
                observers.Add(observer);
            return new ObservableSubscription<TValue>(this, observer);
        }

        public void Unsubscribe(IObserver<ObservableValue<TValue>> observer)
        {
            lock (observers)
                observers.Remove(observer);
        }



        public void LockObservable()
        {
            lock (this)
                LockCount++;
        }



        public override string ToString()
        {
            return Value?.ToString() ?? "<NULL>";
        }
        #endregion

        #region Operators
        public static implicit operator TValue(ObservableValue<TValue> v)
        {
            return v.Value;
        }

        public static implicit operator ObservableValue<TValue>(TValue v)
        {
            return new ObservableValue<TValue>(v);
        }
        #endregion

        #region IStorableValue Implementation
        public virtual void CopyFromStorable(IStorableValue value)
        {
            var v = (value as ObservableValue<TValue>);
            if (v == null)
                throw new Exception($"Cannot copy from storable of type '{value.GetType()}'");
            Value = v.Value;
        }

        public virtual object SerializeStorable()
        {
            object val = Value;
            if (val?.GetType() == typeof(DateTime?))
                val = (DateTime)val;
            if (val is DateTime dt)
                return dt.Ticks;
            return val;
        }

        #endregion

        #region Comparable
        public int CompareTo(object obj)
        {
            if (Value == null)
                return obj == null ? 0 : -1;
            var vc = Value as IComparable;
            if (vc == null)
                throw new NotSupportedException($"Cannot compare value of type {typeof(TValue)}.");
            object target = null;
            var st = obj as ObservableValue<TValue>;
            if (st != null)
                target = st.Value;
            else
                target = st;
            return vc.CompareTo(target);
        }

        public void DeserializeStorable(object value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class ObservableString : ObservableValue<string>
    {
        #region Constructor
        public ObservableString(string value, object owner = null) : base(value, owner)
        {
        }

        public ObservableString() : base()
        {
        }
        #endregion

        #region Operators
        public static implicit operator string(ObservableString v)
        {
            return v.Value;
        }

        public static implicit operator ObservableString(string v)
        {
            return new ObservableString(v);
        }
        #endregion
    }
}
