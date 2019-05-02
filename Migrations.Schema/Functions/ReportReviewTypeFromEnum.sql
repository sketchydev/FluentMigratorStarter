IF EXISTS (SELECT 1 
		   FROM INFORMATION_SCHEMA.ROUTINES
		   WHERE SPECIFIC_SCHEMA = 'dbo' AND 
		         SPECIFIC_NAME = 'ReportReviewTypeFromEnum' AND 
		         ROUTINE_TYPE = 'FUNCTION')
	DROP FUNCTION dbo.ReportReviewTypeFromEnum
go

CREATE FUNCTION dbo.ReportReviewTypeFromEnum (@ReviewType int)
RETURNS varchar(20)
AS BEGIN
	IF (@ReviewType = 0) RETURN 'Health Review'
	ELSE
	BEGIN
		IF (@ReviewType = 1) RETURN 'Screening Test'
	END
	RETURN '<unknown>'
END