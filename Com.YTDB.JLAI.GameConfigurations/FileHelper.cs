using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 提供与文件相关的帮助方法。
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 返回最后访问时间距离现在最近的文件绝对路径。
        /// </summary>
        /// <param name="filePaths">文件绝对路径的数组。</param>
        /// <returns>最后访问时间距离现在最近的文件绝对路径；如果所有文件都不存在，返回null。</returns>
        public static string? GetMostRecentlyAccessedFilePath(string[] filePaths)
        {
            try
            {
                if (filePaths == null || filePaths.Length == 0)
                    return null;

                string? mostRecentFilePath = null;
                DateTime mostRecentAccessTime = DateTime.MinValue;

                foreach (string filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        DateTime lastAccessTime = File.GetLastAccessTime(filePath);
                        if (lastAccessTime > mostRecentAccessTime)
                        {
                            mostRecentAccessTime = lastAccessTime;
                            mostRecentFilePath = filePath;
                        }
                    }
                }

                return mostRecentFilePath;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
