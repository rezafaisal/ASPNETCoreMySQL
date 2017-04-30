create table guestbooks(
  guestbook_id INT NOT NULL AUTO_INCREMENT,
  guest_name VARCHAR(100) NOT NULL,
  guest_email VARCHAR(100) NOT NULL,
  message VARCHAR(256) NOT NULL,
  PRIMARY KEY ( guestbook_id )
);
