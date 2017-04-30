create table authors
(
   author_id            int not null,
   name                 varchar(256),
   email                varchar(256),
   primary key (author_id)
);

create table categories
(
   category_id          int not null,
   name                 varchar(256),
   primary key (category_id)
);

create table books
(
   isbn                 int not null,
   category_id          int,
   title                text,
   photo                varchar(256),
   publish_date         date,
   price                float,
   qty                  int,
   primary key (isbn)
);

create table books_authors
(
   isbn                 int not null,
   author_id            int not null,
   primary key (isbn, author_id)
);

alter table books add constraint FK_REFERENCE_2 foreign key (category_id) references categories (category_id) on delete restrict on update restrict;

alter table books_authors add constraint FK_REFERENCE_3 foreign key (isbn)
references books (isbn) on delete restrict on update restrict;

alter table books_authors add constraint FK_REFERENCE_4 foreign key (author_id) references authors (author_id) on delete restrict on update restrict;

create table users
(
   username             varchar(50) not null,
   roleid               int,
   email                varbinary(256),
   password             varchar(256),
   fullname             varbinary(256),
   primary key (username)
);

create table roles
(
   roleid               int not null,
   rolename             varchar(256),
   primary key (roleid)
);

alter table users add constraint FK_REFERENCE_5 foreign key (roleid) references roles (roleid) on delete restrict on update restrict;
