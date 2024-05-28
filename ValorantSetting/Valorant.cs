using GameConfigurations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ValorantSetting
{
    public class Valorant : IGameSetting
    {
        public string GameName => "无畏契约";

        public GameSettingsCollection GetGameSetting(string? data)
        {
            try
            {
                // 获取配置文件
                var process = Process.GetProcessesByName("VALORANT-Win64-Shipping")?.FirstOrDefault();
                if (process == null)
                    throw new Exception("未找到游戏进程");

                var path = ProcessHelper.GetProcessPath(process.Id);

                if (path == null)
                    throw new Exception("未找到游戏路径");
                var folder = new DirectoryInfo(Path.GetDirectoryName(path)).Parent?.Parent?.FullName;
                if (string.IsNullOrEmpty(folder))
                    throw new Exception($"未找到 {path} 的上2级文件夹");
                var gameUserSettingsPath = Path.Combine(folder, "Saved\\Config\\Windows\\GameUserSettings.ini");
                var riotUserSettingsPath = Path.Combine(folder, "Saved\\Config\\Windows\\RiotUserSettings.ini");

                // 获取配置文件
                //var gameUserSettingsPath = @"C:\Users\ywb\Desktop\游戏配置说明\无畏契约\GameUserSettings.ini";
                //var riotUserSettingsPath = @"C:\Users\ywb\Desktop\游戏配置说明\无畏契约\RiotUserSettings.ini";

                if (!File.Exists(gameUserSettingsPath))
                    throw new Exception($"{gameUserSettingsPath} 不存在");

                if (!File.Exists(riotUserSettingsPath))
                    throw new Exception($"{riotUserSettingsPath} 不存在");

                /* RiotUserSettings.ini配置文件 */
                // 材料质量
                var materialQuality = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::MaterialQuality") ?? string.Empty; // 读取材料质量设置

                // 纹理质量
                var textureQuality = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::TextureQuality") ?? string.Empty; // 读取纹理质量设置

                // 细节质量
                var detailQuality = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::DetailQuality") ?? string.Empty; // 读取细节质量设置

                // 界面质量
                var uiQuality = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::UIQuality") ?? string.Empty; // 读取界面质量设置

                // 渐晕效果
                var vignetteEnabled = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::VignetteEnabled") ?? string.Empty; // 读取渐晕效果设置

                // 抗锯齿
                var antiAliasing = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::AntiAliasing") ?? string.Empty; // 读取抗锯齿设置

                // 各项异性过滤
                var anisotropicFiltering = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::AnisotropicFiltering") ?? string.Empty; // 读取各项异性过滤设置

                // 提高清晰度
                var improveClarity = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::ImproveClarity") ?? string.Empty; // 读取提高清晰度设置

                // 失真
                var disableDistortion = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::DisableDistortion") ?? string.Empty; // 读取失真设置

                // 投射阴影
                var shadowsEnabled = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::ShadowsEnabled") ?? string.Empty; // 读取投射阴影设置

                // 泛光
                var bloomQuality = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresIntSettingName::BloomQuality") ?? string.Empty; // 读取泛光设置

                // [测试]实验性锐化
                var adaptiveSharpenEnabled = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::AdaptiveSharpenEnabled") ?? string.Empty; // 读取实验性锐化设置

                // 始终限制FPS
                var limitFramerateAlways = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresBoolSettingName::LimitFramerateAlways") ?? string.Empty; // 读取始终限制FPS设置

                // 始终最高FPS
                var maxFramerateAlways = IniOperation.InIHelper.ReadConfig<string>(riotUserSettingsPath, "Settings", "EAresFloatSettingName::MaxFramerateAlways") ?? string.Empty; // 读取始终最高FPS设置


                /* GameUserSettings.ini配置文件 */
                // 垂直同步
                var bUseVSync = IniOperation.InIHelper.ReadConfig<string>(gameUserSettingsPath, "/Script/ShooterGame.ShooterGameUserSettings", "bUseVSync") ?? string.Empty; // 读取垂直同步设置

                // 显示模式
                var fullscreenMode = IniOperation.InIHelper.ReadConfig<string>(gameUserSettingsPath, "/Script/ShooterGame.ShooterGameUserSettings", "FullscreenMode") ?? string.Empty; // 读取显示模式设置


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
                                    InnerName = "materialQuality",
                                    Name = "材料质量",
                                    Display = materialQuality switch
                                    {
                                        "" => "低",
                                        "2" => "中",
                                        "1" => "高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = materialQuality,
                                    Type = materialQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "textureQuality",
                                    Name = "纹理质量",
                                    Display = textureQuality switch
                                    {
                                        "" => "低",
                                        "1" => "中",
                                        "2" => "高",
                                        _ => "未知",
                                    },
                                    Value = textureQuality,
                                    Type = textureQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "detailQuality",
                                    Name = "细节质量",
                                    Display = detailQuality switch
                                    {
                                        "" => "低",
                                        "1" => "中",
                                        "2" => "高",
                                        _ => "未知",
                                    },
                                    Value = detailQuality,
                                    Type = detailQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "uiQuality",
                                    Name = "界面质量",
                                    Display = uiQuality switch
                                    {
                                        "" => "低",
                                        "1" => "中",
                                        "2" => "高",
                                        _ => "未知",
                                    },
                                    Value = uiQuality,
                                    Type = uiQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "vignetteEnabled",
                                    Name = "渐晕",
                                    Display = vignetteEnabled switch
                                    {
                                        "True" => "开",
                                        "False" => "关",
                                        "" => "关",
                                        _ => "未知",
                                    },
                                    Value = vignetteEnabled,
                                    Type = vignetteEnabled.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "antiAliasing",
                                    Name = "抗锯齿",
                                    Display = antiAliasing switch
                                    {
                                        "" => "无",
                                        "1" => "MSAA 2x",
                                        "2" => "MSAA 4x",
                                        "4" => "FXAA",
                                        _ => "未知",
                                    },
                                    Value = antiAliasing,
                                    Type = antiAliasing.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "anisotropicFiltering",
                                    Name = "各项异性过滤",
                                    Display = anisotropicFiltering switch
                                    {
                                        "" => "1x",
                                        "2" => "2x",
                                        "4" => "4x",
                                        "8" => "8x",
                                        "16" => "16x",
                                        _ => "未知",
                                    },
                                    Value = anisotropicFiltering,
                                    Type = anisotropicFiltering.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "improveClarity",
                                    Name = "提高清晰度",
                                    Display = improveClarity switch
                                    {
                                        "True" => "开",
                                        "False" => "关",
                                        "" => "关",
                                        _ => "未知",
                                    },
                                    Value = improveClarity,
                                    Type = improveClarity.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "disableDistortion",
                                    Name = "失真",
                                    Display = disableDistortion switch
                                    {
                                        "True" => "开",
                                        "False" => "关",
                                        "" => "关",
                                        _ => "未知",
                                    },
                                    Value = disableDistortion,
                                    Type = disableDistortion.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "shadowsEnabled",
                                    Name = "投射阴影",
                                    Display = shadowsEnabled switch
                                    {
                                        "True" => "开",
                                        "False" => "关",
                                        "" => "关",
                                        _ => "未知",
                                    },
                                    Value = shadowsEnabled,
                                    Type = shadowsEnabled.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bloomQuality",
                                    Name = "泛光",
                                    Display = bloomQuality switch
                                    {
                                        "0" => "关闭",
                                        "" => "关闭",
                                        _ => "未知",
                                    },
                                    Value = bloomQuality,
                                    Type = bloomQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "adaptiveSharpenEnabled",
                                    Name = "实验性锐化",
                                    Display = adaptiveSharpenEnabled switch
                                    {
                                        "True" => "开",
                                        "" => "关闭",
                                        _ => "未知",
                                    },
                                    Value = adaptiveSharpenEnabled,
                                    Type = adaptiveSharpenEnabled.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "limitFramerateAlways",
                                    Name = "始终限制FPS",
                                    Display = limitFramerateAlways switch
                                    {
                                        "True" => "开",
                                        "" => "关闭",
                                        _ => "未知",
                                    },
                                    Value = limitFramerateAlways,
                                    Type = limitFramerateAlways.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "maxFramerateAlways",
                                    Name = "始终最高FPS",
                                    Display = maxFramerateAlways switch
                                    {
                                        "" => "默认",
                                        _ => maxFramerateAlways,
                                    },
                                    Value = maxFramerateAlways,
                                    Type = maxFramerateAlways.GetType().Name,
                                    Unit = ""
                                },

                                new GameSetting.Setting()
                                {
                                    InnerName = "bUseVSync",
                                    Name = "垂直同步",
                                    Display = bUseVSync switch
                                    {
                                        "True" => "开",
                                        "False" => "关",
                                        _ => bUseVSync,
                                    },
                                    Value = bUseVSync,
                                    Type = bUseVSync.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "fullscreenMode",
                                    Name = "显示模式",
                                    Display = fullscreenMode switch
                                    {
                                        "" => "全屏模式",
                                        "1" => "全屏模式",
                                        "2" => "窗口模式",
                                        _ => "未知",
                                    },
                                    Value = fullscreenMode,
                                    Type = fullscreenMode.GetType().Name,
                                    Unit = ""
                                }
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
    }
}
