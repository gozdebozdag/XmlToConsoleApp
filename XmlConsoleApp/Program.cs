using System;
using System.Xml;

class Program
{
    static void Main()
    {
      try
      { 
        while (true)
        {
            Console.WriteLine("1.XML oluştur");
            Console.WriteLine("2.XML'i göster");
            Console.WriteLine("3.XML'deki ürün fiyatını güncelle");
            Console.WriteLine("4.Çıkış");
            Console.WriteLine("Bir seçenek girin: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateXml();
                    break;
                case "2":
                    DisplayXml();
                    break;
                case "3":
                    Console.Write("Güncellenecek ürünün adını girin:");
                    string productName = Console.ReadLine();
                    Console.Write("Yeni fiyatını girin: ");
                    string newPrice = Console.ReadLine();
                    UpdateXml("Products.xml", productName, newPrice);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçnek, lütfen tekrar deneyiniz");
                    break;
            }
        }

    }
        catch(Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }


        
    }

    static void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement root = xmlDoc.DocumentElement;
        xmlDoc.InsertBefore(xmlDeclaration, root);

        XmlElement rootElement = xmlDoc.CreateElement(string.Empty, "Products", string.Empty);
        xmlDoc.AppendChild(rootElement);

        Console.WriteLine("Ürün adı girin:");
        string productName = Console.ReadLine();

        Console.WriteLine("Ürün fiyatı girin:");
        string productPrice = Console.ReadLine();

        if(string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(productPrice))
        {
            Console.WriteLine("Ürün adı ve fiyatı boş olamaz.");
            return;
        }

        if(!decimal.TryParse(productPrice,out _))
        {
            Console.WriteLine("Fiyat geçerli bir sayı olmalıdır.");
            return;
        }

        XmlElement product = xmlDoc.CreateElement(string.Empty, "Product", string.Empty);
        rootElement.AppendChild(product);

        XmlElement name = xmlDoc.CreateElement(string.Empty, "Name", string.Empty);
        XmlText nameText = xmlDoc.CreateTextNode(productName);
        name.AppendChild(nameText);
        product.AppendChild(name);

        XmlElement price = xmlDoc.CreateElement(string.Empty, "Price", string.Empty);
        XmlText priceText = xmlDoc.CreateTextNode(productPrice);
        price.AppendChild(priceText);
        product.AppendChild(price);

        string filePath = "Products.xml";
        xmlDoc.Save(filePath);

        Console.WriteLine("XML dosyası oluşturuldu ve kaydedildi.");
    }

    static void DisplayXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Products.xml");

        XmlElement root = xmlDoc.DocumentElement;

        Console.WriteLine("Ürünler: ");
        foreach (XmlNode productNode in root.ChildNodes)
        {
            string name = productNode["Name"].InnerText;
            string price = productNode["Price"].InnerText;

            Console.WriteLine($"Product Name: {name}, Price: {price}");
        }
    }

    static void UpdateXml(string filePath,string productName,string newPrice)
    {
        XmlDocument xmlDoc=new XmlDocument();
        xmlDoc.Load(filePath);

        XmlNode productNode = xmlDoc.SelectSingleNode($"/Products/Product[Name='{productName}']");
        if(productNode!=null)
        {
            XmlNode priceNode = productNode["Price"];
            priceNode.InnerText = newPrice;

            xmlDoc.Save(filePath);
            Console.WriteLine("Ürün fiyatı güncellendi");
        }
        else
        {
            Console.WriteLine("Ürün bulunamadı.");
        }
    }
}
