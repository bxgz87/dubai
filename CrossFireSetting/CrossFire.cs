using GameConfigurations;

namespace CrossFireSetting
{
    /// <summary>
    /// “穿越火线”游戏的具体设置类，继承自基础游戏设置基类。
    /// </summary>
    public class CrossFire : IGameSetting
    {
        /// <summary>
        /// 获取游戏名称。
        /// </summary>
        public string GameName => "穿越火线";

        /// <summary>
        /// 构造函数，初始化“穿越火线”设置类的新实例。
        /// </summary>
        public CrossFire() : base()
        {

        }

        /// <summary>
        /// 更新游戏设置信息，从配置文件中读取数据。
        /// </summary>
        /// <param name="gameExecutablePath">游戏的主程序路径</param>
        private void Update(string gameExecutablePath)
        {
            try
            {
                var configFilePath = gameExecutablePath;

                if (string.IsNullOrEmpty(configFilePath))
                    throw new Exception("配置文件为空");

                if (!File.Exists(configFilePath))
                    throw new Exception("配置文件不存在");

                // 读取 大厅分辨率
                var lobbyResolution = IniOperation.InIHelper.ReadConfig<int>(configFilePath, "Graphic", "LobbyResolution");

                // 读取 大厅长宽比
                var lobbyAspectRatio = IniOperation.InIHelper.ReadConfig<int>(configFilePath, "Graphic", "LobbyAspectRatio");

                // 读取 游戏内分辨率
                var resolution = IniOperation.InIHelper.ReadConfig<int>(configFilePath, "Graphic", "Resolution");

                // 读取 游戏内长宽比
                var aspectRatio = IniOperation.InIHelper.ReadConfig<int>(configFilePath, "Graphic", "AspectRatio");

                this.lobbyResolution = GetResolution("大厅分辨率", lobbyResolution, "lobbyResolution");
                this.lobbyAspectRatio = GetAspectRatio("大厅长宽比", lobbyAspectRatio, "lobbyAspectRatio");
                this.resolution = GetResolution("游戏内分辨率", resolution, "resolution");
                AspectRatio = GetAspectRatio("游戏内长宽比", aspectRatio, "aspectRatio");
            }
            catch (Exception)
            {
                lobbyAspectRatio = null;
                lobbyAspectRatio = null;
                resolution = null;
                AspectRatio = null;
            }
        }

        /// <summary>
        /// 根据给定的分辨率代码获取分辨率设置。
        /// </summary>
        /// <param name="name">分辨率设置的名称。</param>
        /// <param name="resolution">分辨率代码。</param>
        /// <param name="innerName">内部名字</param>
        /// <returns>分辨率设置。</returns>
        private GameSetting.Setting? GetResolution(string name, int resolution, string innerName)
        {
            string? displayValue = null;
            switch (resolution)
            {
                case 0:
                    displayValue = "800x600";
                    break;
                case 1:
                    displayValue = "1024x768";
                    break;
                case 3:
                    displayValue = "1280x720";
                    break;
                case 16:
                    displayValue = "1920*1080";
                    break;
                default:
                    return null;
            }

            return new GameSetting.Setting()
            {
                Name = name,
                Display = displayValue,
                Unit = string.Empty,
                Value = resolution.ToString(),
                Type = resolution.GetType().Name,
                InnerName = innerName
            };
        }

        /// <summary>
        /// 根据给定的长宽比代码获取长宽比设置。
        /// </summary>
        /// <param name="name">长宽比设置的名称。</param>
        /// <param name="aspectRatio">长宽比代码。</param>
        /// <param name="innerName">内部名字</param>
        /// <returns>长宽比设置。</returns>
        private GameSetting.Setting? GetAspectRatio(string name, int aspectRatio, string innerName)
        {
            string? displayValue = null;
            switch (aspectRatio)
            {
                case 0:
                    displayValue = "4:3";
                    break;
                case 3:
                    displayValue = "16:9";
                    break;
                default:
                    return null;
            }

            return new GameSetting.Setting()
            {
                Name = name,
                Display = displayValue,
                Unit = string.Empty,
                Value = aspectRatio.ToString(),
                Type = aspectRatio.GetType().Name,
                InnerName = innerName
            };
        }

        /// <summary>
        /// 获取游戏的设置集合。
        /// </summary>
        /// <returns>一个包含所有相关游戏设置的 <see cref="GameSettingsCollection"/> 实例。</returns>
        public GameSettingsCollection GetGameSetting(string? data)
        {
            var configFilePath = @"C:\Users\Administrator\Documents\CFSystem\System.ini";
            Update(configFilePath);

            var lobbySettings = new List<GameSetting.Setting>();
            var gameSettings = new List<GameSetting.Setting>();

            if (lobbyResolution != null)
                lobbySettings.Add(lobbyResolution);
            if (lobbyAspectRatio != null)
                lobbySettings.Add(lobbyAspectRatio);

            if (resolution != null)
                gameSettings.Add(resolution);
            if (AspectRatio != null)
                gameSettings.Add(AspectRatio);

            return new GameSettingsCollection(GameName)
            {
                GameSettings = new List<GameSetting>()
                {
                    new GameSetting()
                    {
                        CategoryName = "大厅设置",
                        Settings = lobbySettings
                    },
                    new GameSetting()
                    {
                        CategoryName = "游戏内设置",
                        Settings = gameSettings
                    }
                }
            };
        }

        /// <summary>
        /// 大厅分辨率设置。
        /// </summary>
        private GameSetting.Setting? lobbyResolution;

        /// <summary>
        /// 大厅长宽比设置。
        /// </summary>
        private GameSetting.Setting? lobbyAspectRatio;

        /// <summary>
        /// 游戏内分辨率设置。
        /// </summary>
        private GameSetting.Setting? resolution;

        /// <summary>
        /// 游戏内长宽比设置。
        /// </summary>
        private GameSetting.Setting? AspectRatio;
    }
}
