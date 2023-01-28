using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public class GameStateSingleton
    {
        private static GameStateSingleton instance;

        public int[] cntMaxRockets = new int[3];

        public int[] cntRockets = new int[3];

        public int[] cntMaxPlains = new int[3];

        public int[] cntKilledPlains = new int[3];

        private bool isReloaded =true;

        public bool IsReloaded { get=>isReloaded; }

        public const double reloadTime = 0.7; //in seconds

        public bool Ispause = false;

        public int levelLoaded;

        public void Reload()
        {
            isReloaded = false;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, a) =>
             {
                 isReloaded = true;
                 timer.Stop();
             };
            timer.Interval = TimeSpan.FromSeconds(reloadTime);
            timer.Start();
        }
        private GameStateSingleton() {}


        public static GameStateSingleton  GetInstance()
        {
            if (instance==null)
            {
                instance = new GameStateSingleton();
            }
            return instance;
        }
    }
}
