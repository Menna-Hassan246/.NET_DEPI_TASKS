
CREATE DATABASE CM
GO
use CM
GO 
CREATE TABLE Department (
    DNum INT PRIMARY KEY,
    MANGRID INT NULL,
    DName NVARCHAR(255) NOT NULL,
    Hire_Date DATE NOT NULL,
    );
	 
	CREATE TABLE Employee (
    SSN INT PRIMARY KEY,
	DNum INT default null,
    MANGRID INT NULL ,
    Gender CHAR check (GENDER = 'f' or gender = 'm'),
    BirthDate DATE NULL,
    FName NVARCHAR(255) NOT NULL,
    LName NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_Manager FOREIGN KEY (MANGRID) REFERENCES Employee(SSN)
	ON UPDATE  NO ACTION
      ON DELETE NO ACTION,
	  CONSTRAINT EMP_DEP 
FOREIGN KEY (DNum)REFERENCES Department (DNum) ON UPDATE  NO ACTION
      ON DELETE NO ACTION);


	  ALTER TABLE department
ADD CONSTRAINT MANGE_DEP FOREIGN KEY (MANGRID) REFERENCES Employee(SSN) ON UPDATE CASCADE
      ON DELETE SET DEFAULT;


CREATE TABLE Department_Locations(
    DNum INT  DEFault null,
    Location NVARCHAR(255),
    PRIMARY KEY(DNum,Location),
    FOREIGN KEY (DNum)REFERENCES Department(DNum)  ON UPDATE CASCADE
      ON DELETE SET DEFAULT);

CREATE TABLE Project (
    PNumber INT PRIMARY KEY,
    DNum INT DEFault null, 
    PName NVARCHAR(255) NOT NULL unique,
    City NVARCHAR(255) NOT NULL,
CONSTRAINT project_DEP FOREIGN KEY (DNum) REFERENCES Department(DNum) ON UPDATE CASCADE
      ON DELETE SET DEFAULT);

CREATE TABLE Working_Hours(
    SSN INT DEFault null,
    PNumber INT DEFault null,
    working_hr INT NOT NULL,
    PRIMARY KEY(SSN,PNumber),
    FOREIGN KEY (SSN)REFERENCES Employee(SSN)  ON UPDATE NO ACTION 
      ON DELETE  NO ACTION ,
    FOREIGN KEY (PNumber)REFERENCES Project(PNumber)  ON UPDATE  CASCADE
      ON DELETE SET DEFAULT);

	 

CREATE TABLE Dependent(
    SSN INT default NULL,
    Name NVARCHAR(255) NOT NULL,
    Gender CHAR NOT NULL,
    BirthDate DATE NULL,
    PRIMARY KEY(SSN,Name),
    FOREIGN KEY (SSN)REFERENCES Employee(SSN)  ON UPDATE CASCADE
      ON DELETE SET DEFAULT);


	  USE CM;
GO

-- 1. Insert Departments (no MANGRID yet because Employees not added)
INSERT INTO Department (DNum, MANGRID, DName, Hire_Date) VALUES
(1, NULL, 'Human Resources', '2015-03-01'),
(2, NULL, 'Engineering', '2016-06-15'),
(3, NULL, 'Sales', '2018-01-10');

-- 2. Insert Employees
INSERT INTO Employee (SSN, DNum, MANGRID, Gender, BirthDate, FName, LName) VALUES
(101, 1, NULL, 'f', '1985-05-15', 'Sarah', 'Ali'),         -- HR Manager
(102, 2, NULL, 'm', '1982-03-22', 'Ahmed', 'Kamal'),       -- Engineering Manager
(103, 3, NULL, 'f', '1990-11-12', 'Mona', 'Said'),         -- Sales Manager
(104, 2, 102, 'm', '1995-08-30', 'Youssef', 'Hassan'),     -- Engineer
(105, 2, 102, 'f', '1996-07-14', 'Laila', 'Nabil'),        -- Engineer
(106, 3, 103, 'm', '1993-09-01', 'Omar', 'Samir');         -- Salesperson

-- 3. Update departments to set managers now that employees exist
UPDATE Department SET MANGRID = 101 WHERE DNum = 1;
UPDATE Department SET MANGRID = 102 WHERE DNum = 2;
UPDATE Department SET MANGRID = 103 WHERE DNum = 3;

-- 4. Insert Department Locations
INSERT INTO Department_Locations (DNum, Location) VALUES
(1, 'Cairo'),
(2, 'Alexandria'),
(2, 'Cairo'),
(3, 'Giza');

-- 5. Insert Projects
INSERT INTO Project (PNumber, DNum, PName, City) VALUES
(201, 2, 'AI Research', 'Alexandria'),
(202, 2, 'Web Platform', 'Cairo'),
(203, 3, 'Marketing Campaign', 'Giza');

-- 6. Insert Working Hours (who works on what project)
INSERT INTO Working_Hours (SSN, PNumber, working_hr) VALUES
(104, 201, 35),
(104, 202, 10),
(105, 202, 40),
(106, 203, 30);

-- 7. Insert Dependents
INSERT INTO Dependent (SSN, Name, Gender, BirthDate) VALUES
(101, 'Layla', 'f', '2010-04-22'),
(102, 'Omar', 'm', '2008-09-13'),
(104, 'Hana', 'f', '2015-06-10'),
(105, 'Yassin', 'm', '2017-01-01');
