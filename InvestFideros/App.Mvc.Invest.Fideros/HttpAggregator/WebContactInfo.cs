using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Mvc.Invest.Fideros.HttpAggregator
{
    public class WebContactInfo
    {
        public WebContactInfo()
        {
            AttachNames = new List<string?>{ };
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Reference { get; set; }
        public string? Title { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Position { get; set; }
        public string? PreferredLanguage { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyLegalForm { get; set; }
        public string? CompanyHoldingName { get; set; }
        public string? CompanyRegisterNumber { get; set; }
        public string? CompanyVATNumber { get; set; }
        public string? ActivityDescription { get; set; }
        public string? ActivityCode { get; set; }
        public string? SectorDescription { get; set; }
        public string? SectorCode { get; set; }
        public string? WebSite { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressPostalCode { get; set; }
        public string? AddressPostOfficeBox { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? CountryCode { get; set; }
        public Nullable<Int16> Employee { get; set; }
        public string? ContactPreference { get; set; }
        public string? SubscriptionType1 { get; set; }
        public string? SubscriptionType2 { get; set; }
        public string? SubscriptionType3 { get; set; }
        public Nullable<bool> ReceiveInfo { get; set; }
        public Nullable<bool> ReceivePartnerInfo { get; set; }
        public Nullable<bool> GiveMeCall { get; set; }
        public Nullable<bool> SendCatalog { get; set; }
        public string? CallPeriod { get; set; }
        public string? ProcessingStatus { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string? Message { get; set; }
        public string? Password { get; set; }
        public string? Identifier { get; set; }
        public string? PassQuestion1 { get; set; }
        public string? PassQuestion2 { get; set; }
        public string? PassAnswer1 { get; set; }
        public string? PassAnswer2 { get; set; }
        public string? Origin { get; set; }
        public List<string?> AttachNames { get; set; }
        public string? MessageType { get; set; }

    }

    public class Attachment
    {
        public Attachment()
        {
            FileStream = new byte[] { };
        }
        [JsonConverter(typeof(JsonToByteArrayConverter))]
        public byte[] FileStream { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }

    public class BinaryData
    {
        public byte[] StreamData { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
    }

    internal sealed class JsonToByteArrayConverter : JsonConverter<byte[]?>
    {
        // Converts base64 encoded string to byte[].
        public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!reader.TryGetBytesFromBase64(out byte[]? result) || result == default)
            {
                //throw new Exception("Add your fancy exception message here...");
            }
            return result;
        }

        // Converts byte[] to base64 encoded string.
        public override void Write(Utf8JsonWriter writer, byte[]? value, JsonSerializerOptions options)
        {
            writer.WriteBase64StringValue(value);
        }
    }

}