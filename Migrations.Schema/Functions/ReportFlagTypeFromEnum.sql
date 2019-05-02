IF EXISTS (SELECT * 
		   FROM INFORMATION_SCHEMA.ROUTINES
		   WHERE SPECIFIC_SCHEMA = 'dbo' 
		   AND SPECIFIC_NAME = 'ReportFlagTypeFromEnum' 
		   AND ROUTINE_TYPE = 'FUNCTION')
DROP FUNCTION dbo.ReportFlagTypeFromEnum
GO

CREATE FUNCTION dbo.ReportFlagTypeFromEnum (@FlagType int)
RETURNS varchar(20)
AS BEGIN
	IF (@FlagType = 0) RETURN 'Health Review'
	RETURN '<unknown>'
END
GO