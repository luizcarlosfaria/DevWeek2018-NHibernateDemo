using System;
using System.Collections.Generic;
using System.Text;

namespace DevWeek2018.Entities
{
    public interface IDomainEntity { }

    public partial class Classroom : IDomainEntity
    {
        public virtual int ClassroomId { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Student> Students { get; set; }
    }

    public partial class Language : IDomainEntity
    {
        public virtual string LanguageId { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Student> Students { get; set; }
    }

    public partial class Student : IDomainEntity
    {
        public virtual int StudentId { get; set; }

        public virtual string FullName { get; set; }

        public virtual IList<Classroom> Classrooms { get; set; }

        public virtual Language Language { get; set; }
    }
}
