using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Azure.Management.DataFactories.Models;
using Microsoft.Azure.Management.DataFactories.Runtime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageEmulatorTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "UseDevelopmentStorage=true";

            // reference to storage based on connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // you must create a client based on the storage account, like below, to interact with it
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // reference the container you want
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("testcontainer");

            // create the container if needed
            // you will get an error here if the emulator is not working properly
            // most likely a "forbidden" error or a "bad request" error
            blobContainer.CreateIfNotExists();

            // create some new blobs in the container
            CloudBlockBlob newBlob = blobContainer.GetBlockBlobReference("testFolder/NewBlob.txt");
            CloudBlockBlob newBlob2 = blobContainer.GetBlockBlobReference("testFolder/NewBlob2.txt");
            CloudBlockBlob newBlob3 = blobContainer.GetBlockBlobReference("testFolder/NewBlob3.txt");

            // add some text to the new blobs
            newBlob.UploadText("This is some test text for our new blob.");
            newBlob2.UploadText("This is some test text for our new blob number 2.");
            newBlob3.UploadText("This is some test text for our new blob number 3.");

            // at this point, you should have blobs in your developement storage.
            // and these should be viewable from Azure Storage Explorer

            // write the blobs to a console window to make sure what we created can actually be found
            foreach (CloudBlockBlob blob in blobContainer.ListBlobs(useFlatBlobListing: true))
            {
                Console.WriteLine("New blob was created: " + blob.Uri);
            }

            Console.ReadLine();
        }
    }
}
