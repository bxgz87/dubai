using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 定义了游戏设置的接口，提供了获取游戏名称和设置集合的方法。
    /// </summary>
    public interface IGameSetting
    {
        /// <summary>
        /// 获取游戏的名称。
        /// </summary>
        /// <value>游戏的名称，必须由实现此接口的类来提供。</value>
        string GameName { get; }

        /// <summary>
        /// 根据游戏主程序的路径，获取游戏的设置集合。
        /// </summary>
        /// <param name="data">附带的数据</param>
        /// <returns>一个表示游戏设置集合的 <see cref="GameSettingsCollection"/> 实例。</returns>
        GameSettingsCollection GetGameSetting(string? data = null);
    }
}
