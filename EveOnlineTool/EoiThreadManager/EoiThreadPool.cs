using System.Collections.Generic;

namespace EveOnlineIndustrialist.EoiThreadManager
{
    public class EoiThreadPool
    {
        public LinkedList<EoiThread> WaitingThreads { get; set; } = new LinkedList<EoiThread>();
        public EoiThread ActiveThread { get; set; }
    }
}
