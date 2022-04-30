using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace Task4
{
    class SettingConfig : ISetting
    {
        public Brush BackColor { get; set; }
        public Brush TextColor { get; set; }
        public int TextSize { get; set; }
        public FontFamily TextFont { get; set; }

        XmlDocument doc;

        public SettingConfig()
        {
            doc = LoadConfigDocument(); //запуск метода создания представления xml документа типа XmlDocument (для конфигурации)
            ReadFromXMLorSetDefault(); //запуск метода загрузки из файла
        }

        public void SaveSettings()
        {
            
            XmlNode node = doc.SelectSingleNode("//appSettings"); // Открываем узел appSettings, в котором содержится перечень настроек.
            if (node == null)
            {
                node = doc.CreateNode(XmlNodeType.Element, "appSettings", "");
                XmlElement root = doc.DocumentElement;
                root.AppendChild(node);
            }

            
            string[] keys = new string[] {"BackColor", // Массив ключей (создан для упрощения обращения к файлу конфигурации).
                                          "TextColor",
                                          "TextSize",
                                          "TextFont"};

            
            string[] values = new string[] { BackColor.ToString(), // Массив значений (создан для упрощения обращения к файлу конфигурации).
                                             TextColor.ToString(),
                                             TextSize.ToString(),
                                             TextFont.ToString() };

            
            for (int i = 0; i < keys.Length; i++) // Цикл модификации файла конфигурации.
            {
                XmlElement element = node.SelectSingleNode(string.Format("//add[@key='{0}']", keys[i])) as XmlElement; // Обращаемся к конкретной строке по ключу.

                if (element != null) { element.SetAttribute("value", values[i]); } // Если строка с таким ключем существует - записываем значение.
                else
                {                                                                // Иначе: создаем строку и формируем в ней пару [ключ]-[значение].
                    element = doc.CreateElement("add");
                    element.SetAttribute("key", keys[i]);
                    element.SetAttribute("value", values[i]);
                    node.AppendChild(element);
                }
            }
            doc.Save(Assembly.GetExecutingAssembly().Location + ".config"); // Сохраняем результат модификации.
        }

        private void ReadFromXMLorSetDefault()
        {
            
            NameValueCollection allAppSettings = ConfigurationManager.AppSettings; // возвращает коллекцию ключ/ значение


            var bc = new BrushConverter();
            string messageException = string.Empty;
            try
            {
                BackColor = (Brush)bc.ConvertFromString(allAppSettings["BackColor"]); // читаем value с ключем BackColor ; методом из класса BrushConverter и приводим к типу Brush
            }
            catch (Exception)
            {
                BackColor = (Brush)bc.ConvertFromString(Colors.AliceBlue.ToString());
                messageException += "Цвет фона задан не верно: " + allAppSettings["BackColor"] + Environment.NewLine;
            }

            try
            {
                TextColor = (Brush)bc.ConvertFromString(allAppSettings["TextColor"]);
            }
            catch (Exception)
            {
                TextColor = (Brush)bc.ConvertFromString(Colors.Black.ToString());
                messageException += "Цвет текста задан не верно: " + allAppSettings["TextColor"] + Environment.NewLine;
            }

            try
            {
                TextSize = int.Parse(allAppSettings["TextSize"]);
            }
            catch (Exception)
            {
                TextSize = 12;
                messageException += "Размер текста задан не верно: " + allAppSettings["TextSize"] + Environment.NewLine;
            }

            try
            {
                TextFont = new FontFamily(allAppSettings["TextFont"]);
            }
            catch (Exception)
            {
                TextFont = new FontFamily("Segoe UI");
            }
            

            if (!string.IsNullOrEmpty(messageException))
            {
                MessageBox.Show(messageException);
            }
        }

        private XmlDocument LoadConfigDocument() //получение представления xml файла конфигурации
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(Assembly.GetExecutingAssembly().Location + ".config");// загрузка xml файла // получение полного пути сборки, выполняемой сейчас
                return doc; //(Assembly.GetExecutingAssembly().Location + ".config");
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
    }
}
