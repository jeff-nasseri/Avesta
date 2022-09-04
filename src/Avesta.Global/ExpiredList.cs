using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Avesta.Global
{
    public class ExpiredList<T> : List<T>, IDisposable where T : ExpiredModel
    {
        readonly Timer _timer;
        public ExpiredList(TimeSpan delay)
        {
            _timer = new Timer(delay.TotalMilliseconds);
            _timer.Elapsed += new ElapsedEventHandler(ValidateExpiredTimer);
            _timer.Start();
        }
        public new void Add(T value)
        {
            value.ExpiredDate ??= DateTime.Now.AddMinutes(5);
            base.Add(value);
        }
        public void Add(T value, TimeSpan timeSpan)
        {
            value.ExpiredDate ??= DateTime.Now.Add(timeSpan);
            base.Add(value);
        }
        void ValidateExpiredTimer(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < Count; i++)
            {
                var data = this[i];
                if (DateTime.Now >= data.ExpiredDate)
                {
                    Remove(data);
                }
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class ExpiredModel : BaseModel
    {
        public DateTime? ExpiredDate { get; set; }
    }

    public class BaseModel
    {
        public string ID { get; set; }
    }

}
