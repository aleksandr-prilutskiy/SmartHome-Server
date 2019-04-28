﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с базой данных MySQL
    class MySql
    {
        public static MySqlConnection Connection;
        private static bool _busy;

//===============================================================================================================
// Name...........:	MySql
// Description....:	Подключение к базе данных
// Syntax.........:	new MySql()
//===============================================================================================================
        public MySql()
        {
            Connection = null;
            string connectionString = "server=" + IniFile.DatabaseAddress +
                ";port=" + IniFile.DatabasePort.ToString() +
                ";database=" + IniFile.DatabaseName +
                ";user=" + IniFile.DatabaseUser +
                ";password=" + IniFile.DatabasePassword + ";";
            try
            {
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }
            catch (Exception)
            {
                LogFile.Add(Resources.DatabaseConnectError);
                Connection = null;
            }
            if (Connection != null)
            {
                LogFile.Add("База данных подключена");
                Program.AppWindow.pictureBoxConnectMySQL.Image = Resources.green;
            }
            _busy = false;
            LoadConfig();
        } // MySql()

//===============================================================================================================
// Name...........:	LoadConfig
// Description....:	Чтение из базы данных всех настроек, необходимых для работы программы
// Syntax.........:	LoadConfig()
//===============================================================================================================
        public static void LoadConfig()
        {
            _busy = true;
            if (Connection == null) return;
            MySqlCommand command = new MySqlCommand("SELECT * FROM `config`;", Connection);
            try
            {
                MySqlDataReader readData = command.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        switch ((string)readData[1])
                        {
                            case "MQTT_Address":
                                Sensors.MqttBrokerAddress = (string)readData[2];
                                break;
                            case "MQTT_Port":
                                if (!int.TryParse(readData[2].ToString(), out Sensors.MqttBrokerPort))
                                    Sensors.MqttBrokerPort = 1883;
                                break;
                            case "MQTT_User":
                                Sensors.MqttUserName = (string)readData[2];
                                break;
                            case "MQTT_Password":
                                Sensors.MqttPassword = (string)readData[2];
                                break;
                        }
                    }
                }
                readData.Close();
            }
            catch (Exception)
            {
                LogFile.Add("Error: ошибка чтения настроек системы из базы данных");
            }
            _busy = false;
        } // void LoadConfig(mainForm)

//===============================================================================================================
// Name...........:	ReadTable
// Description....:	Чтение всех строк из указанной таблицы в базе данных
// Syntax.........:	ReadTable(tablename)
// Parameters.....:	tablename   - имя таблицы в базе данных
// Return value(s):	Success:    - коллекцию массивов строк, содержащую данные из таблицы в базе данных
//                  Failure:    - null
//===============================================================================================================
        public static List<string[]> ReadTable(string tablename)
        {
            return ReadTable(tablename, "*", "", "");
        } // List<string[]> ReadTable(tablename)

//===============================================================================================================
// Name...........:	ReadTable
// Description....:	Выборочное чтение строк из указанной таблицы в базе данных
// Syntax.........:	ReadTable(tablename, where)
// Parameters.....:	tablename   - имя таблицы в базе данных
//                  where       - условие выбора строк
// Return value(s):	Success:    - коллекцию массивов строк, содержащую данные из таблицы в базе данных
//                  Failure:    - null
//===============================================================================================================
        public static List<string[]> ReadTable(string tablename, string where)
        {
            return ReadTable(tablename, "*", where, "");
        } // List<string[]> ReadTable(tablename, where)

//===============================================================================================================
// Name...........:	ReadTable
// Description....:	Выборочное чтение строк из указанной таблицы в базе данных
// Syntax.........:	ReadTable(tablename, fields, where)
// Parameters.....:	tablename   - имя таблицы в базе данных
//                  fields      - перечень полей для чтения (через запятую), или "*"
//                  where       - условие выбора строк
// Return value(s):	Success:    - коллекцию массивов строк, содержащую данные из таблицы в базе данных
//                  Failure:    - null
//===============================================================================================================
        public static List<string[]> ReadTable(string tablename, string fields, string where)
        {
            return ReadTable(tablename, fields, where, "");
        } // List<string[]> ReadTable(tablename, fields, where)

//===============================================================================================================
// Name...........:	ReadTable
// Description....:	Выборочное чтение строк из указанной таблицы в базе данных
// Syntax.........:	ReadTable(tablename, fields, where, order)
// Parameters.....:	tablename   - имя таблицы в базе данных
//                  fields      - перечень полей для чтения (через запятую), или "*"
//                  where       - условие выбора строк
//                  order       - порядок сотрировки
// Return value(s):	Success:    - коллекцию массивов строк, содержащую данные из таблицы в базе данных
//                  Failure:    - null
//===============================================================================================================
        public static List<string[]> ReadTable(string tablename, string fields, string where, string order)
        {
            if (Connection == null) return null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 3000) break;
            }
            stopWatch.Stop();
            if (_busy)
            {
                LogFile.Add("Error: ошибка чтения таблицы '" + tablename + "' из базы данных");
                return null;
            }
            _busy = true;
            var newTable = new List<string[]>();
            string sql = "SELECT " + fields + " FROM `" + tablename + "`";
            if (where.Length > 0) sql = sql + " WHERE " + where;
            if (order.Length > 0) sql = sql + " ORDER BY " + order;
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                MySqlDataReader readData = command.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        int n = readData.FieldCount;
                        var newLine = new string[n];
                        for (int i = 0; i < n; i++) newLine[i] = readData[i].ToString();
                        newTable.Add(newLine);
                    }
                }
                readData.Close();
            }
            catch
            {
                newTable = null;
                LogFile.Add("Error: ошибка чтения таблицы '" + tablename + "' из базы данных");
            }
            _busy = false;
            return newTable;
        } // List<string[]> ReadTable(tablename, fields, where, order)

//===============================================================================================================
// Name...........:	SaveTo
// Description....:	Добавление строки в указанную таблицу в базе данных
// Syntax.........:	SaveTo(tablename, fields, values)
// Parameters.....:	tablename   - имя таблицы в базе данных
//                  fields      - список полей через запятую
//                  values      - список значений соотвествующих полей через запятую
//===============================================================================================================
        public static void SaveTo(string tablename, string fields, string values)
        {
            SaveTo(tablename, fields, values, "");
        } // void SaveTo(tablename, fields, values)

//===============================================================================================================
// Name...........:	SaveTo
// Description....:	Запись полей в указанную таблицу в указанные строки в базе данных
// Syntax.........:	SaveTo(tablename, fields, values, where)
// Parameters.....:	tablename   - имя таблицы в базе данных
//                  fields      - список полей через запятую
//                  values      - список значений соотвествующих полей через запятую
//                  where       - условие выбора строк
//===============================================================================================================
        public static void SaveTo(string tablename, string fields, string values, string where)
        {
            if (Connection == null) return;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 3000) break;
            }
            stopWatch.Stop();
            if (_busy)
            {
                LogFile.Add("Ошибка записи таблицы полей '" + fields + "' в таблицу '" + tablename + "'");
                return;
            }
            _busy = true;
            string sql = "";
            if (where.Length > 0)
            {
                string[] fieldnames = fields.Split(new char[] {','});
                string[] fieldvalues = values.Split(new char[] {','});
                for (int i = 0; i < fieldnames.Length; i++)
                    sql += fieldnames[i] + " = " + (i < fieldvalues.Length ? fieldvalues[i] : "") + ", ";
                if (sql.Length > 2) sql = sql.Remove(sql.Length - 2, 2);
                sql = "UPDATE `" + tablename + "` SET " + sql + " WHERE " + where;
            }
            else
            {
                sql = "INSERT INTO `" + tablename + "` (" + fields + ") VALUES (" + values + ")";
            }
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                LogFile.Add("Ошибка записи таблицы полей '" + fields + "' в таблицу '" + tablename + "'");
            }
            _busy = false;
        } // void SaveTo(tablename, fields, data, where)

//===============================================================================================================
// Name...........:	Close
// Description....:	Отключение от базы данных
// Syntax.........:	Close()
//===============================================================================================================
        public static void Close()
        {
            if (Connection != null) Connection.Close();
        } // void Close()

    } // class MySQL
} // namespace SmartHome
