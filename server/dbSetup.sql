-- Active: 1711167878661@@127.0.0.1@3306@adaptable_shaman_540684_db
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE albums(
  id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(60) NOT NULL,
  category ENUM("misc", "cats", "dogs", "games", "gachamon", "capybaras", "hamburgers", "animals") DEFAULT "capybaras",
  archived BOOLEAN NOT NULL DEFAULT false,
  coverImg VARCHAR(1000) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)



DROP TABLE albums;


SELECT 
album.*, 
account.* 
FROM albums album 
JOIN accounts account 
ON album.creatorId = account.id;