
--Create database eBookStore;
use eBookStore ;
CREATE TABLE [Role] (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name nVARCHAR(255) NULL UNIQUE
);
CREATE TABLE Publisher (
    pub_id INT PRIMARY KEY IDENTITY(1,1),
    publisher_name NVARCHAR(255)  NULL UNIQUE,
	city nVARCHAR(255)  NULL ,
	[state] nVARCHAR(255)  NULL ,
	country nVARCHAR(255)  NULL ,
);
CREATE TABLE [User] (
    [user_id] INT PRIMARY KEY IDENTITY(1,1),
    email_address VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
	source NVARCHAR(255)  NULL ,
	full_name nVARCHAR(255)  NULL ,
    role_id INT,
	hire_date date null ,
	pub_id INT ,
    FOREIGN KEY (role_id) REFERENCES Role(role_id),
	FOREIGN KEY (pub_id) REFERENCES Publisher(pub_id)
);



CREATE TABLE Book (
    book_id INT PRIMARY KEY IDENTITY(1,1),
    bookName nVARCHAR(255)  NULL,
    [type] nVARCHAR(50),
    pub_id INT,
    price DECIMAL(10, 2),
    advance DECIMAL(10, 2),
    royalty DECIMAL(10, 2),
    ytd_sales INT,
    notes TEXT,
    publisher_date DATE,
    FOREIGN KEY (pub_id) REFERENCES Publisher(pub_id)
);

CREATE TABLE Author (
    author_id INT PRIMARY KEY IDENTITY(1,1),
	full_name nVARCHAR(255)  NULL ,
	phone VARCHAR(255)  NULL ,
	[address] nVARCHAR(255)  NULL ,
	city nVARCHAR(255)  NULL ,
	[state] nVARCHAR(255)  NULL ,
	zip nVARCHAR(20),
	email_address varchar(255) ,
  
);

CREATE TABLE BookAuthor (
    book_id INT,
    author_id INT,
	author_order INT,
    PRIMARY KEY (book_id, author_id),
    FOREIGN KEY (book_id) REFERENCES Book(book_id),
    FOREIGN KEY (author_id) REFERENCES Author(author_id)
);
INSERT INTO [Role] (role_name) VALUES 
('Admin'),
('Author'),
('Publisher'),
('Reader');




INSERT INTO Publisher (publisher_name, city, [state], country) VALUES 
('Pearson', 'London', 'England', 'UK'),
('Penguin Random House', 'New York', 'NY', 'USA'),
('HarperCollins', 'New York', 'NY', 'USA'),
('Macmillan Publishers', 'London', 'England', 'UK');

INSERT INTO Book (bookName, [type], pub_id, price, advance, royalty, ytd_sales, notes, publisher_date) VALUES 
('Introduction to SQL', 'Educational', 1, 59.99, 5000.00, 10.00, 15000, 'A comprehensive guide to SQL.', '2023-02-01'),
('Mastering Python', 'Educational', 2, 49.99, 3000.00, 12.00, 20000, 'An advanced book on Python programming.', '2023-03-15'),
('Data Science with R', 'Educational', 3, 45.00, 4000.00, 15.00, 10000, 'Learn data science using R.', '2023-01-20'),
('The Art of War', 'History', 4, 19.99, 1500.00, 8.00, 5000, 'Ancient Chinese military treatise.', '2022-12-10');


INSERT INTO Author (full_name, phone, [address], city, [state], zip, email_address) VALUES 
('Alice Walker', '123-456-7890', '123 Main St', 'New York', 'NY', '10001', 'alice.walker@example.com'),
('Robert Jordan', '234-567-8901', '456 Oak St', 'Los Angeles', 'CA', '90001', 'robert.jordan@example.com'),
('Mary Shelley', '345-678-9012', '789 Pine St', 'London', 'England', 'EC1A', 'mary.shelley@example.com'),
('Leo Tolstoy', '456-789-0123', '101 Maple St', 'Moscow', 'Moscow', '101000', 'leo.tolstoy@example.com');


INSERT INTO BookAuthor (book_id, author_id, author_order) VALUES 
(1, 1, 1),  -- Alice Walker là tác giả của "Introduction to SQL"
(2, 2, 1),  -- Robert Jordan là tác giả của "Mastering Python"
(3, 3, 1),  -- Mary Shelley là tác giả của "Data Science with R"
(4, 4, 1);  -- Leo Tolstoy là tác giả của "The Art of War"


INSERT INTO [User] (email_address, password, source, full_name, role_id, hire_date, pub_id) VALUES 
('admin@ebookstore.com', 'admin123', 'internal', 'John Doe', 1, '2023-01-01', NULL),
('author1@ebookstore.com', 'author123', 'internal', 'Jane A. Smith', 2, '2022-05-01', NULL),
('publisher1@ebookstore.com', 'publisher123', 'internal', 'Alice Johnson', 3, '2022-06-15', 1),
('reader1@ebookstore.com', 'reader123', 'internal', 'Bob Brown', 4, NULL, NULL);
