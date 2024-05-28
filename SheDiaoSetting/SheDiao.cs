using GameConfigurations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SheDiaoSetting
{
    public class SheDiao : IGameSetting
    {
        public string GameName => "射雕";

        public GameSettingsCollection GetGameSetting(string? data)
        {
            try
            {
                // 获取配置文件
                var process = Process.GetProcessesByName("shediao")?.FirstOrDefault();
                if (process == null)
                    throw new Exception("未找到游戏进程");

                var path = ProcessHelper.GetProcessPath(process.Id);

                if (path == null)
                    throw new Exception("未找到游戏路径");
                var folder = new DirectoryInfo(Path.GetDirectoryName(path)).Parent?.Parent?.FullName;
                if (string.IsNullOrEmpty(folder))
                    throw new Exception($"未找到 {path} 的上2级文件夹");
                var configFilePath = Path.Combine(folder, "config.ini");
                if (!File.Exists(configFilePath))
                    throw new Exception($"{configFilePath} 不存在");

                // var configFilePath = @"C:\Users\ywb\Desktop\游戏配置说明\射雕\config.ini";

                var resolution = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "resolution") ?? string.Empty; // 显示模式
                var is_grading_level_custom = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Garding_Data", "is_grading_level_custom") ?? string.Empty; // 自定义配置
                //var key_graph_shadow_precision = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Garding_Data", "key_graph_shadow_precision"); // 阴影精度
                var key_graph_quality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_quality") ?? string.Empty; // 图像质量
                //var key_graph_fps = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Garding_Data", "key_graph_fps"); // 帧率
                var key_graph_vsync = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_vsync") ?? string.Empty; // 垂直同步

                //var key_graph_lod = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_lod"); // 场景细节
                //var key_graph_grass_shadow = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_grass_shadow"); // 草地阴影
                //var key_grass_distance = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_grass_distance"); // 草地加载距离
                //var key_tree_distance = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_tree_distance"); // 树的加载距离
                var key_graph_aa = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_aa") ?? string.Empty; // 抗锯齿
                //var key_graph_vsync = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_graph_vsync"); // 体积云
                var key_water_precision = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_water_precision") ?? string.Empty; // 水面精度
                var key_hero_cloth_simulation = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "setting", "key_hero_cloth_simulation") ?? string.Empty; // 布料效果

                // 使用正则表达式匹配 分辨率 和 显示模式
                var match = Regex.Match(resolution, @"(\d+x\d+)([wf]?)", RegexOptions.IgnoreCase);
                string _resolution = string.Empty;
                var fullModel = string.Empty;
                if (match.Success)
                {
                    _resolution = match.Groups[1].Value;
                    fullModel = match.Groups[2].Value;
                }

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
                                    InnerName ="resolution",
                                    Name = "分辨率",
                                    Display = _resolution switch
                                    {
                                        "" => "系统默认",
                                        _ => _resolution, // 默认情况
                                    },
                                    Value = _resolution,
                                    Type = _resolution.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName ="fullModel",
                                    Name = "显示模式",
                                    Display = fullModel switch
                                    {
                                        "w" => "窗口模式",
                                        "f" => "全屏模式",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = fullModel,
                                    Type = fullModel.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName ="is_grading_level_custom",
                                    Name = "自定义配置",
                                    Display = is_grading_level_custom switch
                                    {
                                        "0" => "关闭",
                                        "1" => "开启",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = is_grading_level_custom,
                                    Type = is_grading_level_custom.GetType().Name,
                                    Unit = ""
                                },
                                //new GameSetting.Setting()
                                //{
                                //    Name = "阴影精度",
                                //    Display = key_graph_shadow_precision switch
                                //    {
                                //        "1" => "高",
                                //        "2" => "低",
                                //        _ => "系统默认", // 默认情况
                                //    },
                                //    Value = key_graph_shadow_precision,
                                //    Type = key_graph_shadow_precision.GetType().Name,
                                //    Unit = ""
                                //},
                                new GameSetting.Setting()
                                {
                                    InnerName ="key_graph_quality",
                                    Name = "图像质量",
                                    Display = key_graph_quality switch
                                    {
                                        "0" => "标准",
                                        "1" => "流畅",
                                        "2" => "省电",
                                        "3" => "自定义",
                                        "8" => "精致",
                                        "10" => "原画",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = key_graph_quality,
                                    Type = key_graph_quality.GetType().Name,
                                    Unit = ""
                                },
                                //new GameSetting.Setting()
                                //{
                                //    Name = "帧率",
                                //    Display = key_graph_fps switch
                                //    {
                                //        "1" => "60",
                                //        "2" => "30",
                                //        "3" => "144",
                                //        "4" => "无限制",
                                //        _ => "未知", // 默认情况
                                //    },
                                //    Value = key_graph_fps,
                                //    Type = key_graph_fps.GetType().Name,
                                //    Unit = ""
                                //},
                                new GameSetting.Setting()
                                {
                                    InnerName ="key_graph_vsync",
                                    Name = "垂直同步",
                                    Display = key_graph_vsync switch
                                    {
                                        "0" => "关闭",
                                        "1" => "开启",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = key_graph_vsync,
                                    Type = key_graph_vsync.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName ="key_graph_aa",
                                    Name = "抗锯齿",
                                    Display = key_graph_aa switch
                                    {
                                        "0" => "关闭",
                                        "1" => "标准抗锯齿",
                                        "2" => "DLSS",
                                        "3" => "DLSS高品质",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = key_graph_aa,
                                    Type = key_graph_aa.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName ="key_water_precision",
                                    Name = "水面精度",
                                    Display = key_water_precision switch
                                    {
                                        "0" => "低",
                                        "1" => "高",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = key_water_precision,
                                    Type = key_water_precision.GetType().Name,
                                    Unit = ""
                                },
                                new GameSetting.Setting()
                                {
                                    InnerName ="key_hero_cloth_simulation",
                                    Name = "布料效果",
                                    Display = key_hero_cloth_simulation switch
                                    {
                                        "0" => "关闭",
                                        "1" => "开启",
                                        _ => "未知", // 默认情况
                                    },
                                    Value = key_hero_cloth_simulation,
                                    Type = key_hero_cloth_simulation.GetType().Name,
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