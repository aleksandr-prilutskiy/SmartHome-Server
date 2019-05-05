﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartHome.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartHome.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Система &apos;Умный дом&apos;
        ///Сервер обработки событий
        ///версия {0}
        ///
        ///(c) 2016-2019 Aleksandr Prilutskiy.
        /// </summary>
        internal static string AboutText {
            get {
                return ResourceManager.GetString("AboutText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка!
        ///Одна копия этого приложения уже запущена.
        ///Нельзя запускать несколько копий этой программы одновременно..
        /// </summary>
        internal static string AppAlreadyRunning {
            get {
                return ResourceManager.GetString("AppAlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SmartHome Server.
        /// </summary>
        internal static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на База данных подключена.
        /// </summary>
        internal static string DatabaseConnected {
            get {
                return ResourceManager.GetString("DatabaseConnected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Невозможно установить подключение к базе данных.
        /// </summary>
        internal static string DatabaseConnectedError {
            get {
                return ResourceManager.GetString("DatabaseConnectedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка: Невозможно подключиться к базе данных.
        ///.
        /// </summary>
        internal static string DatabaseConnectError {
            get {
                return ResourceManager.GetString("DatabaseConnectError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap gray {
            get {
                object obj = ResourceManager.GetObject("gray", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap green {
            get {
                object obj = ResourceManager.GetObject("green", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Невозможно создать файл журнала.
        /// </summary>
        internal static string LogFileCreateError {
            get {
                return ResourceManager.GetString("LogFileCreateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Error: .
        /// </summary>
        internal static string LogMsgError {
            get {
                return ResourceManager.GetString("LogMsgError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Event: .
        /// </summary>
        internal static string LogMsgEvent {
            get {
                return ResourceManager.GetString("LogMsgEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MQTT: .
        /// </summary>
        internal static string LogMsgMQTT {
            get {
                return ResourceManager.GetString("LogMsgMQTT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на nooLite: .
        /// </summary>
        internal static string LogMsgNooLite {
            get {
                return ResourceManager.GetString("LogMsgNooLite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ping: .
        /// </summary>
        internal static string LogMsgPing {
            get {
                return ResourceManager.GetString("LogMsgPing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Script event: .
        /// </summary>
        internal static string LogMsgScript {
            get {
                return ResourceManager.GetString("LogMsgScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Shedule event: .
        /// </summary>
        internal static string LogMsgShedule {
            get {
                return ResourceManager.GetString("LogMsgShedule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap logo {
            get {
                object obj = ResourceManager.GetObject("logo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка: ini-файл не найден.
        ///Невозможно запустить программу..
        /// </summary>
        internal static string NoIniFile {
            get {
                return ResourceManager.GetString("NoIniFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Сервер запущен.
        /// </summary>
        internal static string StartServer {
            get {
                return ResourceManager.GetString("StartServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Прекратить работу программы?.
        /// </summary>
        internal static string TerminateApp {
            get {
                return ResourceManager.GetString("TerminateApp", resourceCulture);
            }
        }
    }
}
