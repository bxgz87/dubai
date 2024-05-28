using GameConfigurations;
using System.Diagnostics;
using System.Text;

namespace CsgoSettings
{
    public class Csgo : IGameSetting
    {
        public string GameName => "Csgo";

        public GameSettingsCollection GetGameSetting(string? data)
        {
            try
            {
                //// 获取配置文件
                //var configFilePath = @"C:\Users\ywb\Desktop\游戏配置说明\CSGO\cs2_video.txt";

                var streamProcess = Process.GetProcessesByName("steam")?.FirstOrDefault();

                if (streamProcess == null)
                    throw new Exception("找不到 steam 进程");

                // 获取 steam 进程的基路径
                var steamPath = ProcessHelper.GetProcessPath(streamProcess.Id);
                if (steamPath == null)
                    throw new Exception("找不到 steam 进程的基路径");

                // 查找所有符合条件的cs2_video.txt文件
                var files = FindCs2VideoFiles(Path.GetDirectoryName(steamPath)) ?? new List<string>();
                var configFilePath = FileHelper.GetMostRecentlyAccessedFilePath(files.ToArray());


                if (!File.Exists(configFilePath))
                    return new GameSettingsCollection(GameName);

                // 读取配置文件内容
                var configContent = File.ReadAllText(configFilePath, Encoding.UTF8);

                // 新建配置解析器
                ConfigParser configParser = new ConfigParser(configContent);

                // 多重采样抗锯齿
                var msaa_samples = configParser.GetConfigValue("setting.msaa_samples") ?? string.Empty;

                // 贴图过滤模式
                var r_texturefilteringquality = configParser.GetConfigValue("setting.r_texturefilteringquality") ?? string.Empty;

                // 光影细节
                var shaderquality = configParser.GetConfigValue("setting.shaderquality") ?? string.Empty;

                // 高动态范围
                var sc_hdr_enabled_override = configParser.GetConfigValue("setting.sc_hdr_enabled_override") ?? string.Empty;

                // 分辨率的 长
                var defaultres = configParser.GetConfigValue("setting.defaultres") ?? string.Empty;

                // 分辨率的 宽
                var defaultresheight = configParser.GetConfigValue("setting.defaultresheight") ?? string.Empty;

                // 刷新率
                var refreshrate_numerator = configParser.GetConfigValue("setting.refreshrate_numerator") ?? string.Empty;

                // 纵横比
                var aspectratiomode = configParser.GetConfigValue("setting.aspectratiomode") ?? string.Empty;

                return new GameSettingsCollection(GameName)
                {
                    GameSettings = new List<GameSetting>()
                    {
                        new GameSetting()
                        {
                            CategoryName = "视频设置",
                            Settings = new List<GameSetting.Setting>()
                            {
                                new GameSetting.Setting()
                                {
                                    InnerName = "msaa_samples",
                                    Name = "多重采样抗锯齿",
                                    Display = msaa_samples switch
                                    {
                                        "0" => "无",
                                        "1" => "CMAA2",
                                        "2" => "2xMSAA",
                                        "4" => "4x MSAA",
                                        "8" => "8xMSAA",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = msaa_samples,
                                    Type = msaa_samples.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "r_texturefilteringquality",
                                    Name = "贴图过滤模式",
                                    Display = r_texturefilteringquality switch
                                    {
                                        "0" => "双线性",
                                        "1" => "三线性",
                                        "2" => "异向性2x",
                                        "3" => "异向性4x",
                                        "4" => "异向性8x",
                                        "5" => "异向性16x",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = r_texturefilteringquality,
                                    Type = r_texturefilteringquality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "shaderquality",
                                    Name = "光影细节",
                                    Display = shaderquality switch
                                    {
                                        "0" => "低",
                                        "1" => "高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = shaderquality,
                                    Type = shaderquality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "sc_hdr_enabled_override",
                                    Name = "高动态范围",
                                    Display = sc_hdr_enabled_override switch
                                    {
                                        "3" => "性能",
                                        "-1" => "品质",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = sc_hdr_enabled_override,
                                    Type = sc_hdr_enabled_override.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "defaultres",
                                    Name = "分辨率宽",
                                    Display = defaultres,
                                    Value = defaultres,
                                    Type = defaultres.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "defaultresheight",
                                    Name = "分辨率高",
                                    Display = defaultresheight,
                                    Value = defaultresheight,
                                    Type = defaultresheight.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "refreshrate_numerator",
                                    Name = "刷新率",
                                    Display = refreshrate_numerator,
                                    Value = refreshrate_numerator,
                                    Type = refreshrate_numerator.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "aspectratiomode",
                                    Name = "游戏纵横比",
                                    Display = aspectratiomode switch
                                    {
                                        "0" => "4:3",
                                        "1" => "16:9",
                                        "2" => "16:10",
                                        //"2" => "异向性2x",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = aspectratiomode,
                                    Type = aspectratiomode.GetType().Name,
                                    Unit = ""
                                },
                            }
                        }
                    }
                };
            }
            catch (Exception)
            {
                return new GameSettingsCollection(GameName);
            }
        }

        /// <summary>
        /// 在指定的基路径下搜索符合条件的cs2_video.txt文件。
        /// </summary>
        private List<string> FindCs2VideoFiles(string basePath)
        {
            List<string> filesFound = new List<string>();
            string targetRelativePath = @"730\local\cfg\cs2_video.txt"; // 目标相对路径

            // 构建userdata的完整路径
            string userdataPath = Path.Combine(basePath, "userdata");

            try
            {
                // 检查userdata目录是否存在
                if (Directory.Exists(userdataPath))
                {
                    // 遍历userdata下的每个子目录
                    foreach (string directory in Directory.GetDirectories(userdataPath))
                    {
                        // 为每个子目录构造完整的cs2_video.txt文件路径
                        string filePath = Path.Combine(directory, targetRelativePath);
                        // 检查文件是否存在
                        if (File.Exists(filePath))
                        {
                            filesFound.Add(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }

            return filesFound;
        }
    }
}