using System;
using System.Management;

namespace SmartHome
{
    // Объект для мониторинга сервера "Умного дома"
    public class Monitoring
    {
        
//===============================================================================================================
// Name...........:	System
// Description....:	Мониторинг системных ресурсов
// Syntax.........:	System()
//===============================================================================================================
        public static void System()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
                ManagementObjectCollection mObject = searcher.Get();
                foreach (var item in mObject)
                {
                    var obj = (ManagementObject)item;
                    Sensors.SaveToDatabase("cpu_load", obj["LoadPercentage"].ToString());
                }
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_OperatingSystem");
                ManagementObjectCollection mObject = searcher.Get();
                foreach (var item in mObject)
                {
                    var obj = (ManagementObject)item;
                    Sensors.SaveToDatabase("ram_free", obj["TotalVisibleMemorySize"].ToString());
                }
            }
            catch (Exception)
            {
                // ignored
            }
        } // void System()

    } // class
} // namespace SmartHome
