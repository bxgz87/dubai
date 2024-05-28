using GameConfigurations;
using System.Diagnostics;

namespace LeagueOfLegendsSetting
{
    public class LeagueOfLegends : IGameSetting
    {
        public string GameName => "英雄联盟";

        /// <summary>
        /// 获取游戏配置
        /// </summary>
        /// <param name="gameExecutablePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GameSettingsCollection GetGameSetting(string? data)
        {
            // 获取配置文件所在的文件夹
            var process = Process.GetProcessesByName("LeagueClient")?.FirstOrDefault();
            if (process == null)
                throw new Exception("未找到游戏进程");

            var path = ProcessHelper.GetProcessPath(process.Id);

            if (path == null)
                throw new Exception("未找到游戏路径");
            var folder = new DirectoryInfo(Path.GetDirectoryName(path))?.Parent?.FullName;
            if (string.IsNullOrEmpty(folder))
                throw new Exception($"未找到 {path} 文件夹");

            // 获取配置文件
            //var configFilePath = @"C:\Users\ywb\Desktop\游戏配置说明\英雄联盟\Game.cfg";
            var configFilePath = Path.Combine(folder, "Game\\Config\\Game.cfg");

            if (!File.Exists(configFilePath))
                throw new Exception($"{configFilePath} 不存在");

            // 游戏内/声音
            var masterVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "MasterVolume"); // 总音量调节
            var musicVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "MusicVolume"); // 音乐音量
            var announcerVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "AnnouncerVolume"); // 通报员音量
            var voiceVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "VoiceVolume"); // 通话声音调节
            var sfxVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "SfxVolume"); // 特殊音效音量调节
            var ambienceVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "AmbienceVolume"); // 环境音量
            var pingsVolume = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Volume", "PingsVolume"); // 信号音量

            // 游戏内/用户界面缩放
            var globalScale = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "GlobalScale"); // 用户界面缩放
            var chatScale = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "ChatScale"); // 聊天缩放
            var minimapScale = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "MinimapScale"); // 小地图缩放
            var objectiveVoteScale = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "ObjectiveVoteScale"); // 战略点备战缩放

            // 游戏内/显示和功能设置
            var drawHealthBars = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "DrawHealthBars") ?? string.Empty; // 显示生命槽
            var lossOfControlEnabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "LossOfControl", "LossOfControlEnabled") ?? string.Empty; // 显示失控界面
            var enableHUDAnimations = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "EnableHUDAnimations") ?? string.Empty; // 启用用户界面动画
            var showHealthBarShake = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowHealthBarShake") ?? string.Empty; // 显示生命槽抖动
            var showSummonerNames = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowSummonerNames") ?? string.Empty; // 在生命槽上方显示名字
            var flashScreenWhenDamaged = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "FlashScreenWhenDamaged") ?? string.Empty; // 受伤时屏幕闪烁
            var showGodray = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "ShowGodray") ?? string.Empty; // 在镜头中央高亮显示英雄
            var flashScreenWhenStunned = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "FlashScreenWhenStunned") ?? string.Empty; // 失控时屏幕闪烁
            var showOffScreenPointsOfInterest = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowOffScreenPointsOfInterest") ?? string.Empty; // 显示屏幕事件信号
            var autoDisplayTarget = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "AutoDisplayTarget") ?? string.Empty; // 攻击时显示目标框架
            var enableLineMissileVis = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "EnableLineMissileVis") ?? string.Empty; // 启用线状弹道显示
            var showAttackRadius = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowAttackRadius") ?? string.Empty; // 显示攻击距离
            var disableHudSpellClick = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "DisableHudSpellClick") ?? string.Empty; // 只能使用热键释放技能
            var showSpellCosts = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowSpellCosts") ?? string.Empty; // 显示技能消耗
            var showSpellRecommendations = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowSpellRecommendations") ?? string.Empty; // 显示推荐技能加点
            var numericCooldownFormat = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "NumericCooldownFormat") ?? string.Empty; // 技能冷却显示
            var showNeutralCamps = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowNeutralCamps") ?? string.Empty; // 显示野怪营地
            var flipMiniMap = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "FlipMiniMap") ?? string.Empty; // 左侧显示小地图
            var minimapMoveSelf = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "MinimapMoveSelf") ?? string.Empty; // 允许小地图移动
            var mirroredScoreboard = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "MirroredScoreboard") ?? string.Empty; // 记分板使用镜像布局
            var showSummonerNamesInScoreboard = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowSummonerNamesInScoreboard") ?? string.Empty; // 显示玩家名称
            var showTeamFramesOnLeft = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowTeamFramesOnLeft") ?? string.Empty; // 将团队框架显示在左侧
            var showTimestamps = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ShowTimestamps") ?? string.Empty; // 显示时间戳
            var chatChannelVisibility = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ChatChannelVisibility") ?? string.Empty; // 改变聊天可见度
            var emotePopupUIDisplayMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "EmotePopupUIDisplayMode") ?? string.Empty; // 表情气泡显示
            var emoteSize = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "EmoteSize") ?? string.Empty; // 表情规格
            var hideReciprocityFist = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "HideReciprocityFist") ?? string.Empty; // 静音击拳庆祝
            var eternalsMilestoneDisplayMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "EternalsMilestoneDisplayMode") ?? string.Empty; // 永恒星碑里程点显示
            var damage_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Damage_Enabled") ?? string.Empty; // 伤害
            var invulnerable_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Invulnerable_Enabled") ?? string.Empty; // 状态
            var heal_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Heal_Enabled") ?? string.Empty; // 治疗
            var gold_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Gold_Enabled") ?? string.Empty; // 金币
            var questReceived_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "QuestReceived_Enabled") ?? string.Empty; // 任务
            var manaDamage_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "ManaDamage_Enabled") ?? string.Empty; // 法力值
            var enemyDamage_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "EnemyDamage_Enabled") ?? string.Empty; // 敌方伤害
            var dodge_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Dodge_Enabled") ?? string.Empty; // 躲闪
            var level_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Level_Enabled") ?? string.Empty; // 等级
            var special_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Special_Enabled") ?? string.Empty; // 特殊
            var score_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Score_Enabled") ?? string.Empty; // 积分
            var experience_Enabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "FloatingText", "Experience_Enabled") ?? string.Empty; // 经验值

            // 游戏内/游戏设置
            var preferDX9LegacyMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "PreferDX9LegacyMode") ?? string.Empty; // 偏好DX9传统模式
            var gameMouseSpeed = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "General", "GameMouseSpeed"); // 鼠标速度
            var mapScrollSpeed = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "MapScrollSpeed"); // 镜头移动速度（鼠标）
            var keyboardScrollSpeed = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "HUD", "KeyboardScrollSpeed"); // 镜头移动速度（键盘）
            var snapCameraOnRespawn = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "SnapCameraOnRespawn") ?? string.Empty; // 复活时移动镜头
            var scrollSmoothingEnabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "ScrollSmoothingEnabled") ?? string.Empty; // 启动镜头平滑
            var middleClickDragScrollEnabled = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "MiddleClickDragScrollEnabled") ?? string.Empty; // 按住鼠标拖拽滚屏
            var cameraLockMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "HUD", "CameraLockMode") ?? string.Empty; // 镜头锁定模式
            var autoAcquireTarget = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "AutoAcquireTarget") ?? string.Empty; // 自动攻击
            var predictMovement = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "PredictMovement") ?? string.Empty; // 使用移动侦测
            var showTurretRangeIndicators = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "ShowTurretRangeIndicators") ?? string.Empty; // 显示防御塔射程指示器
            var enableTargetedAttackMove = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "EnableTargetedAttackMove") ?? string.Empty; // 依据鼠标指针攻击移动
            var recommendJunglePaths = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "RecommendJunglePaths") ?? string.Empty; // 显示推荐打野路线

            // 游戏内/图形设置
            var shadowQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "ShadowQuality") ?? string.Empty; // 阴影品质
            var environmentQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "EnvironmentQuality") ?? string.Empty; // 环境品质
            var characterQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "CharacterQuality") ?? string.Empty; // 人物细节品质
            var effectsQuality = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "EffectsQuality") ?? string.Empty; // 特效品质

            // 游戏内/显示设置
            var width = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "Width") ?? string.Empty; // 分辨率宽度
            var height = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "Height") ?? string.Empty; // 分辨率高度
            var windowMode = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "WindowMode") ?? string.Empty; // 窗口模式

            // 游戏内/高级显示设置
            var colorPalette = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "ColorPalette", "ColorPalette") ?? string.Empty; // 色盲模式
            var hideEyeCandy = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "HideEyeCandy") ?? string.Empty; // 隐藏地图细节特效
            var enableScreenShake = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "EnableScreenShake") ?? string.Empty; // 启用屏幕抖动
            var relativeTeamColors = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "RelativeTeamColors") ?? string.Empty; // 使用相关联的队伍颜色
            var frameCapType = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "FrameCapType") ?? string.Empty; // 帧率上限
            var enableFXAA = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "Performance", "EnableFXAA") ?? string.Empty; // 抗锯齿
            var waitForVerticalSync = IniOperation.InIHelper.ReadConfig<string>(configFilePath, "General", "WaitForVerticalSync") ?? string.Empty; // 等待垂直同步

            // 游戏内/色彩设置
            var colorLevel = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Accessibility", "ColorLevel"); // 色彩层次
            var colorGamma = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Accessibility", "ColorGamma"); // 色彩伽马值
            var colorBrightness = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Accessibility", "ColorBrightness"); // 色彩亮度
            var colorContrast = IniOperation.InIHelper.ReadConfig<float>(configFilePath, "Accessibility", "ColorContrast"); // 色彩对比度

            return new GameSettingsCollection(GameName) 
            {
                GameSettings = new List<GameSetting>()
                {
                    new GameSetting
                    {
                        CategoryName = "游戏内/声音",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "masterVolume",
                                Name = "总音量",
                                Display = ((int)(masterVolume * 100)).ToString(),
                                Value = masterVolume.ToString(),
                                Type = masterVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "musicVolume",
                                Name = "音乐音量",
                                Display = ((int)(musicVolume * 100)).ToString(),
                                Value = musicVolume.ToString(),
                                Type = musicVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "announcerVolume",
                                Name = "通报员音量",
                                Display = ((int)(announcerVolume * 100)).ToString(),
                                Value = announcerVolume.ToString(),
                                Type = announcerVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "voiceVolume",
                                Name = "通话声音调节",
                                Display = ((int)(voiceVolume * 100)).ToString(),
                                Value = voiceVolume.ToString(),
                                Type = voiceVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "sfxVolume",
                                Name = "特殊音效音量调节",
                                Display = ((int)(sfxVolume * 100)).ToString(),
                                Value = sfxVolume.ToString(),
                                Type = sfxVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "ambienceVolume",
                                Name = "环境音量",
                                Display = ((int)(ambienceVolume * 100)).ToString(),
                                Value = ambienceVolume.ToString(),
                                Type = ambienceVolume.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "pingsVolume",
                                Name = "信号音量",
                                Display = ((int)(pingsVolume * 100)).ToString(),
                                Value = pingsVolume.ToString(),
                                Type = pingsVolume.GetType().Name,
                                Unit = "%"
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/用户界面缩放",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "globalScale",
                                Name = "用户界面缩放",
                                Display = ((int)(globalScale * 100)).ToString(),
                                Value = globalScale.ToString(),
                                Type = globalScale.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "chatScale",
                                Name = "聊天缩放",
                                Display = ((int)(chatScale)).ToString(),
                                Value = chatScale.ToString(),
                                Type = chatScale.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "minimapScale",
                                Name = "小地图缩放",
                                Display = ((int)(minimapScale * 100)).ToString(),
                                Value = minimapScale.ToString(),
                                Type = minimapScale.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "objectiveVoteScale",
                                Name = "战略点备战缩放",
                                Display = ((int)(objectiveVoteScale * 100)).ToString(),
                                Value = objectiveVoteScale.ToString(),
                                Type = objectiveVoteScale.GetType().Name,
                                Unit = "%"
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/显示和功能设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "drawHealthBars",
                                Name = "显示生命槽",
                                Display = drawHealthBars switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = drawHealthBars,
                                Type = drawHealthBars.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "lossOfControlEnabled",
                                Name = "显示失控界面",
                                Display = lossOfControlEnabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = lossOfControlEnabled,
                                Type = lossOfControlEnabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enableHUDAnimations",
                                Name = "启用用户界面动画",
                                Display = enableHUDAnimations switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enableHUDAnimations,
                                Type = enableHUDAnimations.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showHealthBarShake",
                                Name = "显示生命槽抖动",
                                Display = showHealthBarShake switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showHealthBarShake,
                                Type = showHealthBarShake.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showSummonerNames",
                                Name = "在生命槽上方显示名字",
                                Display = showSummonerNames switch
                                {
                                    "0" => "无",
                                    "1" => "玩家名称",
                                    "2" => "英雄名",
                                    _ => "未知",
                                },
                                Value = showSummonerNames,
                                Type = showSummonerNames.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "flashScreenWhenDamaged",
                                Name = "受伤时屏幕闪烁",
                                Display = flashScreenWhenDamaged switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = flashScreenWhenDamaged,
                                Type = flashScreenWhenDamaged.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showGodray",
                                Name = "在镜头中央高亮显示英雄",
                                Display = showGodray switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showGodray,
                                Type = showGodray.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "flashScreenWhenStunned",
                                Name = "失控时屏幕闪烁",
                                Display = flashScreenWhenStunned switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = flashScreenWhenStunned,
                                Type = flashScreenWhenStunned.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showOffScreenPointsOfInterest",
                                Name = "显示屏幕事件信号",
                                Display = showOffScreenPointsOfInterest switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showOffScreenPointsOfInterest,
                                Type = showOffScreenPointsOfInterest.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "autoDisplayTarget",
                                Name = "攻击时显示目标框架",
                                Display = autoDisplayTarget switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = autoDisplayTarget,
                                Type = autoDisplayTarget.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enableLineMissileVis",
                                Name = "启用线状弹道显示",
                                Display = enableLineMissileVis switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enableLineMissileVis,
                                Type = enableLineMissileVis.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showAttackRadius",
                                Name = "显示攻击距离",
                                Display = showAttackRadius switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showAttackRadius,
                                Type = showAttackRadius.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "disableHudSpellClick",
                                Name = "只能使用热键释放技能",
                                Display = disableHudSpellClick switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = disableHudSpellClick,
                                Type = disableHudSpellClick.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showSpellCosts",
                                Name = "显示技能消耗",
                                Display = showSpellCosts switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showSpellCosts,
                                Type = showSpellCosts.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showSpellRecommendations",
                                Name = "显示推荐技能加点",
                                Display = showSpellRecommendations switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showSpellRecommendations,
                                Type = showSpellRecommendations.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "numericCooldownFormat",
                                Name = "技能冷却显示",
                                Display = numericCooldownFormat switch
                                {
                                    "0" => "无",
                                    "1" => "秒",
                                    "2" => "分钟+秒",
                                    "3" => "分钟",
                                    _ => "未知",
                                },
                                Value = numericCooldownFormat,
                                Type = numericCooldownFormat.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showNeutralCamps",
                                Name = "显示野怪营地",
                                Display = showNeutralCamps switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showNeutralCamps,
                                Type = showNeutralCamps.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "flipMiniMap",
                                Name = "左侧显示小地图",
                                Display = flipMiniMap switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = flipMiniMap,
                                Type = flipMiniMap.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "minimapMoveSelf",
                                Name = "允许小地图移动",
                                Display = minimapMoveSelf switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = minimapMoveSelf,
                                Type = minimapMoveSelf.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "mirroredScoreboard",
                                Name = "记分板使用镜像布局",
                                Display = mirroredScoreboard switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = mirroredScoreboard,
                                Type = mirroredScoreboard.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showSummonerNamesInScoreboard",
                                Name = "显示玩家名称",
                                Display = showSummonerNamesInScoreboard switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showSummonerNamesInScoreboard,
                                Type = showSummonerNamesInScoreboard.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showTeamFramesOnLeft",
                                Name = "将团队框架显示在左侧",
                                Display = showTeamFramesOnLeft switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showTeamFramesOnLeft,
                                Type = showTeamFramesOnLeft.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showTimestamps",
                                Name = "显示时间戳",
                                Display = showTimestamps switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showTimestamps,
                                Type = showTimestamps.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "chatChannelVisibility",
                                Name = "改变聊天可见度",
                                Display = chatChannelVisibility switch
                                {
                                    "0" => "仅预组队",
                                    "1" => "相同队伍",
                                    "2" => "每个人",
                                    _ => "未知",
                                },
                                Value = chatChannelVisibility,
                                Type = chatChannelVisibility.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "emotePopupUIDisplayMode",
                                Name = "表情气泡显示",
                                Display = emotePopupUIDisplayMode switch
                                {
                                    "0" => "开",
                                    "1" => "静音特效",
                                    "2" => "关",
                                    _ => "未知",
                                },
                                Value = emotePopupUIDisplayMode,
                                Type = emotePopupUIDisplayMode.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "emoteSize",
                                Name = "表情规格",
                                Display = emoteSize switch
                                {
                                    "0" => "常规",
                                    "1" => "小型",
                                    _ => "未知",
                                },
                                Value = emoteSize,
                                Type = emoteSize.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "hideReciprocityFist",
                                Name = "静音击拳庆祝",
                                Display = hideReciprocityFist switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = hideReciprocityFist,
                                Type = hideReciprocityFist.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "eternalsMilestoneDisplayMode",
                                Name = "永恒星碑里程点显示",
                                Display = eternalsMilestoneDisplayMode switch
                                {
                                    "0" => "全部",
                                    "1" => "仅限自己和对外",
                                    "2" => "无",
                                    _ => "未知",
                                },
                                Value = eternalsMilestoneDisplayMode,
                                Type = eternalsMilestoneDisplayMode.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "damage_Enabled",
                                Name = "伤害",
                                Display = damage_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = damage_Enabled,
                                Type = damage_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "invulnerable_Enabled",
                                Name = "状态",
                                Display = invulnerable_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = invulnerable_Enabled,
                                Type = invulnerable_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "heal_Enabled",
                                Name = "治疗",
                                Display = heal_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = heal_Enabled,
                                Type = heal_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "gold_Enabled",
                                Name = "金币",
                                Display = gold_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = gold_Enabled,
                                Type = gold_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "questReceived_Enabled",
                                Name = "任务",
                                Display = questReceived_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = questReceived_Enabled,
                                Type = questReceived_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "manaDamage_Enabled",
                                Name = "法力值",
                                Display = manaDamage_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = manaDamage_Enabled,
                                Type = manaDamage_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enemyDamage_Enabled",
                                Name = "敌方伤害",
                                Display = enemyDamage_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enemyDamage_Enabled,
                                Type = enemyDamage_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "dodge_Enabled",
                                Name = "躲闪",
                                Display = dodge_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = dodge_Enabled,
                                Type = dodge_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "level_Enabled",
                                Name = "等级",
                                Display = level_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = level_Enabled,
                                Type = level_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "special_Enabled",
                                Name = "特殊",
                                Display = special_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = special_Enabled,
                                Type = special_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "score_Enabled",
                                Name = "积分",
                                Display = score_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = score_Enabled,
                                Type = score_Enabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "experience_Enabled",
                                Name = "经验值",
                                Display = experience_Enabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = experience_Enabled,
                                Type = experience_Enabled.GetType().Name,
                                Unit = ""
                            }
                        }

                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/游戏设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "preferDX9LegacyMode",
                                Name = "偏好DX9传统模式",
                                Display = preferDX9LegacyMode switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = preferDX9LegacyMode,
                                Type = preferDX9LegacyMode.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "gameMouseSpeed",
                                Name = "鼠标速度",
                                Display = $"{(int)((gameMouseSpeed/20d) * 100d)}",
                                Value = gameMouseSpeed.ToString("0.##"),
                                Type = gameMouseSpeed.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "mapScrollSpeed",
                                Name = "镜头移动速度（鼠标）",
                                Display = $"{(int)(mapScrollSpeed * 100)}",
                                Value = mapScrollSpeed.ToString("0.##"),
                                Type = mapScrollSpeed.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "keyboardScrollSpeed",
                                Name = "镜头移动速度（键盘）",
                                Display = $"{(int)(keyboardScrollSpeed * 100)}",
                                Value = keyboardScrollSpeed.ToString("0.##"),
                                Type = keyboardScrollSpeed.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "snapCameraOnRespawn",
                                Name = "复活时移动镜头",
                                Display = snapCameraOnRespawn switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = snapCameraOnRespawn,
                                Type = snapCameraOnRespawn.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "scrollSmoothingEnabled",
                                Name = "启动镜头平滑",
                                Display = scrollSmoothingEnabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = scrollSmoothingEnabled,
                                Type = scrollSmoothingEnabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "middleClickDragScrollEnabled",
                                Name = "按住鼠标拖拽滚屏",
                                Display = middleClickDragScrollEnabled switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = middleClickDragScrollEnabled,
                                Type = middleClickDragScrollEnabled.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "cameraLockMode",
                                Name = "镜头锁定模式",
                                Display = cameraLockMode switch
                                {
                                    "0" => "基于红/蓝方的镜头偏移",
                                    "1" => "固定的镜头偏移",
                                    "2" => "半锁定",
                                    _ => "未知",
                                },
                                Value = cameraLockMode,
                                Type = cameraLockMode.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "autoAcquireTarget",
                                Name = "自动攻击",
                                Display = autoAcquireTarget switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = autoAcquireTarget,
                                Type = autoAcquireTarget.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "predictMovement",
                                Name = "使用移动侦测",
                                Display = predictMovement switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = predictMovement,
                                Type = predictMovement.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "showTurretRangeIndicators",
                                Name = "显示防御塔射程指示器",
                                Display = showTurretRangeIndicators switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = showTurretRangeIndicators,
                                Type = showTurretRangeIndicators.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enableTargetedAttackMove",
                                Name = "依据鼠标指针攻击移动",
                                Display = enableTargetedAttackMove switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enableTargetedAttackMove,
                                Type = enableTargetedAttackMove.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "recommendJunglePaths",
                                Name = "显示推荐打野路线",
                                Display = recommendJunglePaths switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = recommendJunglePaths,
                                Type = recommendJunglePaths.GetType().Name,
                                Unit = ""
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/图形设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "shadowQuality",
                                Name = "阴影品质",
                                Display = shadowQuality switch
                                {
                                    "0" => "关",
                                    "1" => "低",
                                    "2" => "中等",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = shadowQuality,
                                Type = shadowQuality.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "environmentQuality",
                                Name = "环境品质",
                                Display = environmentQuality switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中等",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = environmentQuality,
                                Type = environmentQuality.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "characterQuality",
                                Name = "人物细节品质",
                                Display = characterQuality switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中等",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = characterQuality,
                                Type = characterQuality.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "effectsQuality",
                                Name = "特效品质",
                                Display = effectsQuality switch
                                {
                                    "0" => "极低",
                                    "1" => "低",
                                    "2" => "中等",
                                    "3" => "高",
                                    "4" => "极高",
                                    _ => "未知",
                                },
                                Value = effectsQuality,
                                Type = effectsQuality.GetType().Name,
                                Unit = ""
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/显示设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "width",
                                Name = "分辨率宽度",
                                Display = width,
                                Value = width,
                                Type = width.GetType().Name,
                                Unit = "像素"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "height",
                                Name = "分辨率高度",
                                Display = height,
                                Value = height,
                                Type = height.GetType().Name,
                                Unit = "像素"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "windowMode",
                                Name = "窗口模式",
                                Display = windowMode switch
                                {
                                    "0" => "全屏",
                                    "1" => "窗口模式",
                                    "2" => "无边框模式",
                                    _ => "未知",
                                },
                                Value = windowMode,
                                Type = windowMode.GetType().Name,
                                Unit = ""
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/高级显示设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "colorPalette",
                                Name = "色盲模式",
                                Display = colorPalette switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = colorPalette,
                                Type = colorPalette.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "hideEyeCandy",
                                Name = "隐藏地图细节特效",
                                Display = hideEyeCandy switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = hideEyeCandy,
                                Type = hideEyeCandy.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enableScreenShake",
                                Name = "启用屏幕抖动",
                                Display = enableScreenShake switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enableScreenShake,
                                Type = enableScreenShake.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "relativeTeamColors",
                                Name = "使用相关联的队伍颜色",
                                Display = relativeTeamColors switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = relativeTeamColors,
                                Type = relativeTeamColors.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "frameCapType",
                                Name = "帧率上限",
                                Display = frameCapType switch
                                {
                                    "3" => "25FPS",
                                    "4" => "30FPS",
                                    "5" => "60FPS",
                                    "6" => "80FPS",
                                    "7" => "120FPS",
                                    "8" => "144FPS",
                                    "9" => "200FPS",
                                    "2" => "240FPS",
                                    "10" => "不封顶",
                                    _ => "未知",
                                },
                                Value = frameCapType,
                                Type = frameCapType.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "enableFXAA",
                                Name = "抗锯齿",
                                Display = enableFXAA switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = enableFXAA,
                                Type = enableFXAA.GetType().Name,
                                Unit = ""
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "waitForVerticalSync",
                                Name = "等待垂直同步",
                                Display = waitForVerticalSync switch
                                {
                                    "0" => "关闭",
                                    "1" => "开启",
                                    _ => "未知",
                                },
                                Value = waitForVerticalSync,
                                Type = waitForVerticalSync.GetType().Name,
                                Unit = ""
                            }
                        }
                    },

                    new GameSetting
                    {
                        CategoryName = "游戏内/色彩设置",
                        Settings = new List<GameSetting.Setting>()
                        {
                            new GameSetting.Setting()
                            {
                                InnerName = "colorLevel",
                                Name = "色彩层次",
                                Display = $"{(int)(colorLevel * 100)}",
                                Value = colorLevel.ToString("0.##"),
                                Type = colorLevel.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "colorGamma",
                                Name = "色彩伽马值",
                                Display = $"{(int)(colorGamma * 100)}",
                                Value = colorGamma.ToString("0.##"),
                                Type = colorGamma.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "colorBrightness",
                                Name = "色彩亮度",
                                Display = $"{(int)(colorBrightness * 100)}",
                                Value = colorBrightness.ToString("0.##"),
                                Type = colorBrightness.GetType().Name,
                                Unit = "%"
                            },
                            new GameSetting.Setting()
                            {
                                InnerName = "colorContrast",
                                Name = "色彩对比度",
                                Display = $"{(int)(colorContrast * 100)}",
                                Value = colorContrast.ToString("0.##"),
                                Type = colorContrast.GetType().Name,
                                Unit = "%"
                            }
                        }
                    },
                }
            };
        }
    }
}