IF EXISTS (SELECT * 
		   FROM INFORMATION_SCHEMA.ROUTINES
		   WHERE SPECIFIC_SCHEMA = 'dbo' 
		   AND SPECIFIC_NAME = 'ReportSuspensionTypeFromEnum' 
		   AND ROUTINE_TYPE = 'FUNCTION')
DROP FUNCTION dbo.ReportSuspensionTypeFromEnum
GO

CREATE FUNCTION dbo.ReportSuspensionTypeFromEnum (@suspensionType int)
RETURNS varchar(25)
AS
BEGIN
	DECLARE @returnValue varchar(25)
	SET @returnValue =
		CASE @suspensionType
		WHEN 0 THEN 'none'
		WHEN 1 THEN 'Suspend this immunisation'
		WHEN 2 THEN 'Suspend all immunisations'
		ELSE null
		END
	RETURN @returnValue
END
GO