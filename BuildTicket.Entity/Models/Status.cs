using System;
using System.Collections.Generic;
using System.Text;

namespace BuildTicket.Entity.Models
{
    #region Status
    public class Status
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    #endregion
}
