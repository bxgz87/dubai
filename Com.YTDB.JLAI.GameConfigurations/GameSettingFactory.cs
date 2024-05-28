using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameConfigurations
{
    /// <summary>
    /// 提供创建游戏设置实例的工厂类。支持通过类型直接创建实例，或从DLL文件中加载并创建实例。
    /// </summary>
    public static class GameSettingFactory
    {
        /// <summary>
        /// 通过反射创建继承自IGameSetting的类的实例。
        /// </summary>
        /// <param name="dllPath">要加载的DLL文件的路径。</param>
        /// <returns>IGameSetting类型的对象实例，如果创建失败，则返回null。</returns>
        public static IGameSetting? CreateInstance(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                Console.WriteLine($"DLL文件不存在：{dllPath}");
                return null;
            }

            try
            {
                // 从文件路径加载程序集
                Assembly assembly = Assembly.LoadFrom(dllPath);

                foreach (Type type in assembly.GetTypes())
                {
                    // 检查类型是否为公开的类，是否实现了IGameSetting接口，且不是抽象类
                    if (type.IsClass && !type.IsAbstract && typeof(IGameSetting).IsAssignableFrom(type) && type.IsPublic)
                    {
                        // 可以进一步通过type.Namespace来判断是否在特定的命名空间下，如果需要的话
                        // if (type.Namespace == "YourDesiredNamespace")

                        // 使用反射创建实例
                        object? instance = Activator.CreateInstance(type);
                        if (instance is IGameSetting gameSetting)
                        {
                            return gameSetting;
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // 输出加载的类型失败的详细信息
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    Console.WriteLine(loaderException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载程序集或创建实例时发生错误：{ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 通过反射从指定的DLL文件中创建所有实现了IGameSetting接口的类的实例。
        /// </summary>
        /// <param name="dllPath">要加载的DLL文件的路径。</param>
        /// <returns>包含所有实现了IGameSetting接口的类实例的列表。如果没有找到任何匹配的实例或者发生错误，将返回一个空列表。</returns>
        public static List<IGameSetting> CreateInstances(string dllPath)
        {
            var instances = new List<IGameSetting>();

            if (!File.Exists(dllPath))
            {
                Console.WriteLine($"DLL文件不存在：{dllPath}");
                return instances;
            }

            try
            {
                // 从文件路径加载程序集
                Assembly assembly = Assembly.LoadFrom(dllPath);

                foreach (Type type in assembly.GetTypes())
                {
                    // 检查类型是否为公开的类，是否实现了IGameSetting接口，且不是抽象类
                    if (type.IsClass && !type.IsAbstract && typeof(IGameSetting).IsAssignableFrom(type) && type.IsPublic)
                    {
                        // 使用反射创建实例
                        object? instance = Activator.CreateInstance(type);
                        if (instance is IGameSetting gameSetting)
                        {
                            instances.Add(gameSetting);
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // 输出加载的类型失败的详细信息
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    Console.WriteLine(loaderException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载程序集或创建实例时发生错误：{ex.Message}");
            }

            return instances;
        }
    }
}