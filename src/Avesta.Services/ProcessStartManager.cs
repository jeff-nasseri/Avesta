using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services
{
    public interface IProcessStartManager
    {
        void StartProcessFromOS(string fileName, string arguments = "");
        void StartProcessFromOS(ProcessStartInfo info);

    }
    public class ProcessStartManager : IProcessStartManager
    {
        public ProcessStartManager()
        {

        }


        public void StartProcessFromOS(string fileName, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

        }
        public void StartProcessFromOS(ProcessStartInfo info)
        {
            Process process = new Process();
            process.StartInfo = info;
            process.Start();
        }
    }
}
