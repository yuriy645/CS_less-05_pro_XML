using System;
using System.Linq;
using System.Xml.Linq;


namespace _2_info_xml
{// Создайте приложение, которое выводит на экран всю информацию об указанном .xml файле.
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xdocument = XDocument.Load("C:\\TelephoneBook.xml"); 

            var items = from tb in xdocument.Element("MyContacts").Elements("Contact") //xdocument.Element("MyContacts") возвращает первый найденный элемент с таким именем.
                        select new                                                //Метод Elements("имя_элемента") возвращает коллекцию элементов "переднего" элемента
                        {
                            Name = tb.Attribute("Name").Value,           //Значение простых элементов, которые содержат один текст, 
                            TelNumber = tb.Element("TelNumber").Value   // можно получить с помощью свойства Value
                        };
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name} - {item.TelNumber}");
            }

            Console.ReadKey();
        }
    }
    
}
