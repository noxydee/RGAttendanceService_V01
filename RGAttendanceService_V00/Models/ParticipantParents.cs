using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGAttendanceService_V00.Models
{
    public class ParticipantParents
    {
        public int ParticipantId { get; set; }
        public Participant Kid { get; set; }

        public int ParentId { get; set; }
        public Parent Parent { get; set; }

    }
}
