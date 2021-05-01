using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {
        public static string OperationAdded = "İşlem Eklendi";
        public static string OperationDeleted = "İşlem Silindi";
        public static string OperationUpdated = "İşlem Güncellendi";
        public static string OperationListed = "İşlem Listelendi";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered="Kayıt oldu";
        public static string UserNotFound="Kullanıcı bulunamadı";
        public static string PasswordError="Parola hatası";
        public static string SuccessfulLogin="Başarılı giriş";
        public static string UserAlreadyExists="Kullanıcı mevcut";
        public static string AccessTokenCreated= "Access token başarıyla oluşturuldu";
        public static string Convert="Dönüştürüldü";
        public static string NotConvert = "Dönüştürülemedi";
    }
}
