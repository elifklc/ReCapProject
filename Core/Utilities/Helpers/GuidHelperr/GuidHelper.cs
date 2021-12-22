using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.GuidHelperr
{
    public class GuidHelper
    {
        public static string CreateGuid()
        {
            return Guid.NewGuid().ToString(); //burada yüklediğimiz dosyamız için eşsiz bir isim oluşturduk.
        }                                     //yani dosya eklerken dosyanın adı kendi olmasın, 
    }                                         //biz ona eşsiz bir isim oluşturalım ki aynı isimde başka bir dosya varsa çakışmasınlar.
}                                             //Guid.NewGuid() => bu metod bize eşsiz bir değer oluşturdu.
                                              //ToString() => bununla da string hale getirdik.
                                              //Yoğurdun kaymağı ile değil nasıl yapıldığı ile ilgileniyorsanız eğer şöyle anlatalım,
                                              //Guid.NewGuid() => bu ifade bana eşsiz bir değer oluşturdu ToString() bunu kullanarak da ben değerimi 16lık sayı tabanına çevirdim.
                                              //tüm amaç eşsiz bir değer oluşturalım ki aynı isimden dosyalar olursa çakışmasın.