using System;
using System.Windows;

namespace Task1.Model
{
    public class User
    {
        private DateTime date;

        public int UserId { get; set; }

        public string Date
        {
            get
            {
                if (date != DateTime.MinValue)
                    return date.ToShortDateString();
                return string.Empty;
            }
            set
            {
                try
                {
                    date = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid date format!");
                }
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SurName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
