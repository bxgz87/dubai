using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    public class GameSetting
    {
        /// <summary>
        /// 初始化 <see cref="GameSetting"/> 类的新实例。
        /// </summary>
        public GameSetting()
        {
            CategoryName = string.Empty;
            Settings = new List<Setting>();
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 设置项列表
        /// </summary>
        [JsonProperty("settings")]
        public List<Setting> Settings { get; set; }

        /// <summary>
        /// 设置项
        /// </summary>
        public class Setting
        {
            /// <summary>
            /// 初始化 <see cref="Setting"/> 类的新实例。
            /// </summary>
            public Setting()
            {
                Name = string.Empty;
                Display = string.Empty;
                Value = string.Empty;
                Unit = string.Empty;
                Type = string.Empty;
                InnerName = string.Empty;
            }

            /// <summary>
            /// 内部名字
            /// </summary>
            [JsonProperty("innerName")]
            public string InnerName { get; set; }

            /// <summary>
            /// 名称标识
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// 显示名称
            /// </summary>
            [JsonProperty("display")]
            public string Display { get; set; }

            /// <summary>
            /// 值
            /// </summary>
            [JsonProperty("value")]
            public string Value { get; set; }

            /// <summary>
            /// 类型（如int、float、bool）
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// 单位（如Hz、px、%），布尔类型设置此属性可为空
            /// </summary>
            [JsonProperty("unit")]
            public string Unit { get; set; }
        }
    }
}
