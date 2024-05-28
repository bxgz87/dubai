 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsgoSettings
{
    /// <summary>
    /// 用于解析和访问配置文件中的配置项的类。
    /// </summary>
    internal class ConfigParser
    {
        /// <summary>
        /// 存储解析后的配置项键值对的字典。
        /// </summary>
        private readonly Dictionary<string, string> _configurations = new Dictionary<string, string>();

        /// <summary>
        /// 构造函数，接收配置文件内容字符串。
        /// </summary>
        /// <param name="configContent">配置文件的文本内容。</param>
        public ConfigParser(string configContent)
        {
            ParseConfig(configContent);
        }

        /// <summary>
        /// 解析配置文件内容，提取配置项并存储到字典中。
        /// </summary>
        /// <param name="configContent">配置文件的文本内容。</param>
        private void ParseConfig(string configContent)
        {
            var lines = configContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var regex = new Regex("\"(.*?)\"\\s*\"(.*?)\"");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    _configurations[match.Groups[1].Value] = match.Groups[2].Value;
                }
            }
        }

        /// <summary>
        /// 根据配置项名称获取其对应的值。
        /// </summary>
        /// <param name="key">配置项的名称。</param>
        /// <returns>对应的配置项值。如果找不到指定的键，返回null。</returns>
        public string? GetConfigValue(string key)
        {
            if (_configurations.TryGetValue(key, out var value))
            {
                return value;
            }

            return null; // 根据实际需求，可以选择抛出异常或返回默认值
        }
    }
}
