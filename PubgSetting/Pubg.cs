using GameConfigurations;

namespace PubgSetting
{
    public class Pubg : IGameSetting
    {
        public string GameName => "绝地求生大逃杀";

        public GameSettingsCollection GetGameSetting(string? data)
        {
            
            try
            {
                var configFilePath = @"C:\Users\Administrator\AppData\Local\TslGame\Saved\Config\WindowsNoEditor\GameUserSettings.ini";

                var fullscreenMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "FullscreenMode") ?? string.Empty; // 显示模式
                var preferredFullscreenMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "PreferredFullscreenMode") ?? string.Empty; //全屏模式
                var resolutionWidth = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "ResolutionWidth") ?? string.Empty; // 屏幕分辨率宽度
                var resolutionHeight = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "ResolutionHeight") ?? string.Empty;// 屏幕分辨率高度

                var lobbyFrameRateLimitType = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "LobbyFrameRateLimitType") ?? string.Empty; // 大厅FPS 限制
                var inGameFrameRateLimitType = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "InGameFrameRateLimitType") ?? string.Empty; // 游戏内FPS限制
                var bUseInGameSmoothedFrameRate = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "bUseInGameSmoothedFrameRate") ?? string.Empty; // 平滑帧率
                var gamma = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "Gamma") ?? string.Empty; // Gamma 亮度

                var bEnableUniversalGamma = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "bEnableUniversalGamma") ?? string.Empty; // 整体亮度
                var screenScale = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "ScreenScale") ?? string.Empty; // 渲染比例
                var antiAliasingQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.AntiAliasingQuality") ?? string.Empty; // 抗锯齿
                var postProcessQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.PostProcessQuality") ?? string.Empty; // 后期处理

                var shadowQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.ShadowQuality") ?? string.Empty; // 阴影
                var textureQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.TextureQuality") ?? string.Empty; // 材质
                var effectsQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.EffectsQuality") ?? string.Empty; // 特效
                var foliageQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.FoliageQuality") ?? string.Empty; // 树木

                var viewDistanceQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ScalabilityGroups", "sg.ViewDistanceQuality") ?? string.Empty; // 可视距离
                var bSharpen = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "bSharpen") ?? string.Empty; // 鲜明度
                var bUseVSync = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "bUseVSync") ?? string.Empty; // 垂直同步
                var bMotionBlur = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/TslGame.TslGameUserSettings", "bMotionBlur") ?? string.Empty; // 运动模糊

                var graphicsAPI = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "/Script/Engine.GameUserSettings", "GraphicsAPI") ?? string.Empty; // DirectX 版本

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
                                    InnerName = "fullscreenMode",
                                    Name = "显示模式",
                                    Display = fullscreenMode switch
                                    {
                                        "0" => "窗口模式",
                                        "1" => "全屏模式",
                                        "2" => "窗口化全屏",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = fullscreenMode,
                                    Type = fullscreenMode.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "preferredFullscreenMode",
                                    Name = "首选显示模式",
                                    Display = preferredFullscreenMode switch
                                    {
                                        "0" => "窗口模式",
                                        "1" => "全屏模式",
                                        "2" => "窗口化全屏",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = preferredFullscreenMode,
                                    Type = preferredFullscreenMode.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "resolutionWidth",
                                    Name = "屏幕分辨率宽度",
                                    Display = resolutionWidth,
                                    Value = resolutionWidth,
                                    Type = resolutionWidth.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "resolutionHeight",
                                    Name = "屏幕分辨率高度",
                                    Display = resolutionHeight,
                                    Value = resolutionHeight,
                                    Type = resolutionHeight.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "lobbyFrameRateLimitType",
                                    Name = "大厅FPS 限制",
                                    Display = lobbyFrameRateLimitType switch
                                    {
                                        "Unlimited" => "无限制",
                                        "Fixed_60" => "60FPS",
                                        "Fixed_30" => "30FPS",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = lobbyFrameRateLimitType,
                                    Type = lobbyFrameRateLimitType.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "inGameFrameRateLimitType",
                                    Name = "游戏内FPS限制",
                                    Display = inGameFrameRateLimitType switch
                                    {
                                        "Unlimited" => "无限制",
                                        "DisplayBased" => "基于显示器",
                                        "Customizable" => "自定义",
                                        "InGameCustomFrameRateLimit" => "最大FPS",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = inGameFrameRateLimitType,
                                    Type = inGameFrameRateLimitType.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bUseInGameSmoothedFrameRate",
                                    Name = "平滑帧率",
                                    Display = bUseInGameSmoothedFrameRate switch
                                    {
                                        "True" => "启用",
                                        "False" => "禁用",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = bUseInGameSmoothedFrameRate,
                                    Type = bUseInGameSmoothedFrameRate.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "gamma",
                                    Name = "Gamma",
                                    Display = gamma,
                                    Value = gamma,
                                    Type = gamma.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bEnableUniversalGamma",
                                    Name = "整体亮度",
                                    Display = bEnableUniversalGamma switch
                                    {
                                        "True" => "启用",
                                        "False" => "禁用",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = bEnableUniversalGamma,
                                    Type = bEnableUniversalGamma.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "screenScale",
                                    Name = "渲染比例",
                                    Display = screenScale,
                                    Value = screenScale,
                                    Type = screenScale.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "antiAliasingQuality",
                                    Name = "抗锯齿",
                                    Display = antiAliasingQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = antiAliasingQuality,
                                    Type = antiAliasingQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "postProcessQuality",
                                    Name = "后期处理",
                                    Display = postProcessQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = postProcessQuality,
                                    Type = postProcessQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "shadowQuality",
                                    Name = "阴影",
                                    Display = shadowQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = shadowQuality,
                                    Type = shadowQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "textureQuality",
                                    Name = "材质",
                                    Display = textureQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = textureQuality,
                                    Type = textureQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "effectsQuality",
                                    Name = "特效",
                                    Display = effectsQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = effectsQuality,
                                    Type = effectsQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "foliageQuality",
                                    Name = "树木",
                                    Display = foliageQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = foliageQuality,
                                    Type = foliageQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "viewDistanceQuality",
                                    Name = "可视距离",
                                    Display = viewDistanceQuality switch
                                    {
                                        "0" => "非常低",
                                        "1" => "低",
                                        "2" => "中型",
                                        "3" => "高",
                                        "4" => "超高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = viewDistanceQuality,
                                    Type = viewDistanceQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bSharpen",
                                    Name = "鲜明度",
                                    Display = bSharpen switch
                                    {
                                        "True" => "启用",
                                        "False" => "禁用",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = bSharpen,
                                    Type = bSharpen.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bUseVSync",
                                    Name = "垂直同步",
                                    Display = bUseVSync switch
                                    {
                                        "True" => "启用",
                                        "False" => "禁用",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = bUseVSync,
                                    Type = bUseVSync.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "bMotionBlur",
                                    Name = "运动模糊",
                                    Display = bMotionBlur switch
                                    {
                                        "True" => "启用",
                                        "False" => "禁用",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = bMotionBlur,
                                    Type = bMotionBlur.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "graphicsAPI",
                                    Name = "DirectX 版本",
                                    Display = graphicsAPI,
                                    Value = graphicsAPI,
                                    Type = graphicsAPI.GetType().Name,
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
    }
}
