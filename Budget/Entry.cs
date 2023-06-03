using System;

namespace Budget
{
    public class Entry
    {

        public string Name { get; set; }
        public string Type { get; set; }

        private int _money;
        public int Money
        {
            get { return _money; }
            set
            {
                IsReceiptOrDeduction = value >= 0;
                if (!IsReceiptOrDeduction)
                    _money = Math.Abs(value);
                else
                    _money = value;
            }
        }

        public bool IsReceiptOrDeduction { get; set; }
        public DateTime Date { get; set; }

        public Entry(string name, string type, int money, DateTime date)
        {
            Name = name;
            Type = type;
            Money = money;
            Date = date;
        }
    }
}
