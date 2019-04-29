using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace SmartHome
{
    // Объект для работы с датчиками "Умного дрма"
    public class Sensors
    {

        public class Sensor         // Запись о датчике
        {
            public uint Id;         // ID датчика
            public String Topic;    // Топик MQTT
            public String Name;     // Наименование датчика
            public String Value;    // Показания датчика
            public int InList;      // Индекс в таблице датчиков
        } // class Sensor
        public static List<Sensor> SensorsList;

//===============================================================================================================
// Name...........:	Sensors
// Description....:	Инициализация объекта
// Syntax.........:	new Sensors()
//===============================================================================================================
        public Sensors()
        {
            SensorsList = new List<Sensor>();
            LoadTable();
        } // Sensors()

//===============================================================================================================
// Name...........:	LoadTable
// Description....:	Чтение таблицы с информацией о датчиках из базы данных
// Syntax.........:	LoadTable()
//===============================================================================================================
        public static void LoadTable()
        {
            var table = MySql.ReadTable("sensors");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Sensor();
                if (!uint.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Topic = record[1];
                item.Name = record[2];
                item.InList = Program.AppWindow.GridViewSensors.Rows.Add(item.Name, item.Topic);
                SensorsList.Add(item);
            }
        } // void LoadTable()

//===============================================================================================================
// Name...........:	Find
// Description....:	Поиск датчика по имени
// Syntax.........:	Find(topic)
// Parameters.....:	topic       - идентификатор топика
// Return value(s):	Success:    - найденный экземпляр объекта типа Sensor
//                  Failure:    - null
//===============================================================================================================
        public static Sensor Find(String topic)
        {
            return SensorsList.FirstOrDefault(sensor => sensor.Topic == topic);
        } // Sensor Find(name)

//===============================================================================================================
// Name...........:	SystemMonitoring
// Description....:	Мониторинг системных ресурсов
// Syntax.........:	SystemMonitoring()
//===============================================================================================================
        public static void SystemMonitoring()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
            ManagementObjectCollection mObject = searcher.Get();
            foreach (var item in mObject)
            {
                var obj = (ManagementObject)item;
                SaveToDatabase("cpu_load", obj["LoadPercentage"].ToString());
            }
            searcher = new ManagementObjectSearcher("Select * FROM Win32_OperatingSystem");
            mObject = searcher.Get();
            foreach (var item in mObject)
            {
                var obj = (ManagementObject)item;
                SaveToDatabase("ram_free", obj["TotalVisibleMemorySize"].ToString());
            }
        } // void SystemMonitoring()

//===============================================================================================================
// Name...........:	SaveToDatabase
// Description....:	Сохранение показаний датчика в базу данных
// Syntax.........:	SaveToDatabase(topic, value)
// Parameters.....:	topic       - идентификатор топика
//                  value       - новое значение
//===============================================================================================================
        public static void SaveToDatabase(String topic, String value)
        {
            Sensor sensor = Find(topic);
            if (sensor == null) return;
            sensor.Value = value;
            SaveToDatabase(sensor);
        } // void SaveToDatabase(topic, value)

//===============================================================================================================
// Name...........:	SaveToDatabase
// Description....:	Сохранение показаний датчика в базу данных
// Syntax.........:	SaveToDatabase(sensor)
// Parameters.....:	sensor      - запись типа датчик
//===============================================================================================================
        public static void SaveToDatabase(Sensor sensor)
        {
            MySql.SaveTo("sensors_data", "sensor,value,status", "'" + sensor.Topic + "','" + sensor.Value + "','0'");
            MySql.SaveTo("sensors", "value", sensor.Value, "topic = '" + sensor.Topic + "'");
            Program.AppWindow.GridViewSensors[2, sensor.InList].Value = sensor.Value;
            Program.AppWindow.GridViewSensors[3, sensor.InList].Value = DateTime.Now.ToString("HH:mm:ss");
            Events.ChekScripts(sensor);
        } // void SaveToDatabase(sensor)

    } // class
} // namespace SmartHome
