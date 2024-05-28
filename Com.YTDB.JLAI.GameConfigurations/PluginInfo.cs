using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 插件信息类，包含插件的版本、标题、作者、说明和用于反射加载的主DLL文件。
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// 构造函数，初始化插件信息的新实例。
        /// </summary>
        public PluginInfo()
        {
            Version = 0;
            Author = string.Empty;
            GameName = string.Empty;
            Notes = string.Empty;
            MainDLL = string.Empty;
            Title = string.Empty;
        }

        /// <summary>
        /// 插件版本。
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 游戏名字。
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 插件标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 插件作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 插件说明。
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 用于反射加载的DLL文件路径。
        /// </summary>
        public string MainDLL { get; set; }

        /// <summary>
        /// 获取插件信息
        /// </summary>
        /// <param name="pluginFolder">插件所在的文件夹</param>
        /// <returns></returns>
        public static List<PluginInfo> LoadPlugin(string pluginFolder)
        {
            try
            {
                List<PluginInfo> plugins = new List<PluginInfo>();

                if (string.IsNullOrEmpty(pluginFolder))
                    return plugins;

                if (!Directory.Exists(pluginFolder))
                    return plugins;

                // 读取插件配置文件
                var pluginConfigFile = Path.Combine(pluginFolder, "config.json");

                if(!File.Exists(pluginConfigFile))
                    return plugins;

                var array = JArray.Parse(File.ReadAllText(pluginConfigFile, Encoding.UTF8));

                foreach (var jsonData in array)
                {
                    try
                    {
                        if (!int.TryParse(jsonData["Version"]?.ToString(), out int version))
                            // throw new Exception("版本错误");
                            continue;


                        var gameName = jsonData["GameName"]?.ToString()?.Trim() ?? string.Empty;
                        if (string.IsNullOrEmpty(gameName))
                            //throw new Exception("游戏名字为空");
                            continue;

                        var title = jsonData["Title"]?.ToString()?.Trim() ?? string.Empty;
                        if (string.IsNullOrEmpty(title))
                            // throw new Exception("标题为空");
                            continue;

                        var author = jsonData["Author"]?.ToString()?.Trim() ?? string.Empty;

                        var notes = jsonData["Notes"]?.ToString()?.Trim() ?? string.Empty;

                        var mainDLL = jsonData["MainDLL"]?.ToString()?.Trim();
                        if (string.IsNullOrEmpty(mainDLL))
                            //throw new Exception("主dll路径为空");
                            continue;

                        mainDLL = Path.Combine(pluginFolder, mainDLL);

                        if (!File.Exists(mainDLL))
                            //throw new Exception("主dll文件不存在");
                            continue;

                        plugins.Add(new PluginInfo()
                        {
                            Version = version,
                            GameName = gameName,
                            Title = title,
                            Author = author,
                            Notes = notes,
                            MainDLL = mainDLL
                        });
                    }
                    catch (Exception)
                    {

                    }
                }
                return plugins;
            }
            catch (Exception)
            {
                return new List<PluginInfo>(0);
            }
        }
    }
}
