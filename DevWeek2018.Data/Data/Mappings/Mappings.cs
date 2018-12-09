using DevWeek2018.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevWeek2018.Data.Mappings
{
    public partial class ClassroomMapper : ClassMap<Classroom>
    {

        partial void CompleteMappings();

        public ClassroomMapper()
        {
            Table("CLASSROOM");

            OptimisticLock.None();

            DynamicUpdate();
            Id(it => it.ClassroomId, "CLASSROOM_ID").GeneratedBy.Native("CLASSROOM_SEQ").Index("PK_CLASSROOM").CustomSqlType("int");
            HasManyToMany(x => x.Students)
                .ParentKeyColumns.Add("CLASSROOM_ID")
                .Table("STUDENT_CLASSROOM")
                .ChildKeyColumns.Add("STUDENT_ID")
                .ForeignKeyConstraintNames("FK_CLASSROOM_TO_STUDENT_CLASSROOM", "FK_STUDENT_TO_STUDENT_CLASSROOM")
                .LazyLoad()
                .Fetch.Select()
                .AsBag();
            Map(it => it.Name, "NAME").Not.Nullable().CustomSqlType("VARCHAR(100)");
            this.CompleteMappings();
        }

    }


    public partial class StudentMapper : ClassMap<Student>
    {

        partial void CompleteMappings();

        public StudentMapper()
        {
            Table("STUDENT");

            OptimisticLock.None();

            DynamicUpdate();
            Id(it => it.StudentId, "STUDENT_ID").GeneratedBy.Native("STUDENT_SEQ").Index("PK_STUDENT").CustomSqlType("int");
            HasManyToMany(x => x.Classrooms)
                .ParentKeyColumns.Add("STUDENT_ID")
                .Table("STUDENT_CLASSROOM")
                .ChildKeyColumns.Add("CLASSROOM_ID")
                .LazyLoad()
                .Fetch.Join()
                .AsBag();
            References(x => x.Language)
                .ForeignKey("FK_LANGUAGE_TO_STUDENT")
                .Columns("LANGUAGE_ID")
                .Fetch.Join()
                .Cascade.None();
            Map(it => it.FullName, "FULL_NAME").Not.Nullable().CustomSqlType("VARCHAR(300)");
            this.CompleteMappings();
        }

    }


    public partial class LanguageMapper : ClassMap<Language>
    {

        partial void CompleteMappings();

        public LanguageMapper()
        {
            Table("LANGUAGE");

            OptimisticLock.None();

            DynamicUpdate();
            Id(it => it.LanguageId, "LANGUAGE_ID").CustomSqlType("CHAR(2)").Index("PK_STUDENT");
            HasMany(x => x.Students)
                .KeyColumns.Add("LANGUAGE_ID")
                .ForeignKeyConstraintName("FK_LANGUAGE_TO_STUDENT")
                .Inverse()
                .Cascade.Delete()
                .LazyLoad()
                .Fetch.Select()
                .AsBag();
            Map(it => it.Name, "NAME").Not.Nullable().CustomSqlType("VARCHAR(300)");
            this.CompleteMappings();
        }

    }
}
