using MessageProcessorWorker.ErrorHandling.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.ErrorHandling.ErrorMessages
{
    public static class ErrorMessages
    {
        public static readonly List<ErrorModel> ErrorList = new List<ErrorModel>
        {
            // Custom Error Codes
            new ErrorModel { Code = "20", Description = "Mesaj metninde ki problemden dolayı gönderilemedi veya standart maksimum mesaj karakter sayısını geçti." },
            new ErrorModel { Code = "30", Description = "Geçersiz kullanıcı adı, şifre veya kullanıcınızın API erişim izninin olmadığını gösterir." },
            new ErrorModel { Code = "40", Description = "Mesaj başlığınızın (gönderici adınızın) sistemde tanımlı olmadığını ifade eder." },
            new ErrorModel { Code = "50", Description = "Abone hesabınız ile İYS kontrollü gönderimler yapılamamaktadır." },
            new ErrorModel { Code = "51", Description = "Aboneliğinize tanımlı İYS Marka bilgisi bulunamadığını ifade eder." },
            new ErrorModel { Code = "70", Description = "Hatalı sorgulama. Gönderdiğiniz parametrelerden birisi hatalı veya zorunlu alanlardan birinin eksik olduğunu ifade eder." },
            new ErrorModel { Code = "80", Description = "Gönderim sınır aşımı." },
            new ErrorModel { Code = "85", Description = "Mükerrer Gönderim sınır aşımı. Aynı numaraya 1 dakika içerisinde 20'den fazla görev oluşturulamaz." },

            // You can add more error codes and descriptions here
        };
    }
}
