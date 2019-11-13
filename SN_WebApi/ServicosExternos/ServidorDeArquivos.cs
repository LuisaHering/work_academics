using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SN_WebApi.ServicosExternos
{
    public class ServidorDeArquivos
    {
        public void UploadDeArquivo(Stream reader, string nomeDoArquivo)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=gabrielcouto26;AccountKey=GVZpPG9E+BGF1jV8PkMTMOR9gOvEb0wbTQPVlA7Ea+PjbhuZf153uQwv/m5zqgY3kKwf38o9WdiljROtl/+fIg==;EndpointSuffix=core.windows.net";

            CloudStorageAccount cloudStorageAccount = null;

            CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("wa-profile-pics");

            container.CreateIfNotExists();

            var cloudBlockBlob = container.GetBlockBlobReference(nomeDoArquivo);

            cloudBlockBlob.UploadFromStream(reader);
        }

        public void UploadDeArquivo(FileStream reader, string nomeDoArquivo)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=gabrielcouto26;AccountKey=GVZpPG9E+BGF1jV8PkMTMOR9gOvEb0wbTQPVlA7Ea+PjbhuZf153uQwv/m5zqgY3kKwf38o9WdiljROtl/+fIg==;EndpointSuffix=core.windows.net";

            CloudStorageAccount cloudStorageAccount = null;

            CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("wa-profile-pics");

            container.CreateIfNotExists();

            var cloudBlockBlob = container.GetBlockBlobReference(nomeDoArquivo);

            cloudBlockBlob.UploadFromStream(reader);
        }
    }
}