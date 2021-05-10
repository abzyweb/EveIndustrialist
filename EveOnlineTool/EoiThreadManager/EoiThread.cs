using System;
using System.Threading;

namespace EveOnlineIndustrialist.EoiThreadManager
{
    public class EoiThread
    {
        public EoiThread(System.Action start, string threadPool = "default", bool forceStart = false) : this(threadPool, forceStart)
        {
            Thread = new Thread(() => start()) { IsBackground = true };
        }
        public EoiThread(Action<object> start, object arguments, string threadPool = "default", bool forceStart = false) : this(threadPool, forceStart)
        {
            this.Arguments = arguments;

            Thread = new Thread(() => start(this.Arguments)) { IsBackground = true };
        }

        private EoiThread(string threadPool, bool forceStart)
        {   
            this.ThreadPool = threadPool;
            this.ForceStart = forceStart;
        }

        public string ThreadPool { get; set; }
        public object Arguments { get; set; }
        public Thread Thread { get; set; }
        public bool ForceStart { get; set; }
    }
}
