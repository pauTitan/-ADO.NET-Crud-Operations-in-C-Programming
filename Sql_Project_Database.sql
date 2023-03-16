use ABC_Bank

CREATE TABLE Account_Details1(
ACC_ID VARCHAR(10)
 PRIMARY KEY CHECK (ACC_ID LIKE 'SB%' OR ACC_ID LIKE 'JT%'OR ACC_ID LIKE 'CU%'),
ACC_TYPE VARCHAR(10) NOT NULL );





CREATE TABLE BRANCH_DETAILS1(
BRANCH_ID VARCHAR(5)  
 PRIMARY KEY CHECK (BRANCH_ID LIKE 'ABC%'),
BRANCH_NAME VARCHAR(50) NOT NULL
);


CREATE TABLE CUSTOMER_DETAILS1(
CID INT PRIMARY KEY IDENTITY (1,1),
NAME VARCHAR(100) NOT NULL,
ACC_ID VARCHAR(10) FOREIGN KEY (ACC_ID) REFERENCES Account_Details1 (ACC_ID) ,
BRANCH_ID VARCHAR(5) FOREIGN KEY (BRANCH_ID) REFERENCES BRANCH_DETAILS1 (BRANCH_ID) ,
ADHAR_CARD INT UNIQUE NOT NULL,
BALANCE FLOAT CHECK (BALANCE>0),
ACCOUNT_OPEN_DATE DATETIME NOT NULL CHECK (ACCOUNT_OPEN_DATE = GetDate()),
COUNTRY VARCHAR(20) NOT NULL DEFAULT 'INDIA'
);





SELECT *
FROM BRANCH_DETAILS1;

SELECT *
FROM Account_Details1;

----------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE USP_INSERT  @name varchar(50),@acc_id varchar(50),@branch_id varchar(50),@adhar_card int,@balance float,@country varchar(50)
AS
BEGIN
INSERT INTO CUSTOMER_DETAILS1 (NAME,ACC_ID,BRANCH_ID,ADHAR_CARD,BALANCE,ACCOUNT_OPEN_DATE,COUNTRY)
VALUES (@name,@acc_id,@branch_id,@adhar_card,@balance,getdate(),@country)
END


EXEC USP_INSERT @name='PAVAN', @acc_id='SB',@branch_id ='ABC2',@adhar_card=12345,@balance=10000,@country='INDIA';


-----------------------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE USP_UPDATE @balance FLOAT,@adhar_no INT
AS
BEGIN
UPDATE CUSTOMER_DETAILS1 set BALANCE=@balance
where ADHAR_CARD= @adhar_no
END

EXEC USP_UPDATE @adhar_no=1234,@balance=21000;


------------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE USP_DELETE @adhar_no INT
AS
BEGIN
DELETE FROM CUSTOMER_DETAILS1 WHERE ADHAR_CARD =@adhar_no
END

EXEC USP_DELETE @adhar_no =1234;


-----------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE GET_SINGLE @adhar_no int
AS
BEGIN
     SELECT *
	 FROM CUSTOMER_DETAILS1
	 WHERE ADHAR_CARD = @adhar_no
END


-----------------------------------------------------------------------------------------------------------


CREATE PROCEDURE USP_DETAILS
AS
BEGIN
SELECT * FROM CUSTOMER_DETAILS1;
END

EXEC USP_DETAILS;



