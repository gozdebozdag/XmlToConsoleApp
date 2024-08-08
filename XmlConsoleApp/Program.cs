using System;
using System.Xml;

class Program
{
    static void Main()
    {
        CreateXml();

        DisplayXml();
    }

    static void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement root = xmlDoc.DocumentElement;
        xmlDoc.InsertBefore(xmlDeclaration, root);

        XmlElement rootElement = xmlDoc.CreateElement(string.Empty, "Products", string.Empty);
        xmlDoc.AppendChild(rootElement);

        XmlElement product1 = xmlDoc.CreateElement(string.Empty, "Product", string.Empty);
        rootElement.AppendChild(product1);

        XmlElement name1 = xmlDoc.CreateElement(string.Empty, "Name", string.Empty);
        XmlText nameText1 = xmlDoc.CreateTextNode("Laptop");
        name1.AppendChild(nameText1);
        product1.AppendChild(name1);

        XmlElement price1 = xmlDoc.CreateElement(string.Empty, "Price", string.Empty);
        XmlText priceText1 = xmlDoc.CreateTextNode("1500");
        price1.AppendChild(priceText1);
        product1.AppendChild(price1);

        XmlElement product2 = xmlDoc.CreateElement(string.Empty, "Product", string.Empty);
        rootElement.AppendChild(product2);

        XmlElement name2 = xmlDoc.CreateElement(string.Empty, "Name", string.Empty);
        XmlText nameText2 = xmlDoc.CreateTextNode("Smartphone");
        name2.AppendChild(nameText2);
        product2.AppendChild(name2);

        XmlElement price2 = xmlDoc.CreateElement(string.Empty, "Price", string.Empty);
        XmlText priceText2 = xmlDoc.CreateTextNode("800");
        price2.AppendChild(priceText2);
        product2.AppendChild(price2);

        string filePath = "Products.xml";
        xmlDoc.Save(filePath);

        Console.WriteLine("XML dosyası oluşturuldu ve kaydedildi.");
    }

    static void DisplayXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Products.xml");

        XmlElement root = xmlDoc.DocumentElement;

        foreach (XmlNode productNode in root.ChildNodes)
        {
            string name = productNode["Name"].InnerText;
            string price = productNode["Price"].InnerText;

            Console.WriteLine($"Product Name: {name}, Price: {price}");
        }
    }
}
