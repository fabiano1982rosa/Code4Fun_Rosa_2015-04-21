using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementFileConnections
{
    public class Converter
    {
        private const string Separator = " ";
        private const string SeparatorTSV = "\t";
        private const string KeyLatency_ms = "latency_ms";
        public const string KeyBandwidth = "bandwidth";

        private int Bandwidth { get; set; }
        private int LatencyMs { get; set; }
        private int NumLatencyMs { get; set; }

        public int TotalBandwidth
        {
            get
            {
                return Bandwidth;
            }
        }

        public decimal AverageLatency 
        { 
            get 
            {
                return NumLatencyMs != 0 ? LatencyMs / NumLatencyMs : 0;
            }
        }

        public Converter()
        {
            LatencyMs = 0;
            Bandwidth = 0;
            NumLatencyMs = 0;
        }

        public void BinToTSV(string  fileName)
        {
            File.WriteAllLines(fileName + ".tsv",
                         File.ReadLines(fileName)                        
                            .Select(line => line.Replace(Separator, SeparatorTSV)));

            Bandwidth += File.ReadLines(fileName).Sum(x => x.Split(Separator.ToCharArray())[0] == KeyBandwidth? int.Parse(x.Split(Separator.ToCharArray())[1]): 0);
            LatencyMs += File.ReadLines(fileName).Sum(x => x.Split(Separator.ToCharArray())[0] == KeyLatency_ms ? int.Parse(x.Split(Separator.ToCharArray())[1]) : 0);
            NumLatencyMs += File.ReadLines(fileName).Count(x => x.Split(Separator.ToCharArray())[0] == KeyLatency_ms);
        }

        public void resetCounter()
        {
            LatencyMs = 0;
            Bandwidth = 0;
            NumLatencyMs = 0;
        }
    }
}
