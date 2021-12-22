using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages  //newlememek için static kullanıyoruz.
    {
        public static string CarAdded = "Araç eklendi.";
        public static string BrandAdded = "Marka eklendi.";
        public static string ColorAdded = "Araç eklendi.";
        public static string UserAdded = "Kullanıcı eklendi.";
        public static string CustomerAdded = "Müşteri eklendi.";
        public static string RentalAdded = "Kiralık araç eklendi.";
        public static string CarDeleted = "Araç silindi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string RentalDeleted = "Kiralık araç silindi.";
        public static string CarUpdated = "Araç bilgileri güncellendi.";
        public static string CustomerUpdated = "Müşteri bilgileri güncellendi.";
        public static string RentalUpdated = "Kiralık araç bilgileri güncellendi.";
        public static string CarDescriptionInvalid = "Araç ismi geçersiz.";
        public static string ColorDescriptionInvalid = "Renk ismi geçersiz.";
        public static string UserDescriptionInvalid = "Kullanıcı ismi geçersiz.";
        public static string CustomerDescriptionInvalid = "Müşteri ismi geçersiz.";
        public static string PasswordDescriptionInvalid = "Şifre geçersiz.";
        public static string RentalDescriptionInvalid = "Kiralık araç bilgileri geçersiz.";
        public static string BrandDescriptionInvalid = "Araba ismi minimum 2 karakter uzunluğunda olmalı!";
        public static string CarModelYearInvalid = "Araç model yılı geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda!";
        public static string CarsListed = "Araçlar listelendi.";
        public static string ColorListed = "Renkler listelendi.";
        public static string BrandListed = "Marka listelendi.";
        public static string UserListed = "Kullanıcı listelendi.";
        public static string CustomerListed = "Müşteri listelendi.";
        public static string RentalListed = "Kiralık araçlar listelendi.";
        public static string AuthorizationDenied = "Yetkiniz yok.";

        public static string CarImageAdded = "Araç görseli eklendi.";

        public static string UserrRegistered = "Kayıt oldu.";
        public static string UserrNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Parola hatası.";
        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string UserrAlreadyExists = "Kullanıcı mevcut.";
        public static string AccessTokenCreated = "Token oluşturuldu.";
        
        public static string ProductCountOfColorError = "Bir renk grubunda 10'dan fazla araç olamaz.";
        public static string CarNameAlreadyExists = "Bu isimde zaten başka bir araç var.";
        public static string ColorLimitExceeded = "Renk limiti aşıldığı için yeni araba eklenemiyor.";
    }
}
