namespace barefoot.finances.service.models
{
    public class PersonIncome
    {
        public string Name { get; set; }
        public SalaryType SalaryType { get; set; }
        public bool AutomaticSalary { get; set; }
        public double MonthlySalary { get; set; }
        public NotificationType NotificationType { get; set; }
        public string NotificationAddress { get; set; }
        public string PaymentDate { get; set; }
    }
}