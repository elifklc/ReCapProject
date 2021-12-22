using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Helpers.GuidHelperr;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Core.Utilities.Helpers
{
    //projeme yükleyeceğim dosyalarla ilgili yükleme, silme ve güncelleme işlemlerini bu class'ta yapıyorum.
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath)//buradaki string filePath, CarImageManager'dan gelen dosyanın kaydedildiği adres ve adı.
        {
            if (File.Exists(filePath)) //if kontrolü ile parametrede gelen adreste öyle bir dosya var mı diye kontrol ediliyor.
            {
                File.Delete(filePath); //eğer dosya var ise dosya bulunduğu yerden siliniyor.
            }
        }

        public string Update(IFormFile file, string filePath, string root) //dosya güncellemek için ise gelen parametreye baktığımızda güncellenecek yeni dosya, eski dosyamızın kayıt dizini ve yeni bir kayıt dizini
        {
            if (File.Exists(filePath)) //tekrar if kontrolü ile parametrede gelen adreste öyle bir dosya var mı diye kontrol ediliyor.
            {
                File.Delete(filePath); //eğer dosya var ise bulunduğu yerden siliniyor.
            }
            return Upload(file, root); //eski dosya silindikten sonra yerine geçecek yeni dosya için alttaki Upload metoduna yeni dosya ve kayıt edileceği adres parametre olarak döndürülüyor.
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0) //file.Length => Dosya uzunluğu byte olarak alınır. Burada dosya gönderilmiş mi gönderilmemiş mi diye test işlemi yapılır.
            {
                if (!Directory.Exists(root)) //Directory=>System.IO'nun bir classı. buradaki işlem tam olrak şu: bu upload metodunun parametresi olan string root CarManager'dan gelmektedir.
                                             //CarImageManager içerisine girdiğinizde buraya parametre olarak *PathConstants.ImagePath* böyle bir şey gönderildiğini görürüz. PathConstants classı içeirisine girdiğinizde string bir ifadeyle bir dizin adresi vardır.
                                             //O adres bizim yükleyeceğimiz dosyaların kayıt edileceği adres burada *Check if a directory Exists* ifadesi şunu belirtiyor, dosyanın kaydedileceği adres dizini var mı? varsa if yapısının kod bloğundan ayrılır eğer yoksa içindeki kodda dosyaların kayıt edilecek dizini oluşturur.     
                {
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName); //Path.GetExtension(file.FileName) => seçmiş olduğumuz dosyanın uzantısını elde diyoruz.
                string guid = GuidHelper.CreateGuid(); //Core.Utilities.Helpers.GuidHelper klasörünün içindeki GuidManager klasörüne gidersek burada satırda ne yapıldığını anlarız. 
                string filePath = guid + extension; //dosyanın oluşturduğumuz adını ve uzantısını yan yana getiriyoruz. mesela metin dosyası ise .txt gibi bu projemizde resim yükleyeceğimiz için .jpg olacak uzantılar.
            
                using (FileStream fileStream = File.Create(root + filePath)) //burada en başta FileStream classının bir örneği oluşturulur. sonrasında File.Create(root + newPath) => belirtilen yolda bir dosya oluşturulur veya üzerine yazar. (root + newPath) => oluşturulacak dosyanın yolu ve adı.
                {
                    file.CopyTo(fileStream); //kopyalanacak dosyanın kopyalanacağı akışı belirtti. yani yukarıda gelen IFromFile türündeki file dosyasının nereye kopyalanacağını söyledik.
                    fileStream.Flush(); //arabellekten siler.
                    return filePath; //burada dosyamızın tam adını geri gönderiyoruz. sebebi de sql servera dosya eklenirken adı ile eklenmesi.
                }

            }
            return null;
        }
    }
}

//IFormFile projemize bir dosya yüklemek için kullanılan yöntemdir, HttpRequest ile gönderilen bir dosyayı temsil eder.
//FileStream, Stream ana soyut sınıfı kullanırak genişletilmiş, belirtilen kaynak dosyalar üzerine okuma/yazma/atlama gibi operasyonları yapmamıza yardımcı olan bir sınıftır.
