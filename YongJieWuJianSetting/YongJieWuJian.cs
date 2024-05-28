using GameConfigurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace YongJieWuJianSetting
{
    public class YongJieWuJian : IGameSetting
    {
        public string GameName => "永劫无间";

        public GameSettingsCollection GetGameSetting(string? data)
        {
            // 获取配置文件所在的文件夹
            var process = Process.GetProcessesByName("NarakaBladepoint")?.FirstOrDefault();
            if (process == null)
                throw new Exception("未找到游戏进程");

            var path = ProcessHelper.GetProcessPath(process.Id);

            if (path == null)
                throw new Exception("未找到游戏路径");
            var folder = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(folder))
                throw new Exception($"未找到 {path} 文件夹");

            // 获取配置文件
            //E:\work\游戏配置获取\游戏配置获取\bin\Debug\GameSetting\
            var configFilePath = @"E:\work\游戏配置获取\game\yongjiewujian\QualitySettingsData.txt";
           

           // var configFilePath = @"C:\Users\ywb\Desktop\游戏配置说明\永劫无间\QualitySettingsData.txt";
         
            //var configFilePath = Path.Combine(folder, @"NarakaBladepoint_Data\QualitySettingsData.txt");

            if (!File.Exists(configFilePath))
                throw new Exception("配置文件不存在");

            var jsonData = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(configFilePath, Encoding.UTF8)) ?? new JObject();

            var graphicQuality = jsonData["l22GraphicQualityLevel"] ?? new JObject();

            // 画面质量设置
            var modelQualityLevel = graphicQuality["m_modelQualityLevel"]?.Value<string>() ?? string.Empty; // 模型精度，值范围为0到3。极低，低，中，高
            var tessellationQualityLevel = graphicQuality["m_tessellationQualityLevel"]?.Value<string>() ?? string.Empty; // 细分曲面，值范围为0到3。关闭，低，中，高
            var visualEffectsQualityLevel = graphicQuality["m_visualEffectsQualityLevel"]?.Value<string>() ?? string.Empty; // 特效品质，值范围为0到3。低，中，高，极高
            var textureQualityLevel = graphicQuality["m_textureQualityLevel"]?.Value<string>() ?? string.Empty; // 贴图质量，值范围为0到3。极低，低，中，高
            var shadowQualityLevel = graphicQuality["m_shadowQualityLevel"]?.Value<string>() ?? string.Empty; // 阴影质量，值范围为0到4。极低，低，中，高，极高
            var volumetricLightLevel = graphicQuality["m_volumetricLightLevel"]?.Value<string>() ?? string.Empty; // 体积光质量，值范围为0到3。低，中，高，极高
            var cloudQualityLevel = graphicQuality["m_cloudQualityLevel"]?.Value<string>() ?? string.Empty; // 体积云质量，值范围为0到4。关闭，低，中，高，极高
            var aoLevel = graphicQuality["m_aoLevel"]?.Value<string>() ?? string.Empty; // 环境光遮蔽，值范围为0到2。关闭，低，高
            var SSRLevel = graphicQuality["m_SSRLevel"]?.Value<string>(); // 屏幕空间反射，值范围为0到4。关闭，低，中，高，极高
            var AALevel = graphicQuality["m_AALevel"]?.Value<string>() ?? string.Empty; // 抗锯齿，值范围为0到2。低，中，高
            var PostProcessingLevel = graphicQuality["m_PostProcessingLevel"]?.Value<string>() ?? string.Empty; // 后处理质量，值范围为0到3。极低，低，中，高
            var LightingQualityLevel = graphicQuality["m_LightingQualityLevel"]?.Value<string>() ?? string.Empty; // 光照质量，值范围为0到3。低，中，高，极高

            // 一般设置
            var systemQuality = jsonData["l22SystemQualitySetting"] ?? new JObject();
            var renderScale = systemQuality["renderScale"]?.Value<float>() ?? 0f; // 渲染比例，值范围为0到1。
            var aaMode = systemQuality["aaMode"]?.Value<string>() ?? string.Empty; // 抗锯齿算法，0关闭，1TAA，2SMAA。
            var fullScreenMode = systemQuality["fullScreenMode"]?.Value<string>() ?? string.Empty; // 画面模式，0全屏，1无边框窗口，2窗口化。
            var resolutionWidth = systemQuality["resolutionWidth"]?.Value<string>() ?? string.Empty; // 分辨率宽度。
            var resolutionHeight = systemQuality["resolutionHeight"]?.Value<string>() ?? string.Empty; ; // 分辨率高度。
            var frameRateLimit = systemQuality["frameRateLimit"]?.Value<string>() ?? string.Empty; // 帧数上限，-1代表无限制。
            var styleMode = systemQuality["styleMode"]?.Value<string>() ?? string.Empty; // 画面风格，值范围为0到4，对应不同风格。
            var mHDRMode = systemQuality["mHDRMode"]?.Value<string>() ?? string.Empty; // HDR Display，0关闭，1开启。
            var vSyncCount = systemQuality["vSyncCount"]?.Value<string>() ?? string.Empty; // 垂直同步，0关闭，1开启。
            var motionBlurEnabled = systemQuality["motionBlurEnabled"]?.Value<string>() ?? string.Empty; // 运动模糊，true开启，false关闭。
            var upSamplingType = systemQuality["upSamplingType"]?.Value<string>() ?? string.Empty; // 图像增强，1到3，对应不同技术。
            var enbaleFSR3FrameInterpolation = systemQuality["enbaleFSR3FrameInterpolation"]?.Value<string>() ?? string.Empty; // AMD 插帧，true开启，false关闭。
            var dlssMode = systemQuality["dlssMode"]?.Value<string>() ?? string.Empty; // NVIDIA DLSS，-2自动，2质量，1平衡，0性能，3超级性能。
            var reflexMode = systemQuality["reflexMode"]?.Value<string>() ?? string.Empty; ; // NVIDIA Reflex，0关闭，1Reflex，2Reflex+Boost。

            return new GameSettingsCollection(GameName)
            {
                GameSettings = new List<GameSetting>()
                {
                    new GameSetting()
                    {
                        CategoryName = "画面质量设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            // 模型精度
                            new GameSetting.Setting()
                            {
                                InnerName = "modelQualityLevel",
                                Name = "模型精度",
                                Display = modelQualityLevel switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    _ => "未知", // 默认情况
                                },
                                Value = modelQualityLevel,
                                Type = modelQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 细分曲面
                            new GameSetting.Setting()
                            {
                                InnerName = "tessellationQualityLevel",
                                Name = "细分曲面",
                                Display = tessellationQualityLevel switch
                                {
                                    "0" => "关闭",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    _ => "未知",
                                },
                                Value = tessellationQualityLevel,
                                Type = tessellationQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 特效品质
                            new GameSetting.Setting()
                            {
                                InnerName = "visualEffectsQualityLevel",
                                Name = "特效品质",
                                Display = visualEffectsQualityLevel switch
                                {
                                    "0" => "低",
                                    "1" => "中",
                                    "2" => "高",
                                    "3" => "极高",
                                    _ => "未知",
                                },
                                Value = visualEffectsQualityLevel,
                                Type = visualEffectsQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 贴图质量
                            new GameSetting.Setting()
                            {
                                InnerName = "textureQualityLevel",
                                Name = "贴图质量",
                                Display = textureQualityLevel switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    _ => "未知",
                                },
                                Value = textureQualityLevel,
                                Type = textureQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 阴影质量
                            new GameSetting.Setting()
                            {
                                InnerName = "shadowQualityLevel",
                                Name = "阴影质量",
                                Display = shadowQualityLevel switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = shadowQualityLevel,
                                Type = shadowQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 体积光质量
                            new GameSetting.Setting()
                            {
                                InnerName = "volumetricLightLevel",
                                Name = "体积光质量",
                                Display = volumetricLightLevel switch
                                {
                                    "0" => "低",
                                    "1" => "中",
                                    "2" => "高",
                                    "3" => "极高",
                                    _ => "未知",
                                },
                                Value = volumetricLightLevel,
                                Type = volumetricLightLevel.GetType().Name,
                                Unit = ""
                            },
                            // 体积云质量
                            new GameSetting.Setting()
                            {
                                InnerName = "cloudQualityLevel",
                                Name = "体积云质量",
                                Display = cloudQualityLevel switch
                                {
                                    "0" => "关闭",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = cloudQualityLevel,
                                Type = cloudQualityLevel.GetType().Name,
                                Unit = ""
                            },
                            // 环境光遮蔽
                            new GameSetting.Setting()
                            {
                                InnerName = "aoLevel",
                                Name = "环境光遮蔽",
                                Display = aoLevel switch
                                {
                                    "0" => "关闭",
                                    "1" => "低",
                                    "2" => "高",
                                    _ => "未知",
                                },
                                Value = aoLevel,
                                Type = aoLevel.GetType().Name,
                                Unit = ""
                            },
                            // 屏幕空间反射
                            new GameSetting.Setting()
                            {
                                InnerName = "SSRLevel",
                                Name = "屏幕空间反射",
                                Display = SSRLevel switch
                                {
                                    "0" => "关闭",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = SSRLevel,
                                Type = SSRLevel.GetType().Name,
                                Unit = ""
                            },
                            // 抗锯齿
                            new GameSetting.Setting()
                            {
                                InnerName = "AALevel",
                                Name = "抗锯齿",
                                Display = AALevel switch
                                {
                                    "0" => "低",
                                    "1" => "中",
                                    "2" => "高",
                                    _ => "未知",
                                },
                                Value = AALevel,
                                Type = AALevel.GetType().Name,
                                Unit = ""
                            },
                            // 后处理质量
                            new GameSetting.Setting()
                            {
                                InnerName = "PostProcessingLevel",
                                Name = "后处理质量",
                                Display = PostProcessingLevel switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中",
                                    "3" => "高",
                                    _ => "未知",
                                },
                                Value = PostProcessingLevel,
                                Type = PostProcessingLevel.GetType().Name,
                                Unit = ""
                            },
                            // 光照质量
                            new GameSetting.Setting()
                            {
                                InnerName = "LightingQualityLevel",
                                Name = "光照质量",
                                Display = LightingQualityLevel switch
                                {
                                    "0" => "低",
                                    "1" => "中",
                                    "2" => "高",
                                    "3" => "极高",
                                    _ => "未知",
                                },
                                Value = LightingQualityLevel,
                                Type = LightingQualityLevel.GetType().Name,
                                Unit = ""
                            }
                        }
                    },

                    new GameSetting()
                    {
                        CategoryName = "系统与显示设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            // 渲染比例
                            new GameSetting.Setting()
                            {
                                InnerName = "renderScale",
                                Name = "渲染比例",
                                Display = $"{(int)(renderScale * 100)}",
                                Value = renderScale.ToString("0.##"),
                                Type = renderScale.GetType().Name,
                                Unit = "%"
                            },
                            // 抗锯齿算法
                            new GameSetting.Setting()
                            {
                                InnerName = "aaMode",
                                Name = "抗锯齿算法",
                                Display = aaMode switch
                                {
                                    "0" => "关闭",
                                    "1" => "TAA",
                                    "2" => "SMAA",
                                    _ => "未知",
                                },
                                Value = aaMode,
                                Type = aaMode.GetType().Name,
                                Unit = ""
                            },
                            // 画面模式
                            new GameSetting.Setting()
                            {
                                InnerName = "fullScreenMode",
                                Name = "画面模式",
                                Display = fullScreenMode switch
                                {
                                    "0" => "全屏",
                                    "1" => "无边框窗口",
                                    "2" => "窗口化",
                                    _ => "未知",
                                },
                                Value = fullScreenMode,
                                Type = fullScreenMode.GetType().Name,
                                Unit = ""
                            },
                            // 分辨率宽度
                            new GameSetting.Setting()
                            {
                                InnerName = "resolutionWidth",
                                Name = "分辨率宽度",
                                Display = resolutionWidth,
                                Value = resolutionWidth,
                                Type = resolutionWidth.GetType().Name,
                                Unit = "像素"
                            },
                            // 分辨率高度
                            new GameSetting.Setting()
                            {
                                InnerName = "resolutionHeight",
                                Name = "分辨率高度",
                                Display = resolutionHeight,
                                Value = resolutionHeight,
                                Type = resolutionHeight.GetType().Name,
                                Unit = "像素"
                            },
                            // 帧数上限
                            new GameSetting.Setting()
                            {
                                InnerName = "frameRateLimit",
                                Name = "帧数上限",
                                Display = frameRateLimit switch
                                {
                                    "-1" => "无限制",
                                    _ => $"{frameRateLimit} FPS",
                                },
                                Value = frameRateLimit,
                                Type = frameRateLimit.GetType().Name,
                                Unit = "FPS"
                            },
                            // 画面风格
                            new GameSetting.Setting()
                            {
                                InnerName = "styleMode",
                                Name = "画面风格",
                                Display = styleMode switch
                                {
                                    "0" => "默认",
                                    "1" => "湖光",
                                    "2" => "柔脂",
                                    "3" => "幽谭",
                                    "4" => "莎草",
                                    _ => "未知",
                                },
                                Value = styleMode,
                                Type = styleMode.GetType().Name,
                                Unit = ""
                            },
                            // HDR Display
                            new GameSetting.Setting()
                            {
                                InnerName = "mHDRMode",
                                Name = "HDR显示",
                                Display = mHDRMode switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = mHDRMode,
                                Type = mHDRMode.GetType().Name,
                                Unit = ""
                            },
                            // 垂直同步
                            new GameSetting.Setting()
                            {
                                InnerName = "vSyncCount",
                                Name = "垂直同步",
                                Display = vSyncCount switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = vSyncCount,
                                Type = vSyncCount.GetType().Name,
                                Unit = ""
                            },
                            // 运动模糊
                            new GameSetting.Setting()
                            {
                                InnerName = "motionBlurEnabled",
                                Name = "运动模糊",
                                Display = motionBlurEnabled.ToLower() switch
                                {
                                    "true" => "开启",
                                    "false" => "关闭",
                                    _ => "未知",
                                },
                                Value = motionBlurEnabled,
                                Type = motionBlurEnabled.GetType().Name,
                                Unit = ""
                            },
                            // 图像增强
                            new GameSetting.Setting()
                            {
                                InnerName = "upSamplingType",
                                Name = "图像增强",
                                Display = upSamplingType switch
                                {
                                    "0" => "关闭",
                                    "1" => "NVIDIA DLSS",
                                    "3" => "AMD FSR2",
                                    "4" => "NVIDIA NIS",
                                    _ => "未知",
                                },
                                Value = upSamplingType,
                                Type = upSamplingType.GetType().Name,
                                Unit = ""
                            },
                            // AMD 插帧
                            new GameSetting.Setting()
                            {
                                InnerName = "enbaleFSR3FrameInterpolation",
                                Name = "AMD 插帧",
                                Display = enbaleFSR3FrameInterpolation.ToLower() switch
                                {
                                    "true" => "开启",
                                    "false" => "关闭",
                                    _ => "未知",
                                },
                                Value = enbaleFSR3FrameInterpolation,
                                Type = enbaleFSR3FrameInterpolation.GetType().Name,
                                Unit = ""
                            },
                            // NVIDIA DLSS
                            new GameSetting.Setting()
                            {
                                InnerName = "dlssMode",
                                Name = "NVIDIA DLSS",
                                Display = dlssMode switch
                                {
                                    "-2" => "自动",
                                    "2" => "质量",
                                    "1" => "平衡",
                                    "0" => "性能",
                                    "3" => "超级性能",
                                    _ => "未知",
                                },
                                Value = dlssMode,
                                Type = dlssMode.GetType().Name,
                                Unit = ""
                            },
                            // NVIDIA Reflex
                            new GameSetting.Setting()
                            {
                                InnerName = "reflexMode",
                                Name = "NVIDIA Reflex",
                                Display = reflexMode switch
                                {
                                    "0" => "关闭",
                                    "1" => "Reflex",
                                    "2" => "Reflex+Boost",
                                    _ => "未知",
                                },
                                Value = reflexMode,
                                Type = reflexMode.GetType().Name,
                                Unit = ""
                            }
                        }
                    },
                },
            };
        }
    }
}
