namespace Boutique.Models
{
	public static class EmailBodyViewModel
	{
		public static string MailBody(string username, string confirmationType, string dueDate, string url)
		{
			string html = @"
<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Şifre Sıfırlama</title>
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f1f1f1;
      padding: 20px;
    }}
    
    .container {{
      max-width: 600px;
      margin: 0 auto;
      background-color: #fff;
      padding: 40px;
      border-radius: 4px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }}
    
    h1 {{
      text-align: center;
      margin-bottom: 30px;
    }}
    
    p {{
      margin-bottom: 20px;
    }}
    
    .btn {{
      display: inline-block;
      background-color: #4CAF50;
      color: #fff;
      padding: 10px 20px;
      text-decoration: none;
      border-radius: 4px;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h1>{0}</h1>
    <p>Merhaba, {1}</p>
    <p>Şifrenizi sıfırlamak için aşağıdaki bağlantıyı kullanabilirsiniz:</p>
    <p><a class='btn' href='{2}'>Şifre Sıfırlama</a></p>
    <p>Eğer şifre sıfırlama talebi yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>
    <p>İyi günler!</p>
  </div>
</body>
</html>
";

			string formattedHtml = string.Format(html,confirmationType, username, url);
            return formattedHtml;
		}

        public static string MailBodyConfirmation(string username, string confirmationType, string dueDate, string url)
        {
            string html = @"
<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Şifre Sıfırlama</title>
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f1f1f1;
      padding: 20px;
    }}
    
    .container {{
      max-width: 600px;
      margin: 0 auto;
      background-color: #fff;
      padding: 40px;
      border-radius: 4px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }}
    
    h1 {{
      text-align: center;
      margin-bottom: 30px;
    }}
    
    p {{
      margin-bottom: 20px;
    }}
    
    .btn {{
      display: inline-block;
      background-color: #4CAF50;
      color: #fff;
      padding: 10px 20px;
      text-decoration: none;
      border-radius: 4px;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h1>{0}</h1>
    <p>Merhaba, {1}</p>
    <p>Eposta adresinizi onaylamak için aşağıdaki bağlantıyı kullanabilirsiniz:</p>
    <p><a class='btn' href='{2}'>Eposta Onaylama</a></p>
    <p>Lütfen eposta adresinizi onaylayın.</p>
    <p>İyi günler!</p>
  </div>
</body>
</html>
";

            string formattedHtml = string.Format(html, confirmationType, username, url);
            return formattedHtml;
        }


        public static string MailBodyNewsLetter(string content,string img)
        {
            string html = @"
<!DOCTYPE html>
<html>
<head>
    <title>Haber Bülteni</title>
</head>
<body>
    <h1>Yeni Ürünler Hakkında Haber Bülteni</h1>
    <p>
        Merhaba değerli müşterilerimiz,
        <br>
        Size yeni ürünler hakkında bilgi vermek istiyoruz. Aşağıda bulunan ürünleri sizinle paylaşıyoruz.
    </p>
    <div style='text-align: center;'>
        <img src='{0}' alt='Yeni Ürünler' width='400' height='300'>
    </div>
    <h2>Yeni Ürünler</h2>
    <ul>
        <li>Ürün 1</li>
        <li>Ürün 2</li>
        <li>Ürün 3</li>
    </ul>
    <p>
        Bu ürünler hakkında daha fazla bilgi almak için lütfen bizimle iletişime geçin. Sizi mağazamızda görmekten mutluluk duyarız!
    </p>
    <p>
        Saygılarımızla,
        <br>
        Haber Bülteni Ekibi
    </p>
</body>
</html>
";

            string formattedHtml = string.Format(html, img);
            return formattedHtml;
        }
    }
}
