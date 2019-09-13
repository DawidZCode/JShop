using Core.Infrastructure.Mailer;
using MediatR;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PaymentModule.Comtracts.Commands;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentModule.Handlers.Commands
{
    public class ExecuteBankTransactionCommandHandler : IRequestHandler<ExecuteBankTransactionCommand, bool>
    {
        private IConfiguration _configuration;
        private IShopMailer _mailerService;

        public ExecuteBankTransactionCommandHandler(IConfiguration configuration, IShopMailer mailerService)
        {
            _mailerService = mailerService;
            _configuration = configuration;
        }
      

        public async Task<bool> Handle(ExecuteBankTransactionCommand request, CancellationToken cancellationToken)
        {
            //następuje operacja opłacenia na podstawie danych z karty bankomatowej i zostaje pobrana faktura z azure Blob i wysłana mailem

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_configuration.GetSection("BlobStorage").GetSection("storageConnection").Value);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(_configuration.GetSection("BlobStorage").GetSection("blobContainer").Value);
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(_configuration.GetSection("BlobStorage").GetSection("tempFileName").Value);

            MemoryStream memStream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(memStream);
            var strin64 = Convert.ToBase64String(memStream.ToArray());

            var resultMail = await _mailerService.SendMailWithAttachment(request.OwnerName, request.MailAddress, "JShop order has been paid", "Your order has been paid", "Faktura.pdf", strin64);

            return resultMail;
        }
    }
}
