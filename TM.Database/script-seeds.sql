USE TaskPlaner
GO

INSERT INTO [User](UserName,[Password],FirstName,LastName)
VALUES ('admin','123','No','Name');
GO

INSERT INTO [Phase]([Name],AcceptMoveId,Code)
VALUES 
	('Destroy',0,'D'),
	('Completed',3,'C'),
	('Order',4,'Or'),
	('Quote',5,'Q'),
	('Opportunity',5,'Op');
GO

INSERT INTO [Priority]([Name])
VALUES 
	('Medium'),
	('Emergency');
GO



