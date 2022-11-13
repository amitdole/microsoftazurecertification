CREATE DATABASE appdb;

use appdb;

CREATE TABLE Products
(
ID int,
Name varchar(1000),
Quantity int
);

INSERT INTO Products VALUES (1, 'Tab', 2);
INSERT INTO Products VALUES (2, 'Mobile', 100);