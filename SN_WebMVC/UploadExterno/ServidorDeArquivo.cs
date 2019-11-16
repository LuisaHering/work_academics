using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SN_WebMVC.UploadExterno {
    public class ServidorDeArquivo {

        private static string connection = @"DefaultEndpointsProtocol=https;AccountName=bankcarlos;AccountKey=ZLhWijJnGIexBsget05vIK8XhgT/zI5xJMSnonsI/+qqv9lojHBi9V5O7OJUEB0kNe+otzxYMBnBPywHjOpnFQ==;EndpointSuffix=core.windows.net";

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