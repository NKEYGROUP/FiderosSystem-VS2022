using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Mvc.Invest.Fideros.HttpAggregator
{
    public interface IWebInfoApiClient
    {
        void CreateWebContactAsync(WebContactInfo webContact);
        void UpdateWebContactAsync(WebContactInfo webContactInfo);
        Task<WebContactInfo> GetWebContactAsync(string objectId);
        Task<List<WebContactInfo>> GetAllWebContactAsync();
        void DeleteWebContactAsync(string objectId);
        Task<string> UpLoadBinaryDataAsync(BinaryData binaryData);
        Task<byte[]> GetBinaryDataAsync(string objectId);
        void DeleteBinaryDataAsync(string objectId);
    }
}
