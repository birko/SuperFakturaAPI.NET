using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Birko.SuperFaktura
{
    // invoice.md "Peppol status webhook": helpers for the receiver of the webhook_url passed to Invoices.SendPeppol.
    public static class PeppolWebhook
    {
        public const string StatusChangedEventType = "invoice.peppol.status_changed";

        public const string IdempotencyKeyHeader = "Idempotency-Key";
        public const string WebhookIdHeader = "X-SuperFaktura-Webhook-Id";
        public const string TimestampHeader = "X-SuperFaktura-Webhook-Timestamp";
        // Present only when a webhook_secret was sent.
        public const string SignatureHeader = "X-SuperFaktura-Webhook-Signature";

        private const string SignaturePrefix = "sha256=";

        public static PeppolWebhookEvent Parse(string body)
        {
            try
            {
                return JsonConvert.DeserializeObject<PeppolWebhookEvent>(body);
            }
            catch (Exception ex)
            {
                throw new Exceptions.ParseException(ex.Message, string.Format("Webhook body: {0}", body), ex);
            }
        }

        // Pass the raw request body; re-serialized JSON will not match the signature.
        public static bool VerifySignature(string timestamp, string body, string signature, string secret)
        {
            return VerifySignature(timestamp, body == null ? null : Encoding.UTF8.GetBytes(body), signature, secret);
        }

        // signature = "sha256=" + lowercase hex HMAC-SHA256(secret, timestamp + "." + raw body bytes).
        public static bool VerifySignature(string timestamp, byte[] body, string signature, string secret)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("The webhook_secret sent with Invoices.SendPeppol is required.", nameof(secret));
            }
            if (string.IsNullOrEmpty(timestamp) || body == null || string.IsNullOrEmpty(signature))
            {
                return false;
            }

            var prefix = Encoding.UTF8.GetBytes(timestamp + ".");
            var message = new byte[prefix.Length + body.Length];
            Buffer.BlockCopy(prefix, 0, message, 0, prefix.Length);
            Buffer.BlockCopy(body, 0, message, prefix.Length, body.Length);

            byte[] hash;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                hash = hmac.ComputeHash(message);
            }
            var expected = new StringBuilder(SignaturePrefix, SignaturePrefix.Length + hash.Length * 2);
            foreach (var b in hash)
            {
                expected.Append(b.ToString("x2"));
            }
            return FixedTimeEquals(expected.ToString(), signature);
        }

        // CryptographicOperations.FixedTimeEquals is not available in netstandard2.0.
        private static bool FixedTimeEquals(string expected, string actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }
            var diff = 0;
            for (var i = 0; i < expected.Length; i++)
            {
                diff |= expected[i] ^ actual[i];
            }
            return diff == 0;
        }
    }
}
