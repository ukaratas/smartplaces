using System;

namespace sp.iot.server
{
    public class Tank
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public double Consumption { get; set; }

        public bool IsActive => FinishDate != null;

    }
}
