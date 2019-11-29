using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SN_WebMVC.UploadExterno {
    public class ServidorDeArquivo {

        private static string connection = @"DefaultEndpointsProtocol=https;AccountName=gabrielcouto26;AccountKey=GVZpPG9E+BGF1jV8PkMTMOR9gOvEb0wbTQPVlA7Ea+PjbhuZf153uQwv/m5zqgY3kKwf38o9WdiljROtl/+fIg==;EndpointSuffix=core.windows.net";

        public void UploadDeArquivo(FileStream reader, string nomeDoArquivo) {

            CloudStorageAccount cloudStorageAccount = null;

            CloudStorageAccount.TryParse(connection, out cloudStorageAccount);

            var CloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var Countainer = CloudBlobClient.GetContainerReference("api-amigo-fotos");

            Countainer.CreateIfNotExists();

            var cloudBlockBlob = Countainer.GetBlockBlobReference(nomeDoArquivo);

            cloudBlockBlob.UploadFromStream(reader);
        }

        public void UploadDeArquivo(Stream reader, string nomeDoArquivo) {

            CloudStorageAccount cloudStorageAccount = null;

            CloudStorageAccount.TryParse(connection, out cloudStorageAccount);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("api-amigo-fotos");

            container.CreateIfNotExists();

            var cloudBlockBlob = container.GetBlockBlobReference(nomeDoArquivo);

            cloudBlockBlob.UploadFromStream(reader);
        }
    }
}