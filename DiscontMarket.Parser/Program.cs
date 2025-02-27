using Newtonsoft.Json;
using productdetails;
using ProductPrices;
using ProductsList;
using System.Diagnostics;
using System.Net;
using System.Text;

internal class Program
{
    /*
     0 - название
     1 - цена
     2 - количество
     3 - название модели
     4 - рейтинг
     5 - полное описание
     */

    //ids
    private const int tv = 1;               //tv
    private const int fridge = 3;           //fridge
    private const int builtin = 4;          //builtin
    private const int washingmachine = 5;   //washingmachine
    private const int smallappliances = 6;  //smallappliances
    private const int dishwashers = 28;     //dishwashers


    //CATEGORY ID 4я строчка пред пред последний символ(перед {6})
    private const string TEMPLATE_PRODUCT = @"
        INSERT INTO ""Products"" 
        (""ProductName"", ""Price"", ""Quantity"", ""IconPath"", ""Rating"", ""Description"", ""FullDescription"", 
            ""Availability"", ""Status"", ""CategoryID"", ""BrandID"", ""UserID"") 
        SELECT '{0}', {1}, {2}, '/items/productimages/{3}_0.png', {4}, '', '{5}', 'InStock', 'Discount', 28, {6}, 1
        WHERE NOT EXISTS (
            SELECT 1 FROM ""Products"" WHERE ""ProductName"" = '{0}'
        );";

    private const string TEMPLATE_PRODUCT_ATTRIBUTE = @"
        INSERT INTO ""ProductAttribute"" (""ProductID"", ""AttributeID"") 
        SELECT p.""ID"", a.""ID"" FROM ""Products"" p, ""Attributes"" a 
        WHERE p.""ProductName"" = '{0}' 
        AND a.""Name"" = '{1}' 
        AND a.""Type"" = '{2}'
        AND NOT EXISTS (
            SELECT 1 FROM ""ProductAttribute"" WHERE ""ProductID"" = p.""ID"" AND ""AttributeID"" = a.""ID""
        );";
    private const string TEMPLATE_PRODUCT_IMAGE = "\r\nINSERT INTO \"Images\" (\"Path\", \"ProductID\")" +
        "\r\n VALUES\r\n('{0}', {1});";
    private const string TEMPLATE_PRODUCT_INSERT_VALUE = "(SELECT \"ID\" FROM \"Products\" WHERE \"ProductName\" = '{0}')";                 // Достает Product по id

    // private const string TEMPLATE_PRODUCT_ATTRIBUTE_INSERT_VALUE = "(SELECT \"ID\" FROM \"Attributes\" WHERE \"Name\" = '{0}')";         // Достает ID Аттрибута (должен существовать Attribute)

    private const string TEMPLATE_PRODUCT_BRAND_INSERT_VALUE = "(SELECT \"ID\" FROM \"Brands\" WHERE \"Name\" = '{0}')";                    // Достает ID Бренда (должен существовать Brand)


    private const string TEMPLATE_ATTRIBUTE = @"
        INSERT INTO ""Attributes"" (""NameTranslate"", ""Name"", ""Type"") 
        SELECT '{0}', '{0}', '{1}'
        WHERE NOT EXISTS (
            SELECT 1 FROM ""Attributes"" WHERE ""Name"" = '{0}' AND ""Type"" = '{1}'
        );";

    //CATEGORY 4я строчка, после  AND c.""Name"" =
    private const string TEMPLATE_ATTRIBUTE_CATEGORY = @"
        INSERT INTO ""AttributeCategory"" (""AttributeID"", ""CategoryID"") 
        SELECT a.""ID"", c.""ID"" FROM ""Attributes"" a, ""Categories"" c 
        WHERE a.""Name"" = '{0}' AND a.""Type"" = '{1}' 
        AND c.""Name"" = 'dishwashers'
        AND NOT EXISTS (
            SELECT 1 FROM ""AttributeCategory"" WHERE ""AttributeID"" = a.""ID"" AND ""CategoryID"" = c.""ID""
        );";

    private const string TEMPLATE_BRAND = @"
        INSERT INTO ""Brands"" (""NameTranslate"", ""Name"", ""Type"") 
        SELECT '{0}', '{0}', 'Бренды'
        WHERE NOT EXISTS (
            SELECT 1 FROM ""Brands"" WHERE ""Name"" = '{0}'
        );";

    //CATEGORY 3я строчка, после  AND c.""Name"" =
    private const string TEMPLATE_BRAND_CATEGORY = @"
        INSERT INTO ""BrandCategory"" (""BrandID"", ""CategoryID"") 
        SELECT b.""ID"", c.""ID"" FROM ""Brands"" b, ""Categories"" c 
        WHERE b.""Name"" = '{0}' AND c.""Name"" = 'dishwashers'
        AND NOT EXISTS (
            SELECT 1 FROM ""BrandCategory"" WHERE ""BrandID"" = b.""ID"" AND ""CategoryID"" = c.""ID""
        );";

    private static int currentProxyId = 0;
    private static WebProxy? currentProxy;
    private static readonly Dictionary<int, KeyValuePair<string, string>> proxies = [];

    private static readonly Random random = new();


    private static async Task Main()
    {
        string productListJson = File.ReadAllText("D:\\Files\\Json\\list_dishwashers.json");
        string productPricesJson = File.ReadAllText("D:\\Files\\Json\\prices_dishwashers.json");

        ProductListRoot productList = JsonConvert.DeserializeObject<ProductListRoot>(productListJson)!;
        PricesRoot productPrices = JsonConvert.DeserializeObject<PricesRoot>(productPricesJson)!;

        int firstIteration = 0;


        /*СОЗДАНИЕ ПРОДУКТОВ*/
        int iteration = 0;
        foreach (Product product in productList.body.products)
        {
            iteration++;
            string TemplateProductAttributes = "";
            string TemplateProductImages = "";

            string sqlAttributeAndBrandScript = "";

            foreach (var property in product.propertiesPortion)
            {


                TemplateProductAttributes += string.Format(TEMPLATE_PRODUCT_ATTRIBUTE,
                   /*0*/ product.name,
                   /*1*/ property.value,
                         property.name) +
                        "\r\n";

                string sqlAttribute = "";
                if (File.Exists($"attributes\\{property.name}_{property.value}.txt")) continue;

                sqlAttribute += string.Format(TEMPLATE_ATTRIBUTE,
                        /*0*/property.value,
                        /*1*/property.name) +
                        "\r\n";

                sqlAttribute += string.Format(TEMPLATE_ATTRIBUTE_CATEGORY,
                    property.value,
                    property.name)
                    + "\r\n";

                string sanitizedFileName = SanitizeFileName($"{property.name}_{property.value}.txt");
                File.WriteAllText($"attributes\\{sanitizedFileName}", sqlAttribute, Encoding.UTF8);
                sqlAttributeAndBrandScript += sqlAttribute;
            }

            if (!File.Exists($"attributes\\{product.brandName}.txt"))
            {
                string sqlBrand = "";

                sqlBrand += string.Format(TEMPLATE_BRAND,
                  /*0*/ product.brandName) +
                  "\r\n" +
                  string.Format(TEMPLATE_BRAND_CATEGORY,
                  /*0*/product.brandName) +
                  "\r\n";

                File.WriteAllText($"attributes\\{product.brandName}.txt", sqlBrand, Encoding.UTF8);
                sqlAttributeAndBrandScript += sqlBrand;
            }
            if (sqlAttributeAndBrandScript != "")
                File.AppendAllText($"sqls\\generalAttributes.txt", sqlAttributeAndBrandScript + "\r\n", Encoding.UTF8);

            if (File.Exists($"items\\productimages\\{product.nameTranslit}_0.png") && File.Exists($"products\\{product.nameTranslit}.txt"))
                continue;

            string productId = product.productId;

        tmp_1:

            HttpResponseMessage detailsRespone;
            using (HttpClient client = await MakeProxiedClient(product.nameTranslit, productId))
            {
                try
                {
                    detailsRespone = await client.GetAsync($"https://www.mvideo.ru/bff/product-details?productId={productId}");
                    detailsRespone.EnsureSuccessStatusCode();
                }
                catch
                {
                    ParseProxies();

                    if (proxies.Count == 0)
                    {
                        Console.WriteLine(" Waiting 10 mins.");
                        Thread.Sleep(600000);

                    }
                    else
                    {
                        Console.WriteLine("Adress banned, try using next proxy");
                        currentProxy = await SelectNewProxy();
                    }

                    goto tmp_1;
                }
            }

            string sql = "";

            string detailsJson = await detailsRespone.Content.ReadAsStringAsync();
            ProductDetailsRoot productDetails = JsonConvert.DeserializeObject<ProductDetailsRoot>(detailsJson)!;

            int productPrice = 0;
            var productInfo = productPrices.body.materialPrices.FirstOrDefault(x => x.productId == productId);
            if (productInfo != null)
            {
                productPrice = productInfo.price.basePrice / 2;
            }

            int productCount = random.Next(5, 20);

            string productRating = Math.Round(new Random().NextDouble() * (5.0 - 4.1) + 4.1, 2).ToString().Replace(",", ".");
            string productDescription = productDetails.body.description;


            if (!File.Exists($"items\\productimages\\{product.nameTranslit}_0.png"))
                TemplateProductImages = await SaveProductImage(product, productId, TemplateProductImages, product.images.Count);


            if (!File.Exists($"products\\{product.nameTranslit}.txt"))
            {
                string productBrandId = string.Format(TEMPLATE_PRODUCT_BRAND_INSERT_VALUE, product.brandName);

                sql += string.Format(TEMPLATE_PRODUCT,
                    /*0*/product.name,
                    /*1*/productPrice,
                    /*2*/productCount,
                    /*3*/product.nameTranslit,
                    /*4*/productRating,
                    /*5*/productDescription,
                    /*6*/productBrandId) +
                    $"\r\n";
                sql += TemplateProductAttributes;
                sql += TemplateProductImages;

                File.WriteAllText($"products\\{product.nameTranslit}.txt", sql, Encoding.UTF8);
                File.AppendAllText($"sqls\\general.txt", sql + "\r\n", Encoding.UTF8);
            }
            Console.WriteLine($"ADD New object: {product.name}!");
            Thread.Sleep(100);
        }

        Console.WriteLine($"Done!");
        Console.ReadKey();

        Process.GetCurrentProcess().Kill();
    }

    private static void ParseProxies()
    {
        if (!File.Exists("D:\\Files\\proxies.txt") || proxies.Count > 0)
            return;

        string[] lines = File.ReadAllLines("D:\\Files\\proxies.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitted = lines[i].Split();

            string proxyIp = splitted[0], proxyPort = splitted[1];
            proxies.Add(i, new KeyValuePair<string, string>(proxyIp, proxyPort));
        }
    }

    private static string SanitizeFileName(string fileName)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars(); // Получаем список недопустимых символов для имени файла
        foreach (char c in invalidChars)
        {
            fileName = fileName.Replace(c, '_'); // Заменяем недопустимые символы на _
        }
        return fileName;
    }

    private static async Task<string> SaveProductImage(Product product, string productId, string TemplateProductImages, int imagesCount)
    {
        using HttpClient client = new();


        for (int i = 0; i < imagesCount; i++)
        {
            string imageLink = i == 0 ? $"https://img.mvideo.ru/Pdb/{productId}b.jpg" : $"https://img.mvideo.ru/Pdb/{productId}b{i}.jpg";

            HttpResponseMessage imageResponse = await client.GetAsync(imageLink);
            if (!imageResponse.IsSuccessStatusCode) break;
            imageResponse.EnsureSuccessStatusCode();

            byte[] image = await imageResponse.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes($"items\\productimages\\{product.nameTranslit}_{i}.png", image);

            TemplateProductImages += string.Format(TEMPLATE_PRODUCT_IMAGE,
               /*0*/ $"/items/productimages/{product.nameTranslit}_{i}.png",
               /*1*/ string.Format(TEMPLATE_PRODUCT_INSERT_VALUE, /*0*/product.name));
        }

        File.WriteAllText($"images\\{product.nameTranslit}.txt", TemplateProductImages, Encoding.UTF8);
        return TemplateProductImages;
    }

    private static async Task<WebProxy?> SelectNewProxy()
    {
        if (proxies.Count == currentProxyId)
        {
            Console.WriteLine("All proxy banned\r\n" +
                "Enter \"again\" or repeat\r\n");

            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) ||
                (!input.Equals("again", StringComparison.CurrentCultureIgnoreCase) &&
                !input.Equals("repeat", StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine("Ну ты пиздец братан, не хочешь - ну и иди нахуй.\r\n");
                Console.ReadKey();

                Process.GetCurrentProcess().Kill();
            }
            else if (input.Equals("again", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Start again...\r\n");
                currentProxyId = 0;
            }
            else if (input.Equals("repeat", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("try repeat\r\n");
                await Main();
            }
        }

        KeyValuePair<string, string> proxyInfo = proxies[currentProxyId];
        currentProxyId++;

        string proxyIp = proxyInfo.Key, proxyPort = proxyInfo.Value;

        Console.WriteLine(
            $"\r\n\r\n-------------------------------------\r\n" +
            $"Used proxy: {proxyIp}:{proxyPort}" +
            $"\r\n-------------------------------------\r\n\r\n");

        currentProxy = new WebProxy($"http://{proxyIp}:{proxyPort}")
        {
            BypassProxyOnLocal = false,
            UseDefaultCredentials = false
        };
        return currentProxy;
    }

    private static Task<HttpClient> MakeProxiedClient(string nameTranslit, string productId)
    {
        HttpClientHandler handler = new() { Proxy = currentProxy };

        HttpClient client = new(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
        client.DefaultRequestHeaders.Add("Referer", $"https://www.mvideo.ru/products/{nameTranslit}-{productId}");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");

        StringBuilder cookieHeaderValue = new();
        cookieHeaderValue.Append("MVID_REGION_ID=1; ");
        cookieHeaderValue.Append("MVID_CITY_ID=CityCZ_975; ");
        cookieHeaderValue.Append("MVID_TIMEZONE_OFFSET=3; ");
        cookieHeaderValue.Append("MVID_REGION_SHOP=S002; ");
        client.DefaultRequestHeaders.Add("Cookie", cookieHeaderValue.ToString());

        return Task.FromResult(client);
    }
}
