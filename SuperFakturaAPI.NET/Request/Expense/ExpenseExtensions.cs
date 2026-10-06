using System;
using System.IO;

namespace Birko.SuperFaktura.Request.Expense
{
    public static class ExpenseExtensions
    {
        /// <summary>
        /// Reads the file at the specified path and stores its Base64 representation in the Attachment property.
        /// </summary>
        /// <param name="expense">The expense to modify.</param>
        /// <param name="filePath">Path to the file to attach.</param>
        public static void SetAttachmentFromFile(this Expense expense, string filePath)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path must be provided.", nameof(filePath));
            }
            var bytes = File.ReadAllBytes(filePath);
            expense.Attachment = Convert.ToBase64String(bytes);
        }
    }
}
