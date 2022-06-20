namespace Birko.SuperFaktura.Request.Expense
{
    public class ExpenseData : Data
    {
        public Expense Expense { get; set; }
        public Extra ExpenseExtra { get; set; }
    }
}
