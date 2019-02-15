/*==============================================================*/
/* Data Model: Art Shop 4.5                                     */
/*                                                              */
/* Copyright (C), Data & Object Factory, LLC                    */
/* All rights reserved. www.dofactory.com                       */
/*==============================================================*/

/*==============================================================*/
/* Table: Artist                                                */
/*==============================================================*/
create table Artist (
   Id                   int                  identity,
   FirstName            nvarchar(30)         not null,
   LastName             nvarchar(30)         not null,
   LifeSpan             nvarchar(30)         null,
   Country              nvarchar(30)         null,
   Description          nvarchar(500)        null,
   TotalProducts        int                  not null default 0,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_ARTIST primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexArtistLastName                                   */
/*==============================================================*/
create index IndexArtistLastName on Artist (
LastName ASC
)
go

/*==============================================================*/
/* Index: IndexArtistCountry                                    */
/*==============================================================*/
create index IndexArtistCountry on Artist (
Country ASC
)
go

/*==============================================================*/
/* Table: Cart                                                  */
/*==============================================================*/
create table Cart (
   Id                   int                  identity,
   Cookie               nvarchar(40)         not null,
   CartDate             datetime             not null default getdate(),
   ItemCount            int                  not null default 0,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_CART primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexCartCookie                                       */
/*==============================================================*/
create index IndexCartCookie on Cart (
Cookie ASC
)
go

/*==============================================================*/
/* Table: CartItem                                              */
/*==============================================================*/
create table CartItem (
   Id                   int                  identity,
   CartId               int                  not null,
   ProductId            int                  not null,
   Price                float                not null,
   Quantity             int                  not null default 1,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_CARTITEM primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexCartItemCartId                                   */
/*==============================================================*/
create index IndexCartItemCartId on CartItem (
CartId ASC
)
go

/*==============================================================*/
/* Table: Error                                                 */
/*==============================================================*/
create table Error (
   Id                   int                  identity,
   UserId               int                  null,
   ErrorDate            datetime             null default getdate(),
   IpAddress            nvarchar(40)         null,
   UserAgent            nvarchar(max)        null,
   Exception            nvarchar(max)        null,
   Message              nvarchar(max)        null,
   Everything           nvarchar(max)        null,
   HttpReferer          nvarchar(500)        null,
   PathAndQuery         nvarchar(500)        null,
   CreatedBy            int                  null,
   CreatedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   constraint PK_ERROR primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexErrorDate                                        */
/*==============================================================*/
create index IndexErrorDate on Error (
ErrorDate ASC
)
go

/*==============================================================*/
/* Table: "Order"                                               */
/*==============================================================*/
create table "Order" (
   Id                   int                  identity,
   UserId               int                  not null,
   OrderDate            datetime             not null,
   TotalPrice           float                not null default 0,
   OrderNumber          int                  not null default 0,
   ItemCount            int                  not null default 0,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_ORDER primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexOrderDate                                        */
/*==============================================================*/
create index IndexOrderDate on "Order" (
OrderDate ASC
)
go

/*==============================================================*/
/* Index: IndexOrderUserId                                      */
/*==============================================================*/
create index IndexOrderUserId on "Order" (
UserId ASC
)
go

/*==============================================================*/
/* Index: IndexOrderOrderNumber                                 */
/*==============================================================*/
create index IndexOrderOrderNumber on "Order" (
OrderNumber ASC
)
go

/*==============================================================*/
/* Table: OrderDetail                                           */
/*==============================================================*/
create table OrderDetail (
   Id                   int                  identity,
   OrderId              int                  not null,
   ProductId            int                  not null,
   Price                float                not null,
   Quantity             int                  not null default 1,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_ORDERDETAIL primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexOrderDetailOrderId                               */
/*==============================================================*/
create index IndexOrderDetailOrderId on OrderDetail (
OrderId ASC
)
go

/*==============================================================*/
/* Table: OrderNumber                                           */
/*==============================================================*/
create table OrderNumber (
   Id                   int                  identity,
   Number               int                  not null default 203317,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_ORDERNUMBER primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexOrderNumber                                      */
/*==============================================================*/
create index IndexOrderNumber on OrderNumber (
Number ASC
)
go

/*==============================================================*/
/* Table: Product                                               */
/*==============================================================*/
create table Product (
   Id                   int                  identity,
   Title                nvarchar(100)        not null,
   Description          nvarchar(250)        null,
   ArtistId             int                  not null,
   Image                nvarchar(30)         not null,
   Price                float                not null,
   QuantitySold         int                  not null default 0,
   AvgStars             float                not null default 0,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_PRODUCT primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexProductTitle                                     */
/*==============================================================*/
create index IndexProductTitle on Product (
Title ASC
)
go

/*==============================================================*/
/* Index: IndexProductArtistId                                  */
/*==============================================================*/
create index IndexProductArtistId on Product (
ArtistId ASC
)
go

/*==============================================================*/
/* Index: IndexProductPrice                                     */
/*==============================================================*/
create index IndexProductPrice on Product (
Price ASC
)
go

/*==============================================================*/
/* Index: IndexProductAvgStars                                  */
/*==============================================================*/
create index IndexProductAvgStars on Product (
AvgStars ASC
)
go

/*==============================================================*/
/* Index: IndexProductQuantitySold                              */
/*==============================================================*/
create index IndexProductQuantitySold on Product (
QuantitySold ASC
)
go

/*==============================================================*/
/* Table: Rating                                                */
/*==============================================================*/
create table Rating (
   Id                   int                  identity,
   UserId               int                  not null,
   ProductId            int                  not null,
   Stars                int                  not null,
   CreatedOn            datetime             not null default getdate(),
   CreatedBy            int                  null,
   ChangedOn            datetime             not null default getdate(),
   ChangedBy            int                  null,
   constraint PK_RATING primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IndexRatingUserId                                     */
/*==============================================================*/
create index IndexRatingUserId on Rating (
UserId ASC
)
go

/*==============================================================*/
/* Index: IndexRatingProductId                                  */
/*==============================================================*/
create index IndexRatingProductId on Rating (
ProductId ASC
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
   City                 nvarchar(30)         null,
   Country              nvarchar(30)         null,
   SignupDate           datetime             not null default getdate(),
   OrderCount           int                  not null default 0,
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

/*==============================================================*/
/* Index: IndexOrderCount                                       */
/*==============================================================*/
create index IndexOrderCount on "User" (
OrderCount ASC
)
go

alter table CartItem
   add constraint FK_CARTITEM_REFERENCE_CART foreign key (CartId)
      references Cart (Id)
go

alter table "Order"
   add constraint FK_ORDER_REFERENCE_USER foreign key (UserId)
      references "User" (Id)
go

alter table OrderDetail
   add constraint FK_ORDERDET_REFERENCE_ORDER foreign key (OrderId)
      references "Order" (Id)
go

alter table OrderDetail
   add constraint FK_ORDERDET_REFERENCE_PRODUCT foreign key (ProductId)
      references Product (Id)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_ARTIST foreign key (ArtistId)
      references Artist (Id)
go

alter table Rating
   add constraint FK_RATING_REFERENCE_USER foreign key (UserId)
      references "User" (Id)
go

alter table Rating
   add constraint FK_RATING_REFERENCE_PRODUCT foreign key (ProductId)
      references Product (Id)
go

-- and now the data

SET IDENTITY_INSERT [Artist] ON
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(1,'Paul','Cézanne','1839–1906','France','Paul Cézanne was a French artist and Post-Impressionist painter whose work laid the foundations of the transition from the 19th-century conception of artistic endeavour to a new and radically different world of art in the 20th century. Cézanne can be said to form the bridge between late 19th-century Impressionism and the early 20th century''s new line of artistic enquiry, Cubism. Both Matisse and Picasso are said to have remarked that Cézanne "is the father of us all."',5,'Mar 30 2013 11:25:34:993AM',1,'Mar 30 2013 11:25:34:993AM',NULL)
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(2,'Claude','Monet','1840-1926','France','Claude Monet was a founder of French impressionist painting, and the most consistent and prolific practitioner of the movement''s philosophy of expressing one''s perceptions before nature, especially as applied to plein-air landscape painting. The term Impressionism is derived from the title of his painting Impression, Sunrise (Impression, soleil levant).',3,'Mar 30 2013 11:26:29:840AM',1,'Mar 30 2013 11:26:29:840AM',NULL)
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(3,'Vincent','Van Gogh','1853-1890','Holland','Vincent Willem van Gogh was a Dutch post-Impressionist painter whose work, notable for its rough beauty, emotional honesty and bold color, had a far-reaching influence on 20th-century art. After years of painful anxiety and frequent bouts of mental illness, he died aged 37 from a gunshot wound, generally accepted to be self-inflicted (although no gun was ever found). His work was then known to only a handful of people and appreciated by fewer still.',4,'Mar 30 2013 11:27:57:660AM',1,'Mar 30 2013 11:27:57:660AM',NULL)
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(4,'Edgar','Degas','1834-1917','France','Edgar Degas was a French artist famous for his paintings, sculptures, prints, and drawings. He is especially identified with the subject of dance; more than half of his works depict dancers. He is regarded as one of the founders of Impressionism, although he rejected the term, and preferred to be called a realist. He was a superb draftsman, and particularly masterful in depicting movement, as can be seen in his renditions of dancers, racecourse subjects and female nudes.',4,'Mar 30 2013 11:28:08:870AM',1,'Mar 30 2013 11:28:08:870AM',NULL)
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(5,'Johannes','Vermeer','1632-1675','Holland','Johan Vermeer was a Dutch painter who specialized in domestic interior scenes of middle class life. Vermeer was a moderately successful provincial genre painter in his lifetime. He seems never to have been particularly wealthy, leaving his wife and children in debt at his death, perhaps because he produced relatively few paintings. Vermeer worked slowly and with great care, using bright colours and sometimes expensive pigments, with a preference for cornflower blue and yellow.',6,'Mar 30 2013 11:29:47:270AM',1,'Mar 30 2013 11:29:47:270AM',NULL)
INSERT INTO [artist] ([Id],[FirstName],[LastName],[LifeSpan],[Country],[Description],[TotalProducts],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(6,'William','Turner','1789-1862','England','William Turner was an English painter who specialised in watercolour landscapes. He was a contemporary of the more famous artist J. M. W. Turner and his style was not dissimilar. He is often known as William Turner of Oxford or just Turner of Oxford to distinguish him from his better known namesake. Many of Turner''s paintings depicted the countryside around Oxford. One of his best known pictures is a view of the city of Oxford from Hinksey Hill.',3,'Mar 30 2013  2:18:40:527PM',1,'Mar 30 2013  2:18:40:527PM',NULL)
SET IDENTITY_INSERT [Artist] OFF

go

SET IDENTITY_INSERT [Product] ON
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(1,'Apples, Peaches, Pears, and Grapes','Oil on Canvas, 37 x 48 cm',1,'cezanne1.jpg',4.850000000000000e+002,5.000000000000000e+000,7,'Mar 30 2013  2:48:38:497PM',1,'Mar 30 2013  2:48:38:497PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(2,'The Blue Vase','Oil on Canvas, 60 x 99 cm',1,'cezanne2.jpg',3.000000000000000e+002,4.300000000000000e+000,6,'Mar 30 2013  2:52:34:317PM',1,'Mar 30 2013  2:52:34:317PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(3,'Boy in a Red Vest (Garçon au gilet rouge)','Oil on Canvas, 45 x 88 cm',1,'cezanne3.jpg',4.250000000000000e+002,4.500000000000000e+000,6,'Mar 30 2013  3:12:37:147PM',1,'Mar 30 2013  3:12:37:147PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(4,'Le Mont Sainte-Victoire','Oil on Canvas, 80.9 x 100.5 cm (31 7/8 x 39 1/2 in)',1,'cezanne4.jpg',1.750000000000000e+002,4.700000000000000e+000,7,'Apr 14 2013 10:29:47:100PM',1,'Apr 14 2013 10:29:47:100PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(5,'Bibemus Quarry (Carrière de Bibemus)','Oil on canvas, 93 x 72.8 cm (36 1/4 x 28 5/8 in)',1,'cezanne5.jpg',3.950000000000000e+002,5.000000000000000e+000,8,'Mar 30 2013  3:44:33:023PM',1,'Mar 30 2013  3:44:33:023PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(6,'The Beach at Trouville','Oil on canvas, 38.1 x 46 cm (15 x 18 1/8 in)',2,'monet1.jpg',2.550000000000000e+002,4.200000000000000e+000,4,'Mar 30 2013  3:49:34:203PM',1,'Mar 30 2013  3:49:34:203PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(7,'The Boat Studio (Le bateau atelier)','Oil on canvas, 72 x 59.8 cm (28 3/8 x 23 1/2 in)',2,'monet2.jpg',4.250000000000000e+002,4.500000000000000e+000,6,'Mar 30 2013  3:53:07:883PM',1,'Mar 30 2013  3:53:07:883PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(8,'Houses of Parliament, London, Sun Breaking Through the Fog','Oil on canvas, 81 x 92 cm (31 7/8 x 36 1/4 in)',2,'monet3.jpg',4.550000000000000e+002,5.000000000000000e+000,6,'Mar 30 2013  3:56:24:793PM',1,'Mar 30 2013  3:56:24:793PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(9,'Vincent''s Room, Arles','Oil on canvas, 73 x 83.3 cm',3,'vangogh1.jpg',4.950000000000000e+002,5.000000000000000e+000,3,'Mar 30 2013  3:59:30:160PM',1,'Mar 30 2013  3:59:30:160PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(10,'Irises','Oil on canvas, 70 x 93 cm (28 x 36 3/4 in)`',3,'vangogh2.jpg',3.000000000000000e+002,4.200000000000000e+000,19,'Mar 30 2013  7:52:58:160PM',1,'Mar 30 2013  7:52:58:160PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(11,'Self-Portrait with Bandaged Ear','Oil on canvas, 60 x 49 cm',3,'vangogh3.jpg',2.250000000000000e+002,3.800000000000000e+000,4,'Mar 30 2013  7:59:09:603PM',1,'Mar 30 2013  7:59:09:603PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(12,'Peasant Man','Oil on canvas, 40 x 45.3 cm',3,'vangogh4.jpg',2.450000000000000e+002,3.900000000000000e+000,12,'Mar 30 2013  8:00:03:593PM',1,'Mar 30 2013  8:00:03:593PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(13,'Seated Dancer (Danseuse assise)','Charcoal and pastel on paper, 40 x 55 cm',4,'degas1.jpg',2.950000000000000e+002,4.100000000000000e+000,4,'Mar 30 2013  8:06:19:087PM',1,'Mar 30 2013  8:06:19:087PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(14,'The Dancing class (La classe de danse)','Oil on canvas, 85 x 74 cm (33 1/2 x 29 1/2 in)',4,'degas2.jpg',1.350000000000000e+002,3.900000000000000e+000,8,'Mar 30 2013  8:12:17:023PM',1,'Mar 30 2013  8:12:17:023PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(15,'Four Dancers','Oil on canvas, 151.1 x 179.2 cm (59 1/2 x 71 in)',4,'degas3.jpg',4.500000000000000e+002,4.600000000000000e+000,2,'Mar 30 2013  8:18:00:823PM',1,'Mar 30 2013  8:18:00:823PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(16,'L''absinthe','Oil on canvas, 90 x 120.1 cm',4,'degas4.jpg',1.250000000000000e+002,4.200000000000000e+000,5,'Mar 30 2013  8:20:03:480PM',1,'Mar 30 2013  8:20:03:480PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(17,'Girl with a Pearl Earring','Oil on canvas, 77.2 x 97.0 cm',5,'vermeer1.jpg',4.950000000000000e+002,5.000000000000000e+000,5,'Mar 30 2013  8:26:23:300PM',1,'Mar 30 2013  8:26:23:300PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(18,'Young Woman with a Water Pitcher','Oil on canvas, 48 x 55.9 cm',5,'vermeer2.jpg',4.500000000000000e+002,4.600000000000000e+000,4,'Mar 30 2013  8:28:26:990PM',1,'Mar 30 2013  8:28:26:990PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(19,'The Kitchen Maid','Oil on canvas, 45.5 x 41 cm',5,'vermeer3.jpg',4.500000000000000e+002,4.500000000000000e+000,6,'Mar 30 2013  8:33:44:787PM',1,'Mar 30 2013  8:33:44:787PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(20,'The Little Street','Oil on canvas, 54.3 x 44 cm',5,'vermeer4.jpg',4.950000000000000e+002,5.000000000000000e+000,5,'Mar 30 2013  8:34:31:283PM',1,'Mar 30 2013  8:34:31:283PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(21,'The Guitar Player','Oil on canvas, 53 x 46.3 cm',5,'vermeer5.jpg',4.150000000000000e+002,3.900000000000000e+000,17,'Mar 30 2013  8:39:07:853PM',1,'Mar 30 2013  8:39:07:853PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(22,'The Love Letter','Oil on canvas, 44 x 38.5 cm',5,'vermeer6.jpg',1.350000000000000e+002,4.000000000000000e+000,6,'Mar 30 2013  9:03:10:600PM',1,'Mar 30 2013  9:03:10:600PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(23,'The Fighting "Temeraire" tugged to her last berth to be broken up','Oil on canvas, 91 x 122 cm',6,'turner1.jpg',4.300000000000000e+002,4.600000000000000e+000,4,'Mar 30 2013  8:50:21:860PM',1,'Mar 30 2013  8:50:21:860PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(24,'Slavers throwing overboard the Dead and Dying','Oil on canvas, 90.8 x 122.6 cm',6,'turner2.jpg',4.500000000000000e+002,4.300000000000000e+000,7,'Mar 30 2013  8:52:59:570PM',1,'Mar 30 2013  8:52:59:570PM',NULL)
INSERT INTO [product] ([Id],[Title],[Description],[ArtistId],[Image],[Price],[AvgStars],[QuantitySold],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(25,'Peace: Burial at Sea','Oil on canvas, 86.9 x 86.6 cm',6,'turner3.jpg',3.950000000000000e+002,4.000000000000000e+000,5,'Mar 30 2013  9:01:22:733PM',1,'Mar 30 2013  9:01:22:733PM',NULL)
SET IDENTITY_INSERT [Product] OFF

go

SET IDENTITY_INSERT [User] ON
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(1,'Terry','Vandenberge','terryvb@bp.com','Houston','USA','Jan  1 2013 12:11:03:000AM',3,'Jan  1 2013 12:11:03:000AM',NULL,'Jan  1 2013 12:11:03:000AM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(3,'Debbie','Anderson','debbie@company.com','New York','USA','Jan  8 2013  9:46:32:000PM',0,'Jan  8 2013  9:46:32:000PM',NULL,'May  6 2013  3:05:56:097PM',3)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(4,'Timothy','King','timx@360market.com','Austin','USA','Jan 17 2013  9:46:32:000PM',4,'Jan 17 2013  9:46:32:000PM',NULL,'Jan 17 2013  9:46:32:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(15,'Henry','Liversidge','hlsidge@zappos.com','Flagstaff','USA','Jan 18 2013  9:46:32:000PM',3,'Jan 18 2013  9:46:32:000PM',NULL,'Jan 18 2013  9:46:32:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(16,'Loes','Derksen','jahoor@loeshier.nl','Groningen','Netherlands','Jan 29 2013  9:48:28:000PM',1,'Jan 29 2013  9:48:28:000PM',NULL,'Jan 29 2013  9:48:28:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(23,'Art','Lover','artlover@gmail.com','San Diego','USA','Feb  1 2013  4:50:04:000PM',5,'Feb  1 2013  4:50:04:000PM',NULL,'May  9 2013 11:05:40:510AM',23)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(24,'Joyce','Petrus','joyce@gmail.com','Columbus','USA','Feb  2 2013  1:19:32:000PM',1,'Feb  2 2013  1:19:32:000PM',NULL,'Feb  2 2013  1:19:32:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(25,'Charly','Guzman','chguz@amc.org','San Antonio','USA','Feb  6 2013  1:20:37:000PM',1,'Feb  6 2013  1:20:37:000PM',NULL,'Feb  6 2013  1:20:37:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(26,'Brandy','Geier','brand.g@hotmail.com','Chicago','USA','Feb 14 2013  1:21:25:000PM',0,'Feb 14 2013  1:21:25:000PM',NULL,'Feb 14 2013  1:21:25:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(27,'Sharon','Erickson','sharone@finnish.org','New York','USA','Feb 21 2013  1:21:57:000PM',5,'Feb 21 2013  1:21:57:000PM',NULL,'Feb 21 2013  1:21:57:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(28,'Helmut','Gantt','helmut@hgantt.com','Munich','Germany','Feb 22 2013  1:22:46:000PM',3,'Feb 22 2013  1:22:46:000PM',NULL,'Feb 22 2013  1:22:46:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(29,'Leticia','Costa','lhcosta@mezzo.br','Barbecena','Brazil','Feb 28 2013  1:23:51:000PM',1,'Feb 28 2013  1:23:51:000PM',NULL,'Feb 28 2013  1:23:51:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(30,'Indra','Van Vlijmen','indra1980@live.nl','Badhoevedorp','Netherlands','Mar  2 2013  1:24:54:000PM',5,'Mar  2 2013  1:24:54:000PM',NULL,'Mar  2 2013  1:24:54:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(31,'Klaas Jan','Handels','kjhandels@rietenmand.nl','Amsterdam','Netherlands','Mar  3 2013  1:25:28:000PM',0,'Mar  3 2013  1:25:28:000PM',NULL,'Mar  3 2013  1:25:28:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(32,'Shane','Lyon','shane90@yahoo.com','St Louis','USA','Mar  4 2013  1:26:14:000PM',2,'Mar  4 2013  1:26:14:000PM',NULL,'Mar  4 2013  1:26:14:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(33,'Ricky','Perez','rperez@deloitte.com','Chicago','USA','Mar  8 2013  1:26:43:000PM',1,'Mar  8 2013  1:26:43:000PM',NULL,'Mar  8 2013  1:26:43:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(34,'Phillip','Kuster','pkuster@volkswagen.de','Bonn','Germany','Mar  9 2013  1:27:43:000PM',2,'Mar  9 2013  1:27:43:000PM',NULL,'Mar  9 2013  1:27:43:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(35,'Thad','Vargas','vargast@google.com','San Mateo','USA','Mar 11 2013  1:28:20:000PM',2,'Mar 11 2013  1:28:20:000PM',NULL,'Mar 11 2013  1:28:20:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(36,'Sue','Nigel','snig@softscreen.com','Los Angeles','USA','Mar 12 2013  1:29:08:000PM',1,'Mar 12 2013  1:29:08:000PM',NULL,'Mar 12 2013  1:29:08:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(37,'Diana','Trommler','ditromm@hertz.com','Berlin','Germany','Mar 18 2013  1:29:46:000PM',2,'Mar 18 2013  1:29:46:000PM',NULL,'Mar 18 2013  1:29:46:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(38,'Hans','De Knegt','hansgriet@knegtje.nl','The Hague','Netherlands','Mar 22 2013  1:30:43:000PM',3,'Mar 22 2013  1:30:43:000PM',NULL,'Mar 22 2013  1:30:43:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(39,'Maria','McKnight','mariem12@yahoo.com','Houston','USA','Mar 28 2013  1:31:26:000PM',1,'Mar 28 2013  1:31:26:000PM',NULL,'Mar 28 2013  1:31:26:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(40,'Terry','Knight','thn@financehelp.com','Miami','USA','Apr  1 2013  1:32:01:000PM',2,'Apr  1 2013  1:32:01:000PM',NULL,'Apr  1 2013  1:32:01:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(41,'Rémy','Chauvin','remych@boule.fr','Lyon','France','Apr  2 2013  1:33:22:000PM',2,'Apr  2 2013  1:33:22:000PM',NULL,'Apr 28 2013 12:14:31:503AM',3)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(42,'Claudia','Gómez','gomezcl@terra.com.ar','Buenos Aires','Argentina','Apr  4 2013  1:38:06:000PM',0,'Apr  4 2013  1:38:06:000PM',NULL,'Apr  4 2013  1:38:06:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(43,'Jon','Halden','jonhalden@cosco.com','Seattle','USA','Apr  6 2013  1:41:41:000PM',1,'Apr  6 2013  1:41:41:000PM',NULL,'Apr  6 2013  1:41:41:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(44,'Núñez','Partida','npartida@bc.gov.br','Sao Paolo','Brazil','Apr  8 2013  1:43:08:000PM',0,'Apr  8 2013  1:43:08:000PM',NULL,'Apr  8 2013  1:43:08:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(46,'Michael','Hall','michaelh@hotmail.com','Buffalo','USA','Apr 11 2013  2:49:46:000PM',0,'Apr 11 2013  2:49:46:000PM',NULL,'Apr 11 2013  2:49:46:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(47,'Assunta','Pirozzi','assunta@tempori.com.ar','Cordoba','Argentina','Apr 13 2013  3:17:52:000PM',1,'Apr 13 2013  3:17:52:000PM',NULL,'Apr 13 2013  3:17:52:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(48,'Kauã','Melo','kmelo@yahoo.br','Rio de Janeiro','Brazil','Apr 14 2013  3:19:31:000PM',0,'Apr 13 2013  3:19:31:000PM',NULL,'Apr 13 2013  3:19:31:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(49,'Helen','Park','helen@parktech.com','Washington','USA','Apr 19 2013  3:20:27:000PM',0,'Apr 14 2013  3:20:27:000PM',NULL,'Apr 14 2013  3:20:27:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(50,'James','Flores','jamesf3@live.com','Atlanta','USA','Apr 22 2013  3:21:43:000PM',1,'Apr 18 2013  3:21:43:000PM',NULL,'Apr 18 2013  3:21:43:000PM',NULL)
INSERT INTO [User] ([Id],[FirstName],[LastName],[Email],[City],[Country],[SignupDate],[OrderCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(52,'Jose','Redden','joredden@netflix.com','San Francisco','USA','Apr 29 2013  3:24:30:000PM',0,'Apr 22 2013  3:24:30:000PM',NULL,'May  6 2013  3:05:45:420PM',3)
SET IDENTITY_INSERT [User] OFF

go

SET IDENTITY_INSERT [Order] ON
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(29,4,'Jan 31 2013 12:00:00:000AM',4.950000000000000e+002,203454,1,'Jan 31 2013 12:00:00:000AM',4,'Jan 31 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(30,4,'Feb  5 2013 12:00:00:000AM',7.500000000000000e+002,203455,2,'Feb  5 2013 12:00:00:000AM',3,'Feb  5 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(31,4,'Feb  6 2013 12:00:00:000AM',4.500000000000000e+002,203456,1,'Feb  6 2013 12:00:00:000AM',3,'Feb  6 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(32,1,'Feb  8 2013 12:00:00:000AM',9.000000000000000e+002,203457,1,'Feb  8 2013 12:00:00:000AM',1,'Feb  8 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(33,23,'Feb 13 2013 12:00:00:000AM',9.450000000000000e+002,203458,2,'Feb 13 2013 12:00:00:000AM',3,'Feb 13 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(34,1,'Feb 14 2013 12:00:00:000AM',7.900000000000000e+002,203459,1,'Feb 14 2013 12:00:00:000AM',1,'Feb 14 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(35,27,'Feb 28 2013 12:00:00:000AM',2.325000000000000e+003,203460,4,'Feb 28 2013 12:00:00:000AM',27,'Feb 28 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(36,15,'Mar  2 2013 12:00:00:000AM',6.000000000000000e+002,203461,1,'Mar  2 2013 12:00:00:000AM',15,'Mar  2 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(37,23,'Mar  4 2013 12:00:00:000AM',8.300000000000000e+002,203462,1,'Mar  4 2013 12:00:00:000AM',23,'Mar  4 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(38,27,'Mar  7 2013 12:00:00:000AM',4.820000000000000e+003,203463,9,'Mar  7 2013 12:00:00:000AM',27,'Mar  7 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(39,36,'Mar 13 2013 12:00:00:000AM',8.500000000000000e+002,203464,1,'Mar 13 2013 12:00:00:000AM',36,'Mar 13 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(40,30,'Mar 18 2013 12:00:00:000AM',4.850000000000000e+002,203465,1,'Mar 18 2013 12:00:00:000AM',30,'Mar 18 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(41,24,'Mar 19 2013 12:00:00:000AM',2.105000000000000e+003,203466,5,'Mar 19 2013 12:00:00:000AM',24,'Mar 19 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(42,30,'Mar 20 2013 12:00:00:000AM',5.900000000000000e+002,203467,1,'Mar 20 2013 12:00:00:000AM',30,'Mar 20 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(43,35,'Mar 21 2013 12:00:00:000AM',9.100000000000000e+002,203468,1,'Mar 21 2013 12:00:00:000AM',35,'Mar 21 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(44,4,'Mar 25 2013 12:00:00:000AM',1.675000000000000e+003,203469,3,'Mar 25 2013 12:00:00:000AM',4,'Mar 25 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(45,38,'Mar 26 2013 12:00:00:000AM',1.250000000000000e+002,203470,1,'Mar 26 2013 12:00:00:000AM',38,'Mar 26 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(46,28,'Mar 27 2013 12:00:00:000AM',4.500000000000000e+002,203471,1,'Mar 27 2013 12:00:00:000AM',28,'Mar 27 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(47,30,'Mar 28 2013 12:00:00:000AM',6.250000000000000e+002,203472,2,'Mar 28 2013 12:00:00:000AM',30,'Mar 28 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(48,16,'Mar 29 2013 12:00:00:000AM',4.300000000000000e+002,203473,1,'Mar 29 2013 12:00:00:000AM',16,'Mar 29 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(49,30,'Mar 30 2013 12:00:00:000AM',2.295000000000000e+003,203474,4,'Mar 30 2013 12:00:00:000AM',30,'Mar 30 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(50,15,'Mar 31 2013 12:00:00:000AM',3.500000000000000e+002,203475,1,'Mar 31 2013 12:00:00:000AM',15,'Mar 31 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(51,34,'Apr  1 2013 12:00:00:000AM',4.850000000000000e+002,203476,1,'Apr  1 2013 12:00:00:000AM',34,'Apr  1 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(52,40,'Apr  2 2013 12:00:00:000AM',9.550000000000000e+002,203477,2,'Apr  2 2013 12:00:00:000AM',40,'Apr  2 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(53,35,'Apr  3 2013 12:00:00:000AM',6.000000000000000e+002,203478,1,'Apr  3 2013 12:00:00:000AM',35,'Apr  3 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(54,27,'Apr  5 2013 12:00:00:000AM',4.150000000000000e+002,203479,1,'Apr  5 2013 12:00:00:000AM',27,'Apr  5 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(55,30,'Apr  6 2013 12:00:00:000AM',1.395000000000000e+003,203480,2,'Apr  6 2013 12:00:00:000AM',30,'Apr  6 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(56,37,'Apr  7 2013 12:00:00:000AM',1.125000000000000e+003,203481,2,'Apr  7 2013 12:00:00:000AM',37,'Apr  7 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(57,41,'Apr  8 2013 12:00:00:000AM',7.200000000000000e+002,203482,2,'Apr  8 2013 12:00:00:000AM',41,'Apr  8 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(58,25,'Apr  9 2013 12:00:00:000AM',3.035000000000000e+003,203483,7,'Apr  9 2013 12:00:00:000AM',3,'Apr  9 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(59,39,'Apr 10 2013 12:00:00:000AM',2.700000000000000e+002,203484,1,'Apr 10 2013 12:00:00:000AM',39,'Apr 10 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(60,32,'Apr 11 2013 12:00:00:000AM',8.500000000000000e+002,203485,1,'Apr 11 2013 12:00:00:000AM',32,'Apr 11 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(61,15,'Apr 13 2013 12:00:00:000AM',9.100000000000000e+002,203486,2,'Apr 13 2013 12:00:00:000AM',15,'Apr 13 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(62,34,'Apr 14 2013 12:00:00:000AM',4.900000000000000e+002,203487,1,'Apr 14 2013 12:00:00:000AM',34,'Apr 14 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(63,29,'Apr 15 2013 12:00:00:000AM',9.900000000000000e+002,203488,1,'Apr 15 2013 12:00:00:000AM',29,'Apr 15 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(64,37,'Apr 16 2013 12:00:00:000AM',2.495000000000000e+003,203489,5,'Apr 16 2013 12:00:00:000AM',37,'Apr 16 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(65,1,'Apr 17 2013 12:00:00:000AM',2.855000000000000e+003,203490,5,'Apr 17 2013 12:00:00:000AM',1,'Apr 17 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(66,38,'Apr 18 2013 12:00:00:000AM',1.595000000000000e+003,203491,3,'Apr 18 2013 12:00:00:000AM',38,'Apr 18 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(67,28,'Apr 19 2013 12:00:00:000AM',4.250000000000000e+002,203492,1,'Apr 19 2013 12:00:00:000AM',28,'Apr 19 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(68,33,'Apr 20 2013 12:00:00:000AM',1.350000000000000e+002,203493,1,'Apr 20 2013 12:00:00:000AM',33,'Apr 20 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(69,38,'Apr 21 2013 12:00:00:000AM',2.550000000000000e+002,203494,1,'Apr 21 2013 12:00:00:000AM',38,'Apr 21 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(70,41,'Apr 22 2013 12:00:00:000AM',4.500000000000000e+002,203495,1,'Apr 22 2013 12:00:00:000AM',41,'Apr 22 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(71,32,'Apr 23 2013 12:00:00:000AM',1.395000000000000e+003,203496,2,'Apr 23 2013 12:00:00:000AM',32,'Apr 23 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(72,27,'Apr 24 2013 12:00:00:000AM',2.600000000000000e+003,203497,5,'Apr 24 2013 12:00:00:000AM',27,'Apr 24 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(73,28,'Apr 25 2013 12:00:00:000AM',8.300000000000000e+002,203498,1,'Apr 25 2013 12:00:00:000AM',28,'Apr 25 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(74,27,'Apr 26 2013 12:00:00:000AM',4.900000000000000e+002,203499,1,'Apr 26 2013 12:00:00:000AM',27,'Apr 26 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(75,43,'Apr 27 2013 12:00:00:000AM',1.265000000000000e+003,203500,2,'Apr 27 2013 12:00:00:000AM',43,'Apr 27 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(76,50,'Apr 28 2013 12:00:00:000AM',1.350000000000000e+002,203501,1,'Apr 28 2013 12:00:00:000AM',50,'Apr 28 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(77,40,'Apr 29 2013 12:00:00:000AM',4.950000000000000e+002,203502,1,'Apr 29 2013 12:00:00:000AM',40,'Apr 29 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(78,47,'Apr 30 2013 12:00:00:000AM',4.250000000000000e+002,203503,1,'Apr 30 2013 12:00:00:000AM',47,'Apr 30 2013 12:00:00:000AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(79,23,'Apr 24 2013  4:10:14:610PM',1.145000000000000e+003,203504,2,'Apr 24 2013  4:10:14:610PM',23,'Apr 24 2013  4:10:14:610PM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(80,23,'Apr 26 2013 11:36:09:913AM',1.245000000000000e+003,203505,1,'Apr 26 2013 11:36:09:913AM',23,'Apr 26 2013 11:36:09:913AM',NULL)
INSERT INTO [order] ([Id],[UserId],[OrderDate],[TotalPrice],[OrderNumber],[ItemCount],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(81,23,'Apr 28 2013  9:39:41:920PM',1.015000000000000e+003,203506,2,'Apr 28 2013  9:39:41:920PM',23,'Apr 28 2013  9:39:41:920PM',NULL)
SET IDENTITY_INSERT [Order] OFF

go

SET IDENTITY_INSERT [OrderDetail] ON
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(43,29,9,4.950000000000000e+002,1,'Jan 31 2013 12:00:00:000AM',4,'Apr 22 2013  9:02:49:307PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(44,30,19,4.500000000000000e+002,1,'Feb  5 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:19:450PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(45,30,2,3.000000000000000e+002,1,'Feb  5 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:19:450PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(46,31,19,4.500000000000000e+002,1,'Feb  6 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:20:727PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(47,32,19,4.500000000000000e+002,2,'Feb  8 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:22:950PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(48,33,17,4.950000000000000e+002,1,'Feb 13 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:31:383PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(49,33,11,2.250000000000000e+002,2,'Feb 13 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:31:383PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(50,34,5,3.950000000000000e+002,2,'Feb 14 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:31:417PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(51,35,24,4.500000000000000e+002,2,'Feb 28 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:31:963PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(52,35,10,3.000000000000000e+002,2,'Feb 28 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:31:963PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(53,35,25,3.950000000000000e+002,1,'Feb 28 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:31:963PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(54,35,23,4.300000000000000e+002,1,'Feb 28 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:31:963PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(55,36,10,3.000000000000000e+002,2,'Mar  2 2013 12:00:00:000AM',15,'Apr 22 2013  9:03:32:097PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(56,37,21,4.150000000000000e+002,2,'Mar  4 2013 12:00:00:000AM',23,'Apr 22 2013  9:03:32:183PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(57,38,20,4.950000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(58,38,16,1.250000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(59,38,1,4.850000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(60,38,12,2.450000000000000e+002,1,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(61,38,17,4.950000000000000e+002,1,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(62,38,14,1.350000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(63,38,12,2.450000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(64,38,6,2.550000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(65,38,10,3.000000000000000e+002,2,'Mar  7 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:32:340PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(66,39,3,4.250000000000000e+002,2,'Mar 13 2013 12:00:00:000AM',36,'Apr 22 2013  9:03:32:673PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(67,40,1,4.850000000000000e+002,1,'Mar 18 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:32:940PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(68,41,6,2.550000000000000e+002,1,'Mar 19 2013 12:00:00:000AM',24,'Apr 22 2013  9:03:33:020PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(69,41,23,4.300000000000000e+002,1,'Mar 19 2013 12:00:00:000AM',24,'Apr 22 2013  9:03:33:020PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(70,41,17,4.950000000000000e+002,1,'Mar 19 2013 12:00:00:000AM',24,'Apr 22 2013  9:03:33:020PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(71,41,5,3.950000000000000e+002,2,'Mar 19 2013 12:00:00:000AM',24,'Apr 22 2013  9:03:33:020PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(72,41,22,1.350000000000000e+002,1,'Mar 19 2013 12:00:00:000AM',24,'Apr 22 2013  9:03:33:020PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(73,42,13,2.950000000000000e+002,2,'Mar 20 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:120PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(74,43,8,4.550000000000000e+002,2,'Mar 21 2013 12:00:00:000AM',35,'Apr 22 2013  9:03:33:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(75,44,15,4.500000000000000e+002,2,'Mar 25 2013 12:00:00:000AM',4,'Apr 22 2013  9:03:33:387PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(76,44,2,3.000000000000000e+002,2,'Mar 25 2013 12:00:00:000AM',4,'Apr 22 2013  9:03:33:387PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(77,44,4,1.750000000000000e+002,1,'Mar 25 2013 12:00:00:000AM',4,'Apr 22 2013  9:03:33:387PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(78,45,16,1.250000000000000e+002,1,'Mar 26 2013 12:00:00:000AM',38,'Apr 22 2013  9:03:33:450PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(79,46,18,4.500000000000000e+002,1,'Mar 27 2013 12:00:00:000AM',28,'Apr 22 2013  9:03:33:487PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(80,47,12,2.450000000000000e+002,2,'Mar 28 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:530PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(81,47,22,1.350000000000000e+002,1,'Mar 28 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:530PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(82,48,23,4.300000000000000e+002,1,'Mar 29 2013 12:00:00:000AM',16,'Apr 22 2013  9:03:33:587PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(83,49,17,4.950000000000000e+002,2,'Mar 30 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:650PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(84,49,13,2.950000000000000e+002,2,'Mar 30 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:650PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(85,49,10,3.000000000000000e+002,1,'Mar 30 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:650PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(86,49,21,4.150000000000000e+002,1,'Mar 30 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:33:650PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(87,50,4,1.750000000000000e+002,2,'Mar 31 2013 12:00:00:000AM',15,'Apr 22 2013  9:03:33:807PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(88,51,1,4.850000000000000e+002,1,'Apr  1 2013 12:00:00:000AM',34,'Apr 22 2013  9:03:33:907PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(89,52,16,1.250000000000000e+002,1,'Apr  2 2013 12:00:00:000AM',40,'Apr 22 2013  9:03:33:940PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(90,52,21,4.150000000000000e+002,2,'Apr  2 2013 12:00:00:000AM',40,'Apr 22 2013  9:03:33:940PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(91,53,10,3.000000000000000e+002,2,'Apr  3 2013 12:00:00:000AM',35,'Apr 22 2013  9:03:33:973PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(92,54,21,4.150000000000000e+002,1,'Apr  5 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:34:083PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(93,55,24,4.500000000000000e+002,2,'Apr  6 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:34:160PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(94,55,9,4.950000000000000e+002,1,'Apr  6 2013 12:00:00:000AM',30,'Apr 22 2013  9:03:34:160PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(95,56,11,2.250000000000000e+002,1,'Apr  7 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:207PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(96,56,19,4.500000000000000e+002,2,'Apr  7 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:207PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(97,57,22,1.350000000000000e+002,2,'Apr  8 2013 12:00:00:000AM',41,'Apr 22 2013  9:03:34:263PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(98,57,18,4.500000000000000e+002,1,'Apr  8 2013 12:00:00:000AM',41,'Apr 22 2013  9:03:34:263PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(99,58,18,4.500000000000000e+002,1,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(100,58,22,1.350000000000000e+002,2,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(101,58,25,3.950000000000000e+002,1,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(102,58,7,4.250000000000000e+002,1,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(103,58,14,1.350000000000000e+002,2,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(104,58,25,3.950000000000000e+002,1,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(105,58,21,4.150000000000000e+002,2,'Apr  9 2013 12:00:00:000AM',3,'Apr 22 2013  9:03:34:317PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(106,59,14,1.350000000000000e+002,2,'Apr 10 2013 12:00:00:000AM',39,'Apr 22 2013  9:03:34:427PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(107,60,7,4.250000000000000e+002,2,'Apr 11 2013 12:00:00:000AM',32,'Apr 22 2013  9:03:34:463PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(108,61,1,4.850000000000000e+002,1,'Apr 13 2013 12:00:00:000AM',15,'Apr 22 2013  9:03:34:550PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(109,61,7,4.250000000000000e+002,1,'Apr 13 2013 12:00:00:000AM',15,'Apr 22 2013  9:03:34:550PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(110,62,12,2.450000000000000e+002,2,'Apr 14 2013 12:00:00:000AM',34,'Apr 22 2013  9:03:34:607PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(111,63,20,4.950000000000000e+002,2,'Apr 15 2013 12:00:00:000AM',29,'Apr 22 2013  9:03:34:650PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(112,64,11,2.250000000000000e+002,1,'Apr 16 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:683PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(113,64,24,4.500000000000000e+002,1,'Apr 16 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:683PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(114,64,5,3.950000000000000e+002,2,'Apr 16 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:683PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(115,64,2,3.000000000000000e+002,2,'Apr 16 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:683PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(116,64,23,4.300000000000000e+002,1,'Apr 16 2013 12:00:00:000AM',37,'Apr 22 2013  9:03:34:683PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(117,65,3,4.250000000000000e+002,1,'Apr 17 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:34:797PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(118,65,1,4.850000000000000e+002,2,'Apr 17 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:34:797PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(119,65,8,4.550000000000000e+002,1,'Apr 17 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:34:797PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(120,65,21,4.150000000000000e+002,2,'Apr 17 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:34:797PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(121,65,4,1.750000000000000e+002,1,'Apr 17 2013 12:00:00:000AM',1,'Apr 22 2013  9:03:34:797PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(122,66,8,4.550000000000000e+002,1,'Apr 18 2013 12:00:00:000AM',38,'Apr 22 2013  9:03:34:883PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(123,66,4,1.750000000000000e+002,2,'Apr 18 2013 12:00:00:000AM',38,'Apr 22 2013  9:03:34:883PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(124,66,25,3.950000000000000e+002,2,'Apr 18 2013 12:00:00:000AM',38,'Apr 22 2013  9:03:34:883PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(125,67,3,4.250000000000000e+002,1,'Apr 19 2013 12:00:00:000AM',28,'Apr 22 2013  9:03:34:950PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(126,68,14,1.350000000000000e+002,1,'Apr 20 2013 12:00:00:000AM',33,'Apr 22 2013  9:03:34:983PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(127,69,6,2.550000000000000e+002,1,'Apr 21 2013 12:00:00:000AM',38,'Apr 22 2013  9:03:35:030PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(128,70,18,4.500000000000000e+002,1,'Apr 22 2013 12:00:00:000AM',41,'Apr 22 2013  9:03:35:073PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(129,71,20,4.950000000000000e+002,1,'Apr 23 2013 12:00:00:000AM',32,'Apr 22 2013  9:03:35:120PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(130,71,24,4.500000000000000e+002,2,'Apr 23 2013 12:00:00:000AM',32,'Apr 22 2013  9:03:35:120PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(131,72,3,4.250000000000000e+002,1,'Apr 24 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(132,72,8,4.550000000000000e+002,2,'Apr 24 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(133,72,2,3.000000000000000e+002,1,'Apr 24 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(134,72,5,3.950000000000000e+002,2,'Apr 24 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(135,72,4,1.750000000000000e+002,1,'Apr 24 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:173PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(136,73,21,4.150000000000000e+002,2,'Apr 25 2013 12:00:00:000AM',28,'Apr 22 2013  9:03:35:230PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(137,74,12,2.450000000000000e+002,2,'Apr 26 2013 12:00:00:000AM',27,'Apr 22 2013  9:03:35:320PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(138,75,21,4.150000000000000e+002,1,'Apr 27 2013 12:00:00:000AM',43,'Apr 22 2013  9:03:35:373PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(139,75,7,4.250000000000000e+002,2,'Apr 27 2013 12:00:00:000AM',43,'Apr 22 2013  9:03:35:373PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(140,76,14,1.350000000000000e+002,1,'Apr 28 2013 12:00:00:000AM',50,'Apr 22 2013  9:03:35:430PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(141,77,9,4.950000000000000e+002,1,'Apr 29 2013 12:00:00:000AM',40,'Apr 22 2013  9:03:35:463PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(142,78,3,4.250000000000000e+002,1,'Apr 30 2013 12:00:00:000AM',47,'Apr 22 2013  9:03:35:507PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(143,79,12,2.450000000000000e+002,1,'Apr 24 2013  4:10:14:907PM',23,'Apr 24 2013  4:10:14:907PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(144,79,10,3.000000000000000e+002,3,'Apr 24 2013  4:10:14:907PM',23,'Apr 24 2013  4:10:14:907PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(145,80,21,4.150000000000000e+002,3,'Apr 26 2013 11:36:10:150AM',23,'Apr 26 2013 11:36:10:150AM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(146,81,10,3.000000000000000e+002,2,'Apr 28 2013  9:39:42:183PM',23,'Apr 28 2013  9:39:42:183PM',NULL)
INSERT INTO [orderdetail] ([Id],[OrderId],[ProductId],[Price],[Quantity],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(147,81,21,4.150000000000000e+002,1,'Apr 28 2013  9:39:42:187PM',23,'Apr 28 2013  9:39:42:187PM',NULL)
SET IDENTITY_INSERT [OrderDetail] OFF

go

SET IDENTITY_INSERT [OrderNumber] ON
INSERT INTO [OrderNumber] ([Id],[Number],[CreatedOn],[CreatedBy],[ChangedOn],[ChangedBy])VALUES(1,203317,'May 31 2013 12:49:06:667PM',NULL,'May 31 2013 12:49:06:667PM',NULL)
SET IDENTITY_INSERT [OrderNumber] OFF

go