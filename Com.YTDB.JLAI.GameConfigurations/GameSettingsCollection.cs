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
    /// 游戏配置集合
    /// </summary>
    public class GameSettingsCollection
    {
        /// <summary>
        /// 初始化一个<see cref="GameSettingsCollection"/>类的实例
        /// </summary>
        /// <param name="gameName">游戏名字</param>
        public GameSettingsCollection(string gameName)
        {
            GameName = gameName;

            GameSettings = new List<GameSetting>();
        }

        /// <summary>
        /// 游戏名字
        /// </summary>
        [JsonProperty("gameName")]
        public string GameName { get;}

        /// <summary>
        /// 游戏设置集合
        /// </summary>
        [JsonProperty("gameSettings")]
        public List<GameSetting> GameSettings { get; set; }

        /// <summary>
        /// 返回当前对象的 JSON 对象
        /// </summary>
        /// <returns></returns>
        public JObject ToJson()
        {
            return JObject.FromObject(this);
        }

        /// <summary>
        /// 返回当前对象的 JSON 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
