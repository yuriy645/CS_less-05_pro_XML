using System;
using System.Xml;
using System.Xml.Linq;

namespace _1_add
{//Создайте .xml файл, который соответствовал бы следующим требованиям:
// имя файла: TelephoneBook.xml
// корневой элемент: “MyContacts”
// тег “Contact”, и в нем должно быть записано имя контакта и атрибут “TelephoneNumber”
//со значением номера телефона.
    class Program
    {
        static void Main(string[] args)
        {
            // добавляем корневой элемент в документ
            XDocument xdoc = new XDocument();

            // создаем корневой элемент
            XElement myContacts = new XElement("MyContacts");
            
            // создаем первый элемент
            XElement vasya = new XElement("Contact");
            // создаем атрибут
            XAttribute vasyaName = new XAttribute("Name", "Вася");
            XElement vasyaPhone = new XElement("TelNumber", "+38066466436");
            
            // добавляем атрибут и элементы в первый элемент
            vasya.Add(vasyaName);
            vasya.Add(vasyaPhone);

            // добавляем в корневой элемент
            myContacts.Add(vasya);
            


            // создаем второй  элемент
            XElement petya = new XElement("Contact");
            // создаем атрибут
            XAttribute petyaName = new XAttribute("Name", "Петя");
            XElement petyaPhone = new XElement("TelNumber", "+3804645774");

            // добавляем атрибут и элементы в первый элемент
            petya.Add(petyaName);
            petya.Add(petyaPhone);

            // добавляем в корневой элемент
            myContacts.Add(petya);

            // добавляем корневой элемент в документ
            xdoc.Add(myContacts);
            //сохраняем документ
            xdoc.Save("c:\\TelephoneBook.xml");

        }
    }
}
