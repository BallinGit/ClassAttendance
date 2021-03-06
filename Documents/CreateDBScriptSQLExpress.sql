USE [master]
GO
/****** Object:  Database [ClassAttendanceDB]    Script Date: 2021-03-02 10:10:56 AM ******/
CREATE DATABASE [ClassAttendanceDB]
 
GO

USE [ClassAttendanceDB]
GO

/****** Object:  Table [dbo].[Attendance]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[AttendanceID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NULL,
	[StudentID] [int] NULL,
	[AttendanceDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Class]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](50) NULL,
	[GradeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Grade](
	[GradeID] [int] IDENTITY(1,1) NOT NULL,
	[GradeName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[GradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentFullName] [varchar](250) NULL,
	[ClassID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Term]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Term](
	[TermID] [int] IDENTITY(1,1) NOT NULL,
	[TermName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [ClassAttendanceDB] SET  READ_WRITE 
GO

/****** populate data tables ******/
truncate table Grade
go
insert into grade(gradename) values('Grade 8')
insert into grade(gradename) values('Grade 9')
insert into grade(gradename) values('Grade 10')
insert into grade(gradename) values('Grade 11')
insert into grade(gradename) values('Grade 12')
go

truncate table Term
go

insert into Term(TermName) values ('Term 1')
insert into Term(TermName) values ('Term 2')
insert into Term(TermName) values ('Term 3')
insert into Term(TermName) values ('Term 4')
go


/****** Object:  StoredProcedure [dbo].[GetReportByTerm]    Script Date: 2021-03-02 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetReportByTerm]
	@StartTime as DateTime, 
	@EndTime as DateTime
	
AS
	
with cteClassCount as
(
select c.ClassName,g.GradeName,
cast(a.AttendanceDateTime as date) as wholeDate,
count(a.AttendanceID) as record_count

from Attendance a
left join Class c on c.ClassID = a.ClassID
left join Grade g on g.GradeID = c.GradeID
where cast(a.AttendanceDateTime as date) between cast(@StartTime as date) and cast(@EndTime as date)
group by c.ClassName,g.GradeName, cast(a.AttendanceDateTime as date)
),
 cteStudentCount as
(
select s.StudentFullName,c.ClassName,g.GradeName,
cast(a.AttendanceDateTime as date) as wholeDate,
count(a.AttendanceID) as record_count

from Attendance a
left join Student s on s.StudentID = a.StudentID
left join Class c on c.ClassID = a.ClassID
left join Grade g on g.GradeID = c.GradeID
where cast(a.AttendanceDateTime as date) between cast(@StartTime as date) and cast(@EndTime as date)
group by s.StudentFullName,c.ClassName,g.GradeName, cast(a.AttendanceDateTime as date)
)
, cteClassesTotal as
(
select ClassName,GradeName,count(*) as ClassesByDay from cteClassCount 
group by className,GradeName

), cteStudentsTotal as
(
select StudentFullName,ClassName,GradeName,count(*) as StudentByDay from cteStudentCount 
group by StudentFullName,ClassName,GradeName

)

select s.ClassName,s.GradeName,s.StudentFullName, StudentByDay as ClassesAttended, (ClassesByDay - StudentByDay) as ClassesMissed from cteStudentsTotal s
left join cteClassesTotal c on c.ClassName = s.ClassName
order by s.ClassName
GO