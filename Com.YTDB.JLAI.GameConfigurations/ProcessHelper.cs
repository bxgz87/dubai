using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 提供与进程相关的帮助方法。
    /// </summary>
    public class ProcessHelper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool QueryFullProcessImageName(
            IntPtr hProcess,
            int dwFlags,
            StringBuilder lpExeName,
            ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            bool bInheritHandle,
            int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            /// <summary>
            /// 查询进程信息的权限标志。
            /// </summary>
            QueryInformation = 0x0400,
        }

        /// <summary>
        /// 根据进程的PID获取该进程文件的绝对路径。
        /// </summary>
        /// <param name="pid">目标进程的PID（进程标识符）。</param>
        /// <returns>如果成功获取，返回进程文件的绝对路径；如果失败，返回null。</returns>
        /// <remarks>
        /// 该方法使用Win32 API获取特定PID的进程文件路径。
        /// 需要相应的权限来访问进程信息，否则可能返回null。
        /// </remarks>
        public static string? GetProcessPath(int pid)
        {
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.QueryInformation, false, pid);
            if (processHandle == IntPtr.Zero)
            {
                return null;
            }

            try
            {
                StringBuilder buffer = new StringBuilder(1024);
                int bufferSize = buffer.Capacity;
                if (QueryFullProcessImageName(processHandle, 0, buffer, ref bufferSize))
                {
                    return buffer.ToString();
                }
            }
            finally
            {
                CloseHandle(processHandle);
            }

            return null;
        }
    }
}
