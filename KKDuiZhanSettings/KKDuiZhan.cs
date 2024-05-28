using GameConfigurations;
using Microsoft.Win32;
using System.Text;

namespace KKDuiZhan
{
    public class KKDuiZhan : IGameSetting
    {
        public string GameName => "KK对战平台";

        // 注册表固定路径
        private const string RegistryPath = @"Software\Reckfeng\KK Battle Platform\Setting\GameGeneral";

        private string? GetRegistryValue(string valueName)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    if (key != null)
                    {
                        var value = key.GetValue(valueName);
                        return value?.ToString(); // 如果value为null，则返回null，否则返回value的字符串表示
                    }
                    return null; // 指定的键或值不存在
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public GameSettingsCollection GetGameSetting(string? datah)
        {
            try
            {
                var graphicsQuality = GetRegistryValue("GraphicsQuality") ?? string.Empty;

                var useOpenGL = GetRegistryValue("UseOpenGL") ?? string.Empty;

                var vulkan = GetRegistryValue("Vulkan") ?? string.Empty;

                var windowMode = GetRegistryValue("WindowMode") ?? string.Empty; ;

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
                                    InnerName = "graphicsQuality",
                                    Name = "图像质量",
                                    Display = graphicsQuality switch
                                    {
                                        "0" => "自定义",
                                        "1" => "低",
                                        "2" => "中",
                                        "3" => "高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = graphicsQuality,
                                    Type = graphicsQuality.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "useOpenGL",
                                    Name = "游戏渲染模式（OpenGL）",
                                    Display = useOpenGL switch
                                    {
                                        "0" => "关闭",
                                        "1" => "开启",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = useOpenGL,
                                    Type = useOpenGL.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "vulkan",
                                    Name = "游戏渲染模式（Vulkan实验性功能）",
                                    Display = vulkan switch
                                    {
                                        "0" => "关闭",
                                        "1" => "开启",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = vulkan,
                                    Type = vulkan.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName = "windowMode",
                                    Name = "显示模式",
                                    Display = windowMode switch
                                    {
                                        "0" => "全屏模式",
                                        "1" => "窗口模式",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = windowMode,
                                    Type = windowMode.GetType().Name,
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
