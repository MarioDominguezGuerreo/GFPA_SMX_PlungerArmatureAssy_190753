//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endregion

#region Project Libraries

# endregion

namespace Software
{
    class OEE
    {
        #region Variables

        #region OEE
            private static double oEE;
            public double _OEE
            {
                get
                {
                    return oEE;
                }

                set
                {
                    oEE = value;
                }
            }
            private static double availability;
            public double Availability
            {
                get
                {
                    return availability;
                }

                set
                {
                    availability = value;
                }
            }
            private static double performance;
            public double Performance
            {
                get
                {
                    return performance;
                }

                set
                {
                    performance = value;
                }
            }
            private static double quality;
            public double Quality
            {
                get
                {
                    return quality;
                }

                set
                {
                    quality = value;
                }
            }

        #endregion

        #region Availability
        public struct sAvailability
        {
            private static string runtime;
            private static double runtime_s;
            private static string cycleTime;
            private static string downtime;
            private static double cycleTime_s;
            private static double downtime_s;
            public string CycleTime
            {
                get
                {
                    return cycleTime;
                }

                set
                {
                    cycleTime = value;
                }
            }
            public string Downtime
            {
                get
                {
                    return downtime;
                }

                set
                {
                    downtime = value;
                }
            }
            public string Runtime
            {
                get
                {
                    return runtime;
                }

                set
                {
                    runtime = value;
                }
            }
            public double CycleTime_s
            {
                get
                {
                    return cycleTime_s;
                }

                set
                {
                    cycleTime_s = value;
                }
            }
            public double Downtime_s
            {
                get
                {
                    return downtime_s;
                }

                set
                {
                    downtime_s = value;
                }
            }
            public double Runtime_s
            {
                get
                {
                    return runtime_s;
                }

                set
                {
                    runtime_s = value;
                }
            }
        }

        //Counters for Cycle Time, Downtime and Runtime
        private static int[] counterDowntime = new int[3];
        private static int[] counterCycleTime = new int[3];
        private static int[] counterRuntime = new int[3];
        public int[] CounterDowntime
        {
            get
            {
                return counterDowntime;
            }

            set
            {
                counterDowntime = value;
            }
        }
        public int[] CounterCycleTime
        {
            get
            {
                return counterCycleTime;
            }

            set
            {
                counterCycleTime = value;
            }
        }
        public int[] CounterRuntime
        {
            get
            {
                return counterRuntime;
            }

            set
            {
                counterRuntime = value;
            }
        }

        #region Process (Threads)
        private static bool[] status = new bool[4] { false, false, false, false };
        public bool[] Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        #endregion

        #endregion

        #region Performance
        public struct sPerformance
        {
            public int IdealCycleTime;
        }
        #endregion

        #region Quality
        public struct sQuality
        {
            private static int totalParts;
            private static int nGParts;
            private static int oKParts;
            public int NGParts
            {
                get
                {
                    return nGParts;
                }

                set
                {
                    nGParts = value;
                }
            }
            public int OKParts
            {
                get
                {
                    return oKParts;
                }

                set
                {
                    oKParts = value;
                }
            }
            public int TotalParts
            {
                get
                {
                    return totalParts;
                }

                set
                {
                    totalParts = value;
                }
            }
        }
        #endregion

        #region Planned Production
        public struct sProdTargets
        {
            private static string targetPieces;
            private static string targetCycle;
            private static string targetRun;
            private static string targetStop;
            public string TargetCycle
            {
                get
                {
                    return targetCycle;
                }

                set
                {
                    targetCycle = value;
                }
            }
            public string TargetRun
            {
                get
                {
                    return targetRun;
                }

                set
                {
                    targetRun = value;
                }
            }
            public string TargetStop
            {
                get
                {
                    return targetStop;
                }

                set
                {
                    targetStop = value;
                }
            }
            public string TargetPieces
            {
                get
                {
                    return targetPieces;
                }

                set
                {
                    targetPieces = value;
                }
            }
        }
    #endregion

        #region Produced
        private static double produced;
        public double Produced
        {
            get
            {
                return produced;
            }

            set
            {
                produced = value;
            }
        }
        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //OEE parameters
        public sAvailability OAvailability = new sAvailability();
        public sPerformance OPerformance = new sPerformance();
        public sQuality OQuality = new sQuality();
        public sProdTargets OProdPlan = new sProdTargets();
        #endregion

        #region Controls

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        /// <OEE>
        /// OEE = (Availabity) * (Performance) * (Quality)
        public double func_OEE()
        {
            #region Calculation
            oEE = quality / 100 * availability / 100 * performance / 100;
            oEE *= 100;
            #endregion

            return oEE;
        }
        ///Availability
        /// Availability = Runtime / Planned Production Time
        /// Runtime = Planned Production Time - Downtime
        public double func_Availability()
        {
            #region Calculation
            int PlannedTime = Convert.ToInt32(OProdPlan.TargetRun);
            int PlannedStopTime = Convert.ToInt32(OProdPlan.TargetStop);

            if (OAvailability.Runtime_s > 0 || PlannedTime > 0 || PlannedStopTime > 0)
            {
                int PlannedTime_seconds = PlannedTime * 60 - PlannedStopTime;

                availability = (PlannedTime_seconds - OAvailability.Downtime_s) * 100 / PlannedTime_seconds;
            }
            else
            {
                availability = 0.0;
            }
            #endregion

            return availability;
        }
        ///Performance
        /// Performance = Performance = (Ideal Cycle Time × Total Count) / Runtime
        public double func_Performance()
        {
            #region Calculation   
            int PlannedCycleTime = Convert.ToInt32(OProdPlan.TargetCycle);

            if (OQuality.TotalParts > 0 && PlannedCycleTime > 0 && OAvailability.Runtime_s > 0)
            {
                double Rate = OAvailability.Runtime_s / OQuality.TotalParts;

                performance = (PlannedCycleTime / Rate) * 100;

                if (performance > 100)
                {
                    performance = 100;
                }
            }
            else
            {
                performance = 0.0;
            }
            #endregion

            return performance;
        }
        ///Quality
        /// Quality = Good Parts / Total Parts
        public double func_Quality()
        {
            #region Calculation
            if (OQuality.OKParts > 0 || OQuality.NGParts > 0)
            {
                OQuality.TotalParts = OQuality.OKParts + OQuality.NGParts;
                quality = OQuality.OKParts * 100 / OQuality.TotalParts;
            }
            else
            {
                quality = 0.00;
            }
            #endregion

            return quality;
        }
        ///Production status (%)
        /// Produced = (Total Parts * 100) / Target Parts
        public double func_ProducedPorcent()
        {
            produced = (OQuality.TotalParts*100) /Convert.ToDouble(OProdPlan.TargetPieces);
            return produced;
        }
        #endregion

        #region Internal
        /// Counters for Time
        /// Counter to hh:mm:ss
        /// hh:mm:ss to Seconds
        public int[] Counter_hms4(int[] Count, int[] Count2)
        {
            int Segundos = Count[0] + Count2[0];
            if (Segundos > 59)
            {
                Segundos -= 59;
                Count[1]++;
                Count[0] = Segundos;
            }
            else
            {
                Count[0] = Segundos;
            }
            int Minutos = Count[1] + Count2[1];
            if (Minutos > 59)
            {
                Minutos -= 59;
                Count[2]++;
                Count[1] = Minutos;
            }
            else
            {
                Count[1] = Minutos;
            }
            int Horas = Count[2] + Count2[2];
            if (Horas > 23)
            {
                Count[0] = 0;
                Count[1] = 0;
                Count[2] = 0;
            }
            else
            {
                Count[2] = Horas;
            }

            return Count;
        }

        /// hh:mm:ss to Seconds
        public int[] Counter_hms3(int[] Count)
        {
            Count[0]++;
            if (Count[0] == 60)
            {
                Count[0] = 0;
                Count[1]++;
            }
            if (Count[1] == 60)
            {
                Count[1] = 0;
                Count[2]++;
            }
            if (Count[2] == 24)
            {
                Count[0] = 0;
                Count[1] = 0;
                Count[2] = 0;
            }
            return Count;
        }
        #endregion

        #endregion

        #region Threads

        #region Cycle Timer
        //Start Cycle Timer
        public void Init_CycleTimer1()
        {
            if (!Status[0])
            {
                //Cycle Time
                CounterCycleTime[0] = 0;
                CounterCycleTime[1] = 0;
                CounterCycleTime[2] = 0;
                //Active the timer
                Thread Tick = new Thread(new ThreadStart(() => CycleTimer1()));
                Tick.Start();
                Status[0] = true;
            }
        }
        public void CycleTimer1()
        {
            while (Status[0])
            {
                Thread.Sleep(1000);

                #region Counter
                int[] Result = Counter_hms3(CounterCycleTime);
                OAvailability.CycleTime = Result[2].ToString("00") + ":" + Result[1].ToString("00") + ":" + Result[0].ToString("00");
                OAvailability.CycleTime_s = (Result[2] * 60 * 60) + (Result[1] * 60) + (Result[0]);
                #endregion
            }
        }
        //Stop Cycle Timer
        public void Stop_CycleTimer1()
        {
            Status[0] = false;
        }
        #endregion

        #region Downtime Timer
        //Start Downtime Timer
        public void Init_Downtime()
        {
            if (!Status[1])
            {
                //Active the timer
                Thread Tick = new Thread(new ThreadStart(() => Downtime()));
                Tick.Start();
                Status[1] = true;
            }
        }
        public void Downtime()
        {
            while (Status[1])
            {
                Thread.Sleep(1000);

                #region Counter
                int[] Result = Counter_hms3(CounterDowntime);
                OAvailability.Downtime = Result[2].ToString("00") + ":" + Result[1].ToString("00") + ":" + Result[0].ToString("00");
                OAvailability.Downtime_s = (Result[2] * 60 * 60) + (Result[1] * 60) + (Result[0]);
                #endregion
            }
        }
        //Stop Downtime Timer
        public void Stop_Downtime()
        {
            Status[1] = false;
        }
        #endregion

        #endregion
    }
}
