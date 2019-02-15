/*==============================================================*/
/* Table: Task                                                  */
/*==============================================================*/
create table Task (
   Id                   int                  identity,
   UserId               int                  not null,
   Name                 nvarchar(100)        not null default '0',
   Description          nvarchar(max)        null default '0',
   StartDate            datetime             not null,
   EndDate              datetime             null default '0',
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_TASK primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexPlanUserId                                       */
/*==============================================================*/
create index IndexPlanUserId on Task (
UserId ASC
)
go

/*==============================================================*/
/* Index: IndexPlanStartDate                                    */
/*==============================================================*/
create index IndexPlanStartDate on Task (
StartDate ASC
)
go

/*==============================================================*/
/* Index: IndexPlanName                                         */
/*==============================================================*/
create index IndexPlanName on Task (
Name ASC
)
go

/*==============================================================*/
/* Table: "User"                                                */
/*==============================================================*/
create table "User" (
   Id                   int                  identity,
   FirstName            nvarchar(30)         not null,
   LastName             nvarchar(30)         not null,
   Email                nvarchar(100)        not null,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_USER primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexUserLastName                                     */
/*==============================================================*/
create index IndexUserLastName on "User" (
LastName ASC
)
go

/*==============================================================*/
/* Index: IndexUserEmail                                        */
/*==============================================================*/
create unique index IndexUserEmail on "User" (
Email ASC
)
go

alter table Task
   add constraint FK_TASK_REFERENCE_USER foreign key (UserId)
      references "User" (Id)
go
