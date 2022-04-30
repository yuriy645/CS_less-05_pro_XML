using System;
using System.Linq;
using System.Xml.Linq;

namespace _3_TelephoneBook
{//Из файла TelephoneBook.xml (файл должен был быть создан в процессе выполнения
 //  дополнительного задания) выведите на экран только номера телефонов.
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xdocument = XDocument.Load("C:\\TelephoneBook.xml");

            var items = from tb in xdocument.Element("MyContacts").Elements("Contact") //xdocument.Element("MyContacts") возвращает первый найденный элемент с таким именем.
                        select new                                                //Метод Elements("имя_элемента") возвращает коллекцию элементов "переднего" элемента
                        {
                            TelNumber = tb.Element("TelNumber").Value   // можно получить с помощью свойства Value
                        };
            foreach (var item in items)
            {
                Console.WriteLine(item.TelNumber);
            }

            Console.ReadKey();
        }
    }
}
