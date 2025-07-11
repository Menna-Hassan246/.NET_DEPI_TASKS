CREATE TABLE Employee (
    SSN INT PRIMARY KEY,
    MANGRID INT NULL ,
    Gender CHAR,
    BirthDate DATE NULL,
    FName NVARCHAR(255) NOT NULL,
    LName NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_Manager FOREIGN KEY (MANGRID) REFERENCES Employee(SSN));

CREATE TABLE Department (
    DNum INT PRIMARY KEY,
    MANGRID INT NULL,
    DName NVARCHAR(255) NOT NULL,
    Hire_Date DATE NOT NULL,
    CONSTRAINT MANGE_DEP FOREIGN KEY (MANGRID) REFERENCES Employee(SSN));

ALTER TABLE Employee
    ADD DNum INT;

ALTER TABLE Employee
ADD CONSTRAINT EMP_DEP
FOREIGN KEY (DNum)REFERENCES Department (DNum);

CREATE TABLE Department_Locations(
    DNum INT,
    Location NVARCHAR(255),
    PRIMARY KEY(DNum,Location),
    FOREIGN KEY (DNum)REFERENCES Department(DNum));

CREATE TABLE Project (
    PNumber INT PRIMARY KEY,
    DNum INT, 
    PName NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
CONSTRAINT project_DEP FOREIGN KEY (DNum) REFERENCES Department(DNum));

CREATE TABLE Working_Hours(
    SSN INT,
    PNumber INT,
    working_hr INT NOT NULL,
    PRIMARY KEY(SSN,PNumber),
    FOREIGN KEY (SSN)REFERENCES Employee(SSN),
    FOREIGN KEY (PNumber)REFERENCES Project(PNumber));

CREATE TABLE Dependent(
    SSN INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Gender CHAR NOT NULL,
    BirthDate DATE NULL,
    PRIMARY KEY(SSN,Name),
    FOREIGN KEY (SSN)REFERENCES Employee(SSN));

INSERT INTO Employee (SSN, MANGRID, Gender,BirthDate, FName, LName,DNum)
VALUES (20,NULL, 'M', '2004-08-24', 'MENNA', 'HASSAN',10);

INSERT INTO dbo.Employee(SSN,MANGRID,Gender,BirthDate,FName,LName,DNum)
VALUES(44, 2,'F','2002-09-23','ASYA','MAHMOUD',20),
    (22,2,'F','2003-01-03','AYA','MAHMOUD',20),
    (4,2,'M','2000-01-10','ALI','HASSAN',20),
	(5,2,'M','2000-06-01','AHMED','SAED',20),
	(6,3,'M','2002-05-01','MOHAMED','YASSER',30);

INSERT INTO dbo.Department(DNum,MANGRID, DName, Hire_Date)
VALUES( 10,1,'HR','2010-01-01')
    (20,2,'IT','2011-02-13'),
    (30,3,'PR','2010-01-01');

UPDATE dbo.employee
SET DNUM=10
WHERE SSN=2AND SSN=3;

DELETE FROM dbo.Dependent WHERE NAME='EMAN';

SELECT * FROM dbo.Employee WHERE DNum=20;
SELECT E.FNAME,E.LNAME ,P.PNAME FROM EMPLOYEE E 
JOIN Working_Hours W  ON E.SSN=W.SSN 
JOIN Project P ON P.PNumber=W.PNumber
    WHERE W.working_hr IS NOT NULL;
